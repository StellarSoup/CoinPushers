using UnityEngine;
using System.Collections;

public class BG_ButtonPresser_Menu : GameState {


	private GameObject instructions;
    //Keeps track of if the player needs to press the button
    //and if the player has won
    public bool playerNeedsToPressTheButton;
    public bool playerHasWon;


    // Use this for initialization
    void Start () {
        playerHasWon = false;
        instructions = GameObject.Find("Instructions/Text");
        setTimer();
	}

    //Set if the player will lose at the end of the game or not
    void setTimer()
    { 
        int choice = (int)(Random.value * 2);

        //If the player needs to press the button
        if (choice == 0)
        {
            playerNeedsToPressTheButton = true;
            instructions.GetComponent<TextMesh>().text = "Do Press" + "\n" + "The Button";
            TimerLose();
        }
        //If the player does not need to press the button
        else
        {
            playerNeedsToPressTheButton = false;
            instructions.GetComponent<TextMesh>().text = "Don't Press" + "\n" + "The Button";
            TimerWin();
        }

        print("playerNeedsToPressTheButton = " + playerNeedsToPressTheButton);
    }
}
