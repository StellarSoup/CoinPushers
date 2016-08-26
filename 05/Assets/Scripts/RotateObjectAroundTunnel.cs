using UnityEngine;
using System.Collections;

public class RotateObjectAroundTunnel : BG_TunnelRunner_Menu {

    public GameObject tunnel;

    private bool playerHasWon;

    //Rotates the player to the left
    public void RotateLeft()
    {
        Camera.main.transform.Rotate(new Vector3(0, 0, -90));
    }
    //Rotates the player to the right
    public void RotateRight()
    {
        Camera.main.transform.Rotate(new Vector3(0, 0, 90));
    }
	// Use this for initialization
	void Start () {
        playerHasWon = false;
        //Generates a tunnel at different rotations
        for (int i = 0; i < 15; i++)
        {
            GameObject tun = Instantiate(tunnel);
            tun.transform.position = new Vector3(tun.transform.position.x, tun.transform.position.y, tun.transform.position.z + (15f*(i+1)));
            int ranRot = (Random.Range(0,4));
            tun.transform.Rotate(0, 0, ranRot*90);
        }
	}
	
	// Update is called once per frame
	void Update () {
        //If the player presses the right arrow key
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //They will rotate right
            RotateRight();
        }
        //If they press the left arrow key
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //They will rotate left
            RotateLeft();
        }
        //Keep moving the player forward
        Camera.main.transform.position += new Vector3(0, 0, 0.15f);
	}

    void OnCollisionEnter(Collision col)
    {
        //If the player collides with the barrier they will lose
        if (col.transform.parent.name.Equals("Barrier") && GameObject.Find("Timer").GetComponent<GameStopWatch>().time > 0)
        {
            Lose();
            Destroy(transform.GetComponent<Rigidbody>());
        }
    }
}
