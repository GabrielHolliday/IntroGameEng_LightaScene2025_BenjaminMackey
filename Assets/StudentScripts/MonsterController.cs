using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System;
using System.Threading.Tasks;

public class MonsterController : MonoBehaviour
{

    static System.Random rand = new System.Random();
    static List<GameObject> nodes = new List<GameObject>();
    public bool runMonsterCycle = false;
    public GameObject plr;
    public GameObject monsterBody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        GameObject[] n1 = GameObject.FindGameObjectsWithTag("BypassNode");
        GameObject[] n2 = GameObject.FindGameObjectsWithTag("StopNode");
        for (int i = 0; i < n1.Length; i++)
        {
            nodes.Add(n1[i]);
        }
        for (int i = 0; i < n2.Length; i++)
        {
            nodes.Add(n2[i]);
        }
        
    }
    void Start()
    {
        moveCycle();
       
    }

    static GameObject curNode = null;
    private async void moveCycle()
    {
        runMonsterCycle = true;
        while (runMonsterCycle == true)
        {

            List<List<GameObject>> possibleMovePoints = new List<List<GameObject>>();//saves as full paths, so each internal list is a list of nodes that make up a path
            //List<GameObject> bypassNodesToHit = new List<GameObject>();
            for (int i = 0; i < nodes.Count; i++)
            {

                if (nodes[i] == curNode) continue;
                bool fT = true;
                
                GameObject passbackNode =  monsterBody; 
                GameObject tNode = nodes[i];
                GameObject internalPrevNode = null;
                int emergencyCount = 0;
                while (passbackNode != null && tNode != null)
                {
                    emergencyCount++;
                    RaycastHit hit;
                    if (!Physics.Linecast(passbackNode.transform.position, tNode.transform.position, out hit))
                    {
                        break;
                    }
                    if (hit.transform.gameObject == tNode && hit.collider.gameObject.CompareTag("StopNode"))
                    {
                        if(fT == true)
                        {
                            possibleMovePoints.Add(new List<GameObject>());
                        }
                        fT = false;
                        Debug.Log("Stop");
                        possibleMovePoints[possibleMovePoints.Count - 1].Add(hit.transform.gameObject);
                        passbackNode = null;
                    }
                    else if (hit.transform.gameObject == tNode && hit.transform.gameObject.CompareTag("BypassNode"))
                    {
                        if (fT == true)
                        {
                            possibleMovePoints.Add(new List<GameObject>());
                        }
                        fT = false;
                        internalPrevNode = hit.transform.gameObject;
                        Debug.Log("Bypass");
                        possibleMovePoints[possibleMovePoints.Count - 1].Add(hit.transform.gameObject);
                        passbackNode = hit.transform.gameObject;
                        //looking for another bypass node
                        bool suc = false;
                        for (int j = 0; j < nodes.Count; j++)
                        {
                            RaycastHit internalHit;
                            if (!Physics.Linecast(passbackNode.transform.position, nodes[j].transform.position, out internalHit))
                            {
                                continue;
                            }
                            Debug.Log(possibleMovePoints[possibleMovePoints.Count - 1].Contains(internalHit.transform.gameObject));
                            if ((internalHit.collider.gameObject.CompareTag("StopNode") | internalHit.collider.gameObject.CompareTag("BypassNode")) && internalHit.transform.gameObject != curNode && !possibleMovePoints[possibleMovePoints.Count - 1].Contains(internalHit.transform.gameObject))//fix here
                            {
                                suc = true; 
                                tNode = internalHit.transform.gameObject;
                            }
                            
                        }
                        if(suc == false)
                        {
                            tNode = null;
                        }
                    }
                    else
                    {
                        //Debug.Log(hit.collider.gameObject.name);
                        passbackNode = null; //just in case
                    }
                    if (emergencyCount > 5) break;
                }
            }


            if (possibleMovePoints.Count == 0) Debug.Log("Twin something not working");

            await Task.Delay(rand.Next(5000, 10000));


            List<GameObject> chosenPath = possibleMovePoints[rand.Next(0, possibleMovePoints.Count)];
            /*
            for (int i = 0; i < chosenPath.Count; i++)
            {
                Debug.Log(chosenPath[i].name);
            }
            */
            for (int i = 0; i < chosenPath.Count; i++)
            {
                Vector3 endPos = chosenPath[i].transform.position;
                Vector3 startPos = monsterBody.transform.position;

                for (int j = 0; j < 1000/chosenPath.Count; j++) // change to use some kind of vector3.magnitude or something
                {
                    monsterBody.transform.position = Vector3.Lerp(startPos, endPos, (float)j / (float)(1000/chosenPath.Count));
                    await Task.Delay(1);
                }
            }
            curNode = chosenPath[chosenPath.Count - 1];

            /*Vector3 movePos = possibleMovePoints[rand.Next(0, possibleMovePoints.Count)].transform.position;
            
            
            for (int i = 0; i < bypassNodesToHit.Count; i++)
            {
                await Task.Delay(1);
                monsterBody.transform.position = Vector3.Lerp(startPos, movePos, (float)i / 1000);
            }
            */
        }
    }


    // Update is called once per frame
    void Update()
    {
        //moving the plane so its looking at the player
        monsterBody.transform.LookAt(plr.transform);

    }
}
