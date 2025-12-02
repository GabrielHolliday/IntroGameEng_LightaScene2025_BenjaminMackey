using System.Collections.Generic;

using UnityEngine;
using System.Collections;

using System;

public class LightController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool flickering = false;
    private bool forceOffFoeva = false;

    static System.Random rand = new System.Random();

    static GameObject[] lights; 
    public List<bool> lightBaseState = new List<bool>();

    private void buildDefaultState()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].transform.Find("CeilingLight OFF").gameObject.SetActive(true);
            if (lights[i].transform.Find("CeilingLight ON") != null && lights[i].transform.Find("CeilingLight ON").gameObject.activeInHierarchy == true)
            {
                lightBaseState.Add(true);
                //Debug.Log("true");
            }
            else
            {
                lightBaseState.Add(false);
                //Debug.Log("false");
            }

        }
    }
    void Awake()
    {
        lights = GameObject.FindGameObjectsWithTag("CeelingLight");
        buildDefaultState();
        //Debug.Log(lights[1].transform.Find("CeilingLight ON") == null);
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private List<GameObject> alreadyFlickering = new List<GameObject>();

    public IEnumerator flicker(GameObject light)
    {
        if (alreadyFlickering.Contains(light)) yield break;
        alreadyFlickering.Add(light);
        light.GetComponent<Animator>().enabled = true;
        light.GetComponent<Animator>().Play("Flicker");
        yield return new WaitForSeconds(1);
        light.GetComponent<Animator>().enabled = false;
        if (light.transform.Find("CeilingLight ON"))
        {
            light.transform.Find("CeilingLight ON").gameObject.SetActive(lightBaseState[Array.IndexOf<GameObject>(lights, light)]);
        }
        alreadyFlickering.Remove(light);
        if(forceOffFoeva == true)
        {
            light.transform.Find("CeilingLight ON").gameObject.SetActive(false);
        }
    }
    
    public IEnumerator flickerAll()
    {
        flickering = true;
        while(flickering == true)
        {
            for (int i = 0; i < lights.Length; i++)
            {
                StartCoroutine( flicker(lights[i]));
                yield return new WaitForSeconds(0.03f);
            }
            yield return new WaitForSeconds(1f);
        }
        
    }

    public void TurnAllOff()
    {
        forceOffFoeva = true;
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].transform.Find("CeilingLight ON").gameObject.SetActive(false);
            
        }
        for (int i = 0; i < lightBaseState.Count; i++)
        {
            lightBaseState[i] = false;
        }
        StartCoroutine(flickerAll());
    }

    // Update is called once per frame
    void Update()
    {
        if(rand.Next(0,999) >= 950 && forceOffFoeva == false)
        {
            StartCoroutine( flicker(lights[rand.Next(0, lights.Length - 1)]));
        }
        
    }
}
