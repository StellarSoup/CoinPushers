using UnityEngine;
using System.Collections;

public class BG_WesternShootout_Menu : GameState {

    private bool playerHasWon;

	// Use this for initialization
	void Start () {
        playerHasWon = false;
        TimerLose();
    }
	
	// Update is called once per frame
	void Update () {
        //Gets all of the ducks being spawned
        WesternEnemy[] animal = FindObjectsOfType(typeof(WesternEnemy)) as WesternEnemy[];
        //if there are no duck left the player wins
        if(animal.Length == 0 && !playerHasWon)
        {
            playerHasWon = true;
            WinGame();
        }
    }
}
