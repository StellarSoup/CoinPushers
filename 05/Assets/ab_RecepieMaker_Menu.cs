using UnityEngine;
using System.Collections;

public class ab_RecepieMaker_Menu : GameState {

    private MatchTwoPairControls[] mTwoPair;
    private bool playerHasWon;
    // Use this for initialization
    void Start()
    {
        mTwoPair = FindObjectsOfType<MatchTwoPairControls>();
        TimerLose();
        playerHasWon = false;
    }

    void Update()
    {
        //If all the pieces are correct the player wins
        if (CheckAllPiecesAreMatching() && !playerHasWon)
        {
            WinGame();
            print("All pieces are matching");
            playerHasWon = true;
        }
    }
    //Checks if all the pieces are with their correct matches
    bool CheckAllPiecesAreMatching()
    {
        bool allMatches = true;
        for (int i = 0; i < mTwoPair.Length; i++)
        {
            if (!mTwoPair[i].piecesAreMatching)
            {
                allMatches = false;
            }
        }
        return allMatches;
    }
}
