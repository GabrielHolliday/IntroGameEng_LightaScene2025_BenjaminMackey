using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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

    private async void moveCycle()
    {
        runMonsterCycle = true;
        while (runMonsterCycle == true)
        {

            List<GameObject> possibleMovePoints = new List<GameObject>();
            List<GameObject> bypassNodesToHit = new List<GameObject>();
            for (int i = 0; i < nodes.Count; i++)
            {
                GameObject passbackNode = monsterBody;
                while (passbackNode != null)
                {
                    RaycastHit hit;
                    if (!Physics.Linecast(passbackNode.transform.position, nodes[i].transform.position, out hit))
                    {
                        break;
                    }
                    if (hit.collider.gameObject.CompareTag("StopNode"))
                    {
                        Debug.Log("Stop");
                        possibleMovePoints.Add(nodes[i]);
                        passbackNode = null;
                    }
                    else if (hit.collider.gameObject.CompareTag("BypassNode"))
                    {
                        Debug.Log("Bypass");
                        passbackNode = hit.collider.gameObject;
                    }
                    else
                    {
                        Debug.Log(hit.collider.gameObject.name);
                        passbackNode = null; //just in case
                    }
                }
            }


            if (possibleMovePoints.Count == 0) Debug.Log("Twin something not working");

            await Task.Delay(rand.Next(5000, 10000));

            //WIP !!!!!!!!!! CLEAR UP STUFF WITH THE BYPASS NODES SO THE MONSTER ACTUALLY FOLLOWS IT

            Vector3 movePos = possibleMovePoints[rand.Next(0, possibleMovePoints.Count)].transform.position;
            Vector3 startPos = monsterBody.transform.position;
            
            for (int i = 0; i < bypassNodesToHit.Count; i++)
            {
                await Task.Delay(1);
                monsterBody.transform.position = Vector3.Lerp(startPos, movePos, (float)i / 1000);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        //moving the plane so its looking at the player
        monsterBody.transform.LookAt(plr.transform);

    }
}
