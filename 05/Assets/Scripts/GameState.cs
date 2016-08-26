using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameState : MonoBehaviour {
    /* Hive mind for the entire project
     * - Handles conditions if player wins and loses the game
     * - Loads levels
     * - Sets up instructions
     */

    private string canvasName = "GameCanvas";

    //Sets the game to win when the time reaches 0
    protected void TimerWin()
    {
        GSWinAtEnd(true);
        setUpInstructions();
    }

    //Sets the game to lose when the time reaches 0
    protected void TimerLose()
    {
        GSWinAtEnd(false);
        setUpInstructions();
    }

    //Sets up the instructions for the next game
    private void setUpInstructions()
    {
        GameObject instructionsPanel = GameObject.Find("Instructions");
        //Load in new instructions
        instructionsPanel.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = KeyDirectory.Games.getLevelInstructions();
        //Slide instructions into position
        StartCoroutine(slideDownInstructions(instructionsPanel,0.3f));      
    }
    //Slides instructions into position
    IEnumerator slideDownInstructions(GameObject instructions,float duration)
    {
        RectTransform instructRect = instructions.transform.GetChild(0).GetComponent<RectTransform>();
            float time = 0;
        while (time < 1)
            {
                time += Time.deltaTime/duration;
            instructRect.offsetMin = new Vector2(-50, Mathf.Lerp(Screen.height * 0.1f, 0, time));
            instructRect.offsetMax = new Vector2(50, Mathf.Lerp(Screen.height * 0.1f, 0, time));


            yield return new WaitForEndOfFrame();
            }
    }
    //Slides instructions out of position
    IEnumerator slideUpInstructions(GameObject instructions, float duration)
    {
        RectTransform instructRect = instructions.transform.GetChild(0).GetComponent<RectTransform>();
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime/duration;
            instructRect.offsetMin = new Vector2(-50, Mathf.Lerp(0, Screen.height * 0.1f, time));
            instructRect.offsetMax = new Vector2(50, Mathf.Lerp(0, Screen.height * 0.1f, time));


            yield return new WaitForEndOfFrame();
        }
    }
    
    //Sets if timer is running
    protected void timeRunning(bool state)
    {
        GameObject.Find("Timer&MusicPlayer").SendMessage("runStopWatch", state);
    }
    //Player wins game
    //Note: Premptive win
    protected void Win(){
        //Slide up instructions
        StartCoroutine(slideUpInstructions(GameObject.Find("Instructions"),0.3f));
        //Add a win and load in a new level
        KeyDirectory.Games.Score.AddWin();
        LoadNewLevel(true);
        timeRunning(false);
        GameObject.Find(canvasName).GetComponent<GameStopWatchGUI>().SlideTimer(false, 0.5f);
        //Life was not lost
        KeyDirectory.Lives.wasLifeLost(false);   
	}
    //Sets the timer so when it reaches 0, the player wins
    protected void WinGame()
    {
        SlideInWinBanner();
        GSWinAtEnd(true);
    }

    //Handles sliding in the win banner
    private void SlideInWinBanner()
    {
        YouWinControls winControls = (YouWinControls)FindObjectOfType(typeof(YouWinControls));
        winControls.SlideInBanner();
    }
    //Handles player losing the game
	protected void Lose(){
            print("You Lost");

        //Player loses a life
        PlayerSettings.LoseHealth();
        //If player loses all of there lives
        if (PlayerSettings.CURRENT_HEALTH <= 0)
        {
            //They are taken to the lose screen
            if (KeyDirectory.Mode.Game.Get().Equals(KeyDirectory.Mode.Game.ARCADE))
            {
                LoadNewLevel(false);
            }else
            {
                SceneManager.LoadScene("Title Card");
            }
            GSResetTimer();
        }else
        {
            //If the player still has some lives
            //Keep playing
            StartCoroutine(slideUpInstructions(GameObject.Find("Instructions"),0.5f));
            LoadNewLevel(true);       
            GameObject.Find(canvasName).GetComponent<GameStopWatchGUI>().SlideTimer(false, 0.5f);
        }
        timeRunning(false);
        KeyDirectory.Lives.wasLifeLost(true);


    }

    //Handles loading new level
    void LoadNewLevel(bool newGame)
    {
        //Stops the timer
        timeRunning(true);
        SceneTransitionController sceneController = GameObject.Find("SceneTransitions").GetComponent<SceneTransitionController>();

        //If the game is playing arcade mode
        if (KeyDirectory.Mode.Game.Get() == KeyDirectory.Mode.Game.ARCADE)
        {
            //if a new game is set 
            if (newGame) {
                //Move to mid game
                sceneController.slideTrans("ArcadeMidGame");
            }else{
                //Else move to lose screen
                sceneController.slideTrans("ArcadeLoseScreen");
            }
        }else
        //If the game is playing story mode
        {
            if (KeyDirectory.Mode.Story.Get() == KeyDirectory.Mode.Story.EASY)
            {
                //If the player has enough wins
                if (KeyDirectory.Games.Score.doesPlayerHaveEnoughWins())
                {
                    //Take the player to the end game
                    sceneController.slideTrans("Test-EasyGame");
                }else{
                    //else take the player to the midgame
                    sceneController.slideTrans("EasyMode-PauseGame");
                }
            }
        }

     }

    //Resets the timer
    protected void GSResetTimer()
    {
        GameObject tAndM = GameObject.Find("Timer&MusicPlayer");
        tAndM.GetComponent<GameStopWatch>().resetTimer();
    }
    //Sets if at the end of the timer its a win or lose
    private void GSWinAtEnd(bool state)
    {
        GameObject tAndM = GameObject.Find("Timer&MusicPlayer");
        tAndM.GetComponent<GameStopWatch>().WinAtEnd(state);
    }


}
