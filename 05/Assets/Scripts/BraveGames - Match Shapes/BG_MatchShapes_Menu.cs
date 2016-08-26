using UnityEngine;
using System.Collections;

public class BG_MatchShapes_Menu : GameState {

    private MatchTwoPairControls[] mTwoPair;
    private bool playerHasWon;

	void Start () {
        //Gets all of the available pairs
        mTwoPair = FindObjectsOfType<MatchTwoPairControls>();
        TimerLose();
        playerHasWon = false;
	}

    // Update is called once per frame
    void Update()
    {
        //if all of the pieces are in the right slots then the player wins
        if (CheckAllPiecesAreMatching() && !playerHasWon)
        {
            WinGame();
            print("All pieces are matching");
            playerHasWon = true;
        }
    }

    //Checks if all the pieces are in the slots
    bool CheckAllPiecesAreMatching()
    {
        bool allMatches = true;
        for(int i = 0; i < mTwoPair.Length; i++)
        {
            if (!mTwoPair[i].piecesAreMatching)
            {
                allMatches = false;
            }
        }
        return allMatches;
    }
}
