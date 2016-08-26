using UnityEngine;
using System.Collections;

public class ButtonActions : MonoBehaviour {

    BG_ButtonPresser_Menu buttonPresser;
    private GameObject button;
    
    	// Use this for initialization
	void Start () {
        buttonPresser = GameObject.Find("Game3").GetComponent<BG_ButtonPresser_Menu>();
        button = gameObject;
	}

    //Handles the visuals
    void pushButtonDown(bool buttonIsPushed)
    {
        
        Vector3 buttonPos = button.transform.localPosition;
        //If the button is pressed
        if (buttonIsPushed)
        {
            //Move it down
            button.transform.localPosition = new Vector3(buttonPos.x, -1, buttonPos.z);
        }
        //else
        else
        {
            //keep it up
            button.transform.localPosition = new Vector3(buttonPos.x, 1, buttonPos.z);
        }
    }

    void OnMouseDown()
    {
        pushButtonDown(true);
        if (buttonPresser.playerNeedsToPressTheButton)
        {
            if (!buttonPresser.playerHasWon)
            {
                buttonPresser.winButtonGame();
                buttonPresser.playerHasWon = true;
            }
        }else
        {
            buttonPresser.loseButtonGame();
        }
    }
    void OnMouseUp()
    {
        pushButtonDown(false);
    }
}
