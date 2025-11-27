using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.Playables;

public class TriggerManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public MonsterJumpscarrer jumpscarrer;

    public GameObject overlay;
    public GameObject triggerParent;
    public GameObject monsterBody;

    private List<GameObject> triggers = new List<GameObject>();
    void Start()
    {
        for (int i = 0; i < triggerParent.transform.childCount; i++)
        {
            triggers.Add(triggerParent.transform.GetChild(i).gameObject);
        }
        triggers.Add(monsterBody);
        Debug.Log("Player Trigger Starting");
    }



    private void OnCollisionEnter(Collision other)
    {
        if(!triggers.Contains(other.gameObject)) return;
        Debug.Log(other.gameObject.name);
        triggers.Remove(other.gameObject);
        switch(other.gameObject.name)
        {
            case "Flashlight":
                overlay.SetActive(true);
                overlay.GetComponent<PlayableDirector>().Play();
                break;
            case "BossEntr":
                break;
            case "MonsterBody":
                StartCoroutine(jumpscarrer.Jumpscare());
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if(!triggers.Contains(other.gameObject)) return;
        Debug.Log(other.name);
        triggers.Remove(other.gameObject);
        switch(other.gameObject.name)
        {
            case "Flashlight":
                overlay.SetActive(true);
                overlay.GetComponent<PlayableDirector>().Play();
                break;
            case "BossEntr":
                break;
            case "MonsterBody":
                StartCoroutine(jumpscarrer.Jumpscare());
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
