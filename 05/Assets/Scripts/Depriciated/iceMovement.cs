using UnityEngine;
using System.Collections;

public class iceMovement : MonoBehaviour {
    public float time;
    public float speed;
    public Vector2 startPos;
    private Vector3 extramovement;
	// Use this for initialization
	void Start () {
        time = 0;
        speed = 0;
        float randomx = Random.Range(-40, 40);
        float randomy = Random.Range(-20, 20);
        extramovement = new Vector2(randomx, randomy);
        transform.position = new Vector2(transform.position.x + randomx, transform.position.y);
        startPos = transform.position;

	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector2.Lerp(startPos, GameObject.FindGameObjectWithTag("Enemy").transform.position+ extramovement, time);
        time += speed;
        speed += Time.deltaTime/12;
        if(time >= 1)
        {
            GetComponent<Animator>().enabled = true;
        }
        if(time >= 3)
        {
            Destroy(gameObject);
        }
       
	}
}
