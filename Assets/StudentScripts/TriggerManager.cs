using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Playables;

public class TriggerManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public MonsterJumpscarrer jumpscarrer;
    public GameObject monsterAppearEvent;
    public GameObject flyByEvent;
    public CollectibleManager collectibleManager;

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



   
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.CompareTag("Collectible")) collectibleManager.AllertCollected(other.transform.parent.gameObject);
        if(!triggers.Contains(other.gameObject)) return;
        Debug.Log(other.gameObject.name);
        triggers.Remove(other.gameObject);
        switch(other.gameObject.name)
        {
            case "Flashlight":
                overlay.SetActive(true);
                triggerParent.transform.Find("MonsterFlyBy").gameObject.SetActive(true);
                overlay.GetComponent<PlayableDirector>().Play();
                break;
            case "MonsterAppear":
                monsterAppearEvent.SetActive(true);
                break;
            case "MonsterBody":
                StartCoroutine(jumpscarrer.Jumpscare());
                break;
            case "MonsterFlyBy":
                flyByEvent.SetActive(true);
                break;
            case "Win":
                StartCoroutine(collectibleManager.Win());
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
