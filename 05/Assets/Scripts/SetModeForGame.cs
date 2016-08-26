using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SetModeForGame : MonoBehaviour {

    public string selectMode;
    public string storyDifficulty;



	// Use this for initialization
	void Start () {
        if(selectMode == null)
        {
            selectMode = "None";
        }
	    if(storyDifficulty == null)
        {
            storyDifficulty = "None";
        }
	}
    //Sets the game and story mode
    public void SetModes()
    {
        SetGameMode(selectMode);
        
    }
    //Sets the game modes
    private void SetGameMode(string mode)
    {
        if (mode.Equals(KeyDirectory.Mode.Game.ARCADE))
        {
            KeyDirectory.Mode.Game.Set(KeyDirectory.Mode.Game.ARCADE);

            SceneManager.LoadScene("StartNewArcadeGame");
            


        }else if (mode.Equals(KeyDirectory.Mode.Game.STORY))
        {
            KeyDirectory.Mode.Game.Set(KeyDirectory.Mode.Game.STORY);
            SetStoryModeDifficulty(storyDifficulty);
        }else
        {
            print("Invalid game mode selection");
            KeyDirectory.Mode.Game.Set("None");
        }
        print("Mode set to " + KeyDirectory.Mode.Game.Get());
    }
    //Sets story difficulty
    private void SetStoryModeDifficulty(string level)
    {
        StoryModeController storyController = GameObject.Find("TVSIDE").GetComponent<StoryModeController>();

        if (level.Equals(KeyDirectory.Mode.Story.EASY))
        {
            KeyDirectory.Mode.Story.Set(level);
            SceneManager.LoadScene("StartNewEasyGame");
            storyController.LoadNewGame();

        } else if (level.Equals(KeyDirectory.Mode.Story.NORMAL))
        {
            KeyDirectory.Mode.Story.Set(level);
        } else if (level.Equals(KeyDirectory.Mode.Story.HARD))
        {
            KeyDirectory.Mode.Story.Set(level);
        }
        else
        {
            print("Invalid level selection");
            KeyDirectory.Mode.Story.Set("None");
        }
        print("Story Difficulty set to " + KeyDirectory.Mode.Story.Get());
    }
}
