using UnityEngine;
using System.Collections;

public class GameStopWatch : GameState {

    /*Handles the time for each minigame*/

    public float time;
    private bool winAtEnd;
    private bool isRunning = false;

    private MusicPlayer gameMusic;

    private float duration = 0.5f;

    void Start()
    {
        gameMusic = GetComponent<MusicPlayer>();
        //Starts playing the game music
        gameMusic.PlayMusic();
        time = KeyDirectory.Timer.getNewTimer();
        DontDestroyOnLoad(gameObject);
    }

    //Handles pausing and unpausing of stopwatch
    public void runStopWatch(bool state)
    {
        if (state)
        {
            gameMusic.ChangeVolume(duration, 0.5f);
        }

        isRunning = state;
    }

    //Set weather the player wins at the end or not
    public void WinAtEnd(bool state)
    {
        winAtEnd = state;
    }

    void Update()
    {

        if (isRunning)
        {
            time -= Time.deltaTime;
        }
        TimesUp();
    }
    
    //When the player runs out of time
    //They eitehr win or lose
    void TimesUp()
    {
        if (time <= 0)
        {
            if (winAtEnd)
            {
                Win();
            }else if(!winAtEnd)
            {
                Lose();
            }           
            resetTimer();
            gameMusic.ChangeVolume(duration, 1);
        }
        
    }
    //Resets the timer to full
    public void resetTimer()
    {
        time = KeyDirectory.Timer.getNewTimer();
    }
}
