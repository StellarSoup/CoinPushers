using UnityEngine;
using System.Collections;

public class ButtonPresserControls : BG_ButtonPresser_Menu
{

	private GameObject instructions;

	private int ranNum;
    private bool hasPlayerWon;

	// Use this for initialization
	void Start () {
		instructions = gameObject;
		gameObject.name = "Instructions";

		SetNewText ();
        hasPlayerWon = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) && ranNum == 0) {
            if (!hasPlayerWon)
            {
                hasPlayerWon = true;
                Win();
            }
		}
        if(Input.GetMouseButtonDown(0) && ranNum == 1)
        {
            Lose();
        }
	}

	void SetNewText(){
		ranNum = (int)(Random.value * 2);

		TextMesh txtM = instructions.GetComponent<TextMesh> ();
		if (ranNum == 0) {
			txtM.text = "Tap Here";
            TimerLose();
		} else {
			txtM.text = "Don't Tap Here";
            TimerWin();
		}
	}

}
