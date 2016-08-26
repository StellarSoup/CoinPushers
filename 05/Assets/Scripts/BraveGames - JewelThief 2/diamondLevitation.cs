using UnityEngine;
using System.Collections;

public class diamondLevitation : MonoBehaviour {

    private float time;
    private Vector3 startPos;
    private Vector3 endPos;
    private float speed;
	// Use this for initialization
	void Start () {
        startPos = transform.position;
        endPos = startPos + new Vector3(0, 0.15f, 0);
        time = 0;
        speed = Time.deltaTime*0.5f;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(startPos, endPos, time);
        time += speed;

        if(time >= 1 || time < 0)
        {
            speed *= -1;
        }

	}
}
