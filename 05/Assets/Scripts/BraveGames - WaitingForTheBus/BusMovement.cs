using UnityEngine;
using System.Collections;

public class BusMovement : MonoBehaviour {
    public Vector2 startpos;
    public Vector2 endpos;
    private float busSpeed = Time.deltaTime;
    public float time;
	// Use this for initialization
	void Start () {
        time = 0;
        startpos = new Vector2(-2, 0.06f);
        endpos = new Vector2(2, 0.06f);
        float waitTime = 1 + Random.value;
        StartCoroutine(startMovingBus(waitTime));
	}
    //After a certain amount of time the buss starts to move
    IEnumerator startMovingBus(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector2.Lerp(startpos, endpos, time);
        time += busSpeed;        
	}
    //Stops the bus
    public void StopBus()
    {
            busSpeed = 0;
    }
}
