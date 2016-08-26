using UnityEngine;
using System.Collections;

public class PongPaddleControls : MonoBehaviour {

    private GameObject pongBall;

    private bool beingControled;

	// Use this for initialization
	void Start () {
        beingControled = false;
        pongBall = GameObject.Find("Pong Ball");
	}
    private bool ballIsAbove()
    {
        return transform.position.y < pongBall.transform.position.y;
    }
    private float distanceFromBall()
    {
        float distance = transform.position.y - pongBall.transform.position.y;
        if(distance < 0)
        {
            distance *= -1;
        }
        return distance;
    }

    private void Automovement()
    {
        float speed = 0.11f;
        if (!ballIsAbove())
        {
            speed *= -1;
        }
        float yPos = Mathf.Clamp(transform.position.y + speed, -3.9f, 3.9f);
        if (distanceFromBall() > 0.1f)
        {
            transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
        }
    }

	
	// Update is called once per frame
	void Update () {
        if (!beingControled)
        {
            Automovement();
        }
	}
    void OnMouseDrag()
    {
        beingControled = true;
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        point.y = Mathf.Clamp(point.y, -3.4f, 3.4f);
        point.z = gameObject.transform.position.z;
        point.x = gameObject.transform.position.x;

        gameObject.transform.position = point;
    }
}
