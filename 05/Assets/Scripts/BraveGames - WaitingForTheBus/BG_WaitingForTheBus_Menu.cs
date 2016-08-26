using UnityEngine;
using System.Collections;

public class BG_WaitingForTheBus_Menu : GameState {

    public BusMovement bus;
    private bool playerMadeMove;
	// Use this for initialization
	void Start () {
        playerMadeMove = false;
        TimerLose();
    }
	
	// Update is called once per frame
	void Update () {
        //If the mouse is down
        if (Input.GetMouseButtonDown(0))
        {
            if (!playerMadeMove)
            {
                
                float distanceFromBus = Vector2.Distance(bus.transform.GetChild(0).position, new Vector2(GameObject.Find("BusStop").transform.position.x,bus.transform.GetChild(0).position.y));
                print("DistFromBus " + distanceFromBus);
                //If the bus is close to the bus stop they win
                if (distanceFromBus < 0.4f)
                {
                    bus.StopBus();
                    WinGame();
                }
                //Else they lose
                else
                {
                    Lose();
                }
                playerMadeMove = true;
            }
        }
	}
}
