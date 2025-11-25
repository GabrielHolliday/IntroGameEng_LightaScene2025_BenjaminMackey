using System.Collections;
using UnityEngine;

public class SignalWaiter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    static System.Random rand = new System.Random();
    private sign
    void Start()
    {
        
    }

    public IEnumerator RandomWaitAndGiveSignal()
    {
        yield return new WaitForSeconds(rand.Next(3, 10));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
