using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameStopWatchGUI : MonoBehaviour {

    /*Handles the timer being displayed at the bottom of the minigames*/

    //Get the game stop watch
    private GameStopWatch gsWatch;
    //Loading bar aesthetics
    public GameObject LoadingBar;
    private Image LoadBarImage;
    private GameObject radTimer;
    private RectTransform radTimerRect;
    private Vector2 radTimerDimensions;

	// Use this for initialization
	void Start () {
        //Get the initial conditions for the radical timer and move it off screen
        gsWatch = GameObject.Find("Timer&MusicPlayer").GetComponent<GameStopWatch>();
        LoadBarImage = LoadingBar.GetComponent<Image>();
        radTimer = transform.GetChild(0).gameObject;
        radTimerRect = radTimer.GetComponent<RectTransform>();
        radTimerDimensions = (new Vector2(Screen.width,Screen.height) * 0.1f);
        radTimerRect.localPosition = new Vector2(radTimer.transform.localPosition.x, -radTimerDimensions.y - Screen.height / 2);
    }
	//Slide the GUI timer 
    public void SlideTimer(bool state,float duration)
    {
        StartCoroutine(slideTimer(state, duration));
    }

	// Update is called once per frame
	void Update () {
        //Don't dostroy the timer when loading different minigames
        DontDestroyOnLoad(gameObject);
        displayBarProgress();
    }
    //Converts from 255 values to decimal values
    private Color rgbToFloat(float r,float g, float b)
    {
        float convertToDec = 255;

        float newR = r / convertToDec;
        float newG = g / convertToDec;
        float newB = b / convertToDec;

        Color newColor = new Color(newR,newG,newB);
        return newColor;
    }
    //Handles how much of the bar is filled
    void displayBarProgress()
    {
        Color startCol = rgbToFloat(62,35,206);
        Color endCol = Color.red;
        //As time runs out the loading bar changes to red
        LoadBarImage.color = Color.Lerp(endCol, startCol, gsWatch.time / KeyDirectory.Timer.getNewTimer());
        //And the bar emptys
        LoadBarImage.fillAmount = gsWatch.time / KeyDirectory.Timer.getNewTimer();
    }
    
    //Handles sliding of timer into and out of position
    IEnumerator slideTimer(bool show,float duration)
    {
        Vector2 diffPos = new Vector2(Screen.width, Screen.height) * 0f;
        Vector2 startPos = new Vector2(radTimer.transform.localPosition.x, -(radTimerDimensions.y+diffPos.y) - Screen.height/2);
        Vector2 endPos = new Vector2(startPos.x, (radTimerDimensions.y+diffPos.y) - Screen.height*0.575f);
        radTimer.GetComponent<RectTransform>().localPosition = startPos;
        float time = 0;
        while(time < 1)
        {
            if (show)
            {
                radTimer.GetComponent<RectTransform>().localPosition = Vector2.Lerp(startPos, endPos, time);
            }else
            {
                radTimer.GetComponent<RectTransform>().localPosition = Vector2.Lerp(endPos, startPos, time);
            }
            time += Time.deltaTime/duration;
            yield return new WaitForEndOfFrame();
        }
    }
}
