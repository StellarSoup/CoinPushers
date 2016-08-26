using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ArcadeLoseScreenSceneController : MonoBehaviour {

    AudioSource audioPlayer;

	// Use this for initialization
	void Start () {
        audioPlayer = GetComponent<AudioSource>();
        //Depending on the star rating a different animation will play
        //When the player loses
        playBitState();
        //Gets the current score
        int currentScore = KeyDirectory.Games.Score.CountWins();
        //Sets it as the highscore if its higher than the current highscore
        KeyDirectory.Games.HighScore.setNewHighScore(currentScore);
        //Displays the current score  on screen
        GameObject.Find("LoseDetails/PlayerScoreDetails/Score/Text").GetComponent<Text>().text = currentScore.ToString();

        //Resets current score
        KeyDirectory.Games.Score.ResetScore();
        //Plays the animation to load in lose details
        StartCoroutine(loadLoseDetails());
	}

    //Play the game over sound
    private void PlayGameOverSound()
    {
        audioPlayer.Play();
    }


    //Controls what coin animation will be playing
    //depending on star rating
    private void playBitState()
    {
        //Gets the animator
        Animator anim = GameObject.Find("PlayerScoreDetails/Bit-Image").GetComponent<Animator>();
        //Get the star rating
        int rating = KeyDirectory.StarRanking.getStarRating();
        if (rating == 5)
        {
            //Display the coin spinning
            anim.Play("Bit-Spinning", 0);
        }
        else if (rating == 4)
        {
            //Display the system with a shocked face
            anim.Play("Bit-Shock", 0);
        }
        else if (rating == 3)
        {
            //Display the coin smiling
            anim.Play("Bit-Idle", 0);
        }
        else if (rating == 2)
        {
            //Display the coin in a blank expression
            anim.Play("Bit-Blank", 0);
        }
        else
        {
            //Display the coin laughing
            anim.Play("Bit-Laughing", 0);
        }
    }
    //Load in all of the necessary lose details
    IEnumerator loadLoseDetails()
    {
        GameObject gameOverTitle = GameObject.Find("GAME OVER");
        GameObject arcadeCamera = GameObject.Find("GameCamera");
        Rect startCamPos = new Rect(Vector2.zero, Vector2.one);
        Rect endCamPos = arcadeCamera.GetComponent<Camera>().rect;

        //Get the exterior canvas'
        RectTransform arcadeControls = GameObject.Find("AracdeControlsOverView").GetComponent<RectTransform>();
        RectTransform outsideCanvas = GameObject.Find("Arcade Cabinent Canvas/Center").GetComponent<RectTransform>();
        Vector3 outsideCanvScale = outsideCanvas.localScale;
        //Start of the exterior canvas' in an expanded state
        arcadeControls.localScale = Vector3.one * 3;
        arcadeCamera.GetComponent<Camera>().rect = startCamPos;
        outsideCanvas.localScale = new Vector3(1.65f, 1.65f, 1.65f);

        //Have the game over sound play twice and flash the game over text
        float iterations = 0;
        while(iterations < 5)
        {
            if(iterations%2 == 1)
            {
                PlayGameOverSound();
            }
            gameOverTitle.SetActive(!gameOverTitle.activeSelf);
            iterations++;
            yield return new WaitForSeconds(1f);
        }
        float time = 0;
        float change = 0;
        while(time < 1)
        {
            time += change;
            change += Time.deltaTime;
            arcadeCamera.GetComponent<Camera>().rect = new Rect(Vector2.Lerp(startCamPos.position, endCamPos.position, time), Vector2.Lerp(startCamPos.size, endCamPos.size, time));
            outsideCanvas.localScale = Vector3.Lerp(new Vector3(1.65f, 1.65f, 1.65f), outsideCanvScale, time);
            arcadeControls.localScale = Vector3.Lerp(Vector3.one*3, Vector3.one, time);

            yield return new WaitForEndOfFrame();
        }
        RectTransform playerScore = GameObject.Find("PlayerScoreDetails").GetComponent<RectTransform>();
        RectTransform buttons = GameObject.Find("Buttons").GetComponent<RectTransform>();

        //Slide in the player score and the buttons to navigate to other scenes

        time = 0;
        while(time < 1)
        {
            time += Time.deltaTime;
            playerScore.offsetMin = Vector2.Lerp(new Vector2(-Screen.width / 2, 0), Vector2.zero, time);
            playerScore.offsetMax = Vector2.Lerp(new Vector2(-Screen.width / 2, 0), Vector2.zero, time);

            buttons.offsetMin = Vector2.Lerp(new Vector2(Screen.width / 2, 0), Vector2.zero, time);
            buttons.offsetMax = Vector2.Lerp(new Vector2(Screen.width / 2, 0), Vector2.zero, time);

            yield return new WaitForEndOfFrame();
        }
    }

    //Load in a new arcade game
    public void LoadNewGame()
    {
        StartCoroutine(loadNewGame());
    }
    //Handles loading back to arcade mode
    IEnumerator loadNewGame()
    {
        RectTransform playerScore = GameObject.Find("PlayerScoreDetails").GetComponent<RectTransform>();
        RectTransform buttons = GameObject.Find("Buttons").GetComponent<RectTransform>();

        //Slides the buttons and score back
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime;
            playerScore.offsetMin = Vector2.Lerp(Vector2.zero, new Vector2(-Screen.width / 2, 0), time);
            playerScore.offsetMax = Vector2.Lerp(Vector2.zero, new Vector2(-Screen.width / 2, 0), time);

            buttons.offsetMin = Vector2.Lerp(Vector2.zero, new Vector2(Screen.width / 2, 0), time);
            buttons.offsetMax = Vector2.Lerp(Vector2.zero, new Vector2(Screen.width / 2, 0), time);

            yield return new WaitForEndOfFrame();
        }
        //Loads back to the start of arcade mode
        SceneManager.LoadScene("StartNewArcadeGame");

    }
    //Loads back to main menu
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
