using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class FingerFighterControls : BG_FingerFighter_Menu {

    //Keeps track of the commands left to be inputted
    private int[] command;
    private int listLength;
    private bool playerHasWon;
    
    public Image leftArrow, rightArrow;
    //current command that has to be inputted
    private int currentCommand;

	// Use this for initialization
	void Start () {
        //Randomises a list
        listLength = Random.Range(8, 11);
        currentCommand = 0;
        command = new int[listLength];
        for (int i = 0; i < listLength; i++)
        {
            //Prints out 0s and 1s
            int ranNumber = Random.Range(0, 2);
            command[i] = ranNumber;
        }
        //Displays the list
        DisplayList();
        playerHasWon = false;     
	}
    //Displays a list of left and right arrows for the player to input
    void DisplayList()
    {
        if (currentCommand < command.Length)
        {
            Image currentArrow = GameObject.Find("Field/Screen/Image").GetComponent<Image>();
            if (command[currentCommand] == 0)
            {
                currentArrow.sprite = leftArrow.sprite;
            }
            else
            {
                currentArrow.sprite = rightArrow.sprite;
            }
            int num = command.Length - currentCommand;
            Text txt = GameObject.Find("NumberOfCommands/Text").GetComponent<Text>();
            txt.text = num.ToString();
        }


    }
    //Handles Left touch input
    public void LeftTouchInput()
    {
        //If the current input is left
        if (currentCommand < command.Length)
        {
            if (0 == command[currentCommand])
            {
                //move down the list 
                currentCommand++;
                DisplayList();
            }else
            {
                //Else the player loses
                Lose();
            }
        }
    }
    //Handles right touch input
    public void RightTouchInput()
    {
        //If the current input is right
        if (currentCommand < command.Length)
        {
            if (1 == command[currentCommand])
            {
                //Move down the list
                currentCommand++;
                DisplayList();
            }else
            {
                //else the player loses
                Lose();
            }
        }
    }

    

	// Update is called once per frame
	void Update () {
        //Checks if the player is at the end of the list
        //If they are they win the game
        if (command.Length == currentCommand && !playerHasWon)
        {
            Text txt = GameObject.Find("NumberOfCommands/Text").GetComponent<Text>();
            txt.text = 0.ToString();
            WinGame();
            playerHasWon = true;
        }
	}
}
