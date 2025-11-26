using UnityEngine;

public class MonsterBodyTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public MonsterJumpscarrer jumpscarrer;
    void Start()
    {
     
        
    }
    private bool canJumpscare = true;
    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Colliding");
        if (collider.CompareTag("Player") && canJumpscare == true)
        {
            Debug.Log("a");
            canJumpscare = false;
            StartCoroutine(jumpscarrer.Jumpscare());
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
