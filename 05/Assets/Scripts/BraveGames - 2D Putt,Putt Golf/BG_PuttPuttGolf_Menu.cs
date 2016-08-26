using UnityEngine;
using System.Collections;

public class BG_PuttPuttGolf_Menu : GameState{
	
    
	void Start () {
        //When the timer reaches 0 they will lose the game
        TimerLose();
        //Load in a new play area
        LoadInNewPlayArea();
    }

    //Loads in a new play area with a ball and a hole
    private void LoadInNewPlayArea()
    {
        int randomPlayZone = (int)((Random.value * 3) + 1);
        GameObject playZone = Resources.Load("Prefabs/00-2DPuttPuttGolf/Areas/" + randomPlayZone.ToString()) as GameObject;
        Instantiate(playZone);
    }
}
