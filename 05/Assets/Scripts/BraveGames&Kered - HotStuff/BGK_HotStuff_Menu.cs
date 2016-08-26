using UnityEngine;
using System.Collections;

public class BGK_HotStuff_Menu : GameState {

	// Use this for initialization
	void Start () {
        //Set the game to lose when the time hits 0
        TimerLose();
    }
}
