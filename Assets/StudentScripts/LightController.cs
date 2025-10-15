using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using System;
using Unity.Mathematics;
public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool flickering = false;

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

    public async void flicker(GameObject light)
    {
        if (alreadyFlickering.Contains(light)) return;
        alreadyFlickering.Add(light);
        light.GetComponent<Animator>().enabled = true;
        light.GetComponent<Animator>().Play("Flicker");
        await Task.Delay(1000);
        light.GetComponent<Animator>().enabled = false;
        if (light.transform.Find("CeilingLight ON"))
        {
            light.transform.Find("CeilingLight ON").gameObject.SetActive(lightBaseState[Array.IndexOf<GameObject>(lights, light)]);
        }
        alreadyFlickering.Remove(light);
    }
    
    private void flickerAll()
    {
        while(flickering == true)
        {
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(rand.Next(0,999) >= 995)
        {
            flicker(lights[rand.Next(0, lights.Length - 1)]);
        }
        
    }
}
