using System;
using System.Collections;
using System.Linq;
using Mono.Cecil.Cil;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MonsterJumpscarrer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private System.Random rand = new System.Random();
    public GameObject monsterBody;
    public GameObject playerBody;
    public TMP_Text text;

    private PlayableDirector playableDirector;
    private TimelineAsset timeline;
    private IMarker[] markers;


    //private BoxCollider monsterCollider;
    private string[] deathTexts = new string[] {"You're Dead...", "Another One bites the dust...", "You should've tried harder...", "3rd Strike... And you're out...", "Another quater wont get you out of this one..."};

    void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();    
        timeline = playableDirector.playableAsset as TimelineAsset;
        markers = timeline.markerTrack.GetMarkers().ToArray<IMarker>();
        
    }

  
        
    

    public IEnumerator Jumpscare()
    {
        
        text.text = deathTexts[rand.Next(0, deathTexts.Length)];

        playableDirector.Play();

        yield return new WaitForSeconds(rand.Next(2, (int)markers[0].time));

        playableDirector.time = markers[0].time;

        yield return new WaitForSeconds((float)playableDirector.duration - (float)markers[0].time);
        Application.Quit();
        
        
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
