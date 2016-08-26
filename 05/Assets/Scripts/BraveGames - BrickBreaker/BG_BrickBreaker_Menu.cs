using UnityEngine;
using System.Collections;

public class BG_BrickBreaker_Menu : GameState {
    //Gets the paddle ball and paddle
    public GameObject paddleBall;
    public GameObject paddle;
    private bool playerHasLost;
	// Use this for initialization
	void Start () {

        playerHasLost = false;
        //Moves the ball in a varying direction towards the blocks
        Rigidbody2D rigBod= paddleBall.GetComponent<Rigidbody2D>();
        float appliedForce = 120;
        rigBod.AddForce(new Vector2(Random.value*4, 5)* appliedForce);
        //Player wins when the timer reaches 0
        TimerWin();
	}
	
	// Update is called once per frame
	void Update () {
        //If the ball goes past the paddle the player loses
	    if(paddleBall.transform.position.y < paddle.transform.position.y && !playerHasLost && GameObject.Find("Timer&MusicPlayer").GetComponent<GameStopWatch>().time > 0)
        {
            Lose();
            playerHasLost = true;
        }
	}
}
