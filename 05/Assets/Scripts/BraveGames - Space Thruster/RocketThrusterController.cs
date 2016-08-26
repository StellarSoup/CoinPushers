using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RocketThrusterController : BG_SpaceThruster_Menu
{

    private float shipPower;
    private bool isAscending;
    private float energy;

    private float shipSpeed;

    public GameObject ship;
    private bool playerHasWon;
	// Use this for initialization
	void Start () {
        Init();
	}
    private void Init()
    {
        //Set the animation to 0 and initialises starting conditions
        shipSpeed = 0;
        shipPower = 0;
        isAscending = true;
        energy = 0.01f;
        playerHasWon = false;
    }

    //Ignites the ship thrusters and moves the ship off the screen
    private void playAnimation()
    {
        ship.transform.localPosition = new Vector3(ship.transform.localPosition.x - shipSpeed, ship.transform.localPosition.y, ship.transform.localPosition.z);
        shipSpeed += Time.deltaTime;
        ship.transform.GetChild(0).gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        //If the player has won then the player will fly of the screen to the left
        if (playerHasWon)
        {
            playAnimation();
        }

        changeSpeed(); // Controls the fill amount for the bar

        //When the player taps the screen
        if (Input.GetMouseButtonDown(0))
        {
            //The game will check if the user has the right power level
            checkifEnoughPower();
        }
	}

    private void checkifEnoughPower()
    {
        if (!playerHasWon && GameObject.Find("Timer&MusicPlayer").GetComponent<GameStopWatch>().time > 0)
        {
            if (shipPower >= 0.9 && shipPower <= 1)
            {
                playerHasWon = true;
                WinGame();

            }
            else
            {
                Lose();
            }
            energy = 0;
        }
    }

    //Changes the speed of the thruster progress bar
    private void changeSpeed()
    {

        if (isAscending)
        {
            shipPower += energy;
        }
        else
        {
            shipPower -= energy;
        }

        //Limits the time between 0 and 1
        shipPower = Mathf.Clamp01(shipPower);

        //if the time hits the edges then flip the speed and move it in the opposite direction
        if (shipPower >= 1 || shipPower <= 0)
        {
            isAscending = !isAscending;
        }
        transform.GetChild(0).GetChild(1).GetComponent<Image>().fillAmount = shipPower;

    }
}
