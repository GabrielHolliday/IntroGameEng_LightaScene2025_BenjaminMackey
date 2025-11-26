using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

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
        Debug.Log("1");
        playableDirector.Play();
        Debug.Log("2");
        yield return new WaitForSeconds(rand.Next(2, (int)markers[0].time));
        Debug.Log("3");
        playableDirector.time = markers[0].time;
        Debug.Log("4");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
