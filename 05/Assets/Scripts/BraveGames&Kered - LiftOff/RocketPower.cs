using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GAF.Core;

public class RocketPower : BG_LiftOff_Menu {
    
    private float power;
    private bool playerHasWon;

    public GAFMovieClip gafMovie;
	// Use this for initialization
	void Start () {
        power = 0;
        playerHasWon = false;
	}
	
	// Update is called once per frame
	void Update () {

        //The power is always decreasing with time
        if (!playerHasWon)
        {
            power -= Time.deltaTime / 4;
        }
        //If the player taps the screen they will gain more power
        if (Input.GetMouseButtonDown(0))
        {
            power += 0.1f;
        }
        //Checks if the power is full
        //If it is the player wins
        DidPlayerWin();

        power = Mathf.Clamp01(power);
        GetComponent<Image>().fillAmount = power;//Displays power on screen
        GameObject.Find("PowerTwo").GetComponent<Image>().fillAmount = power;
	}
    //Checks if the player has won
    private void DidPlayerWin()
    {
        if(power >= 1 && !playerHasWon)
        {
            gafMovie.play();
            playerHasWon = true;
            WinGame();
        }
    }
}
