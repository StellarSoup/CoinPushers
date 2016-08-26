using UnityEngine;
using System.Collections;

public class ab_AddAttacker_Menu : GameState {

    //Keeps count of how many adds are on screen
    protected static int numberOfAds;

    private bool playerHasWon;

	// Use this for initialization
	void Start () {
        playerHasWon = false;
        numberOfAds = 0;
        //At the start of the game create some ads
        CreateAds(4);
        TimerLose();
	}

    //If the player has no more adds on scree then they win
    protected void checkIfthereAreNoMoreAds()
    {
        if (numberOfAds == 0 && !playerHasWon)
        {
            playerHasWon = true;
            WinGame();
        }
    }

    //Creates a series of ads
    void CreateAds(int numberOfAds)
    {
        for(int i = 0; i < numberOfAds; i++)
        {
            int numberOfAvailableAds = 14;
            string newAdIndex = ((int)(Random.value * numberOfAvailableAds) + 1).ToString();
            //Loads in a new add from a selection of prefabs
            GameObject newAd = Instantiate(Resources.Load("16-AddAttacker-Resources/Prefabs/" + newAdIndex, typeof(GameObject))) as GameObject;
            //Randomly places it on the screen
            newAd.transform.SetParent(transform);
            newAd.transform.localScale = new Vector3(0.6f, 0.6f, 1);
            newAd.transform.GetChild(0).localPosition = Vector3.zero;
            newAd.transform.localPosition = new Vector2(Random.Range(-250, 250), Random.Range(-100,0));
        }
    }
}
