using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using System.Collections;

public class CollectibleManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private System.Random random = new System.Random();
    private List<GameObject> collectibles = new List<GameObject>();
    private AudioSource audioSource;
    private TMP_Text scoreText;
    private Image winImage;
    private Image winImageSpecial;
    public float collectedRatio = 0f;
    [SerializeField]GameObject WinTrigger;

    private int score;
    private int totalScoreNeeded;
    [SerializeField] GameObject outsideEvent;

    [SerializeField] AudioClip pickupSound;
    [SerializeField] AudioClip birdsChirping;
    void Start()
    {
        winImage = transform.Find("CollectiblesUI").transform.Find("Image1").GetComponent<Image>();
        winImageSpecial = transform.Find("CollectiblesUI").transform.Find("Image2").GetComponent<Image>();
        audioSource = transform.Find("PickupSoundSource").GetComponent<AudioSource>();
        scoreText = transform.Find("CollectiblesUI").transform.Find("ScoreText").GetComponent<TMP_Text>();
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).name == "CollectiblesUI" || transform.GetChild(i).name == "PickupSoundSource") continue;
            collectibles.Add(transform.GetChild(i).gameObject);
            totalScoreNeeded ++;
        }
        scoreText.text = score +  "/" + totalScoreNeeded;
    }

    public void AllertCollected(GameObject collected)
    {
        Debug.Log("hehoo");
        if(!collectibles.Contains(collected)) return;
        Debug.Log("yohoo");
        collectibles.Remove(collected);
        audioSource.pitch = (float)random.Next(98,103) / 100f;
        audioSource.PlayOneShot(pickupSound);
        collected.SetActive(false);
        score ++;
        scoreText.text = score +  "/" + totalScoreNeeded;
        collectedRatio = (float)score / (float)totalScoreNeeded;
        if(score >= totalScoreNeeded)
        {
            scoreText.text = "Run to the exit!";
            outsideEvent.SetActive(true);
            WinTrigger.SetActive(true);
        }

    }

    public IEnumerator Win()
    {
        Color clr= winImage.color;
        clr.a = 1;
        audioSource.pitch = 1;
        audioSource.PlayOneShot(birdsChirping);
        winImage.gameObject.SetActive(true);
        
        winImage.CrossFadeAlpha(0,0, true);
        winImage.CrossFadeAlpha(255f, 2.7f, true);
        if(random.Next(1,101) == 1)
        {
            winImageSpecial.CrossFadeAlpha(0,0, true);
            winImageSpecial.CrossFadeAlpha(70f, 2.7f, true);
        }
        yield return new WaitForSeconds(5);
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
