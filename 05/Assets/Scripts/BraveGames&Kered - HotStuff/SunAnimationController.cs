using UnityEngine;
using System.Collections;
using GAF.Core;


public class SunAnimationController : BGK_HotStuff_Menu {
    /**
     *  KeyFrames
     *  0 - Start Of Animation
     *  153 - Start of win animation
     *  179 - Start of lose animation
     *  
     */
    private GAFMovieClip movie;
    private bool playerHasWon;
    private AudioSource audioPlayer;

	// Use this for initialization
	void Start () {
        //Set that the player has not won and start animation at the start
        playerHasWon = false;
        audioPlayer = GetComponent<AudioSource>();
        movie = GetComponent<GAFMovieClip>();
        movie.gotoAndPlay(1);
	}
	
    //Plays the kiss sound that will indicate that the player has won
    void PlayKissSound()
    {
       AudioClip newClip = (AudioClip)Resources.Load("Sounds/HotStuffSoundEffects/KissedSFX", typeof(AudioClip));
        audioPlayer.clip = newClip;
        audioPlayer.Play();
    }

    public void kissTheSun()
    {
        //If the player taps the screen they will win the game
        if (!playerHasWon && movie.currentFrameNumber >= 70)
        {
            PlayKissSound();
            movie.gotoAndPlay(153);
            WinGame();
            playerHasWon = true;
        }
    }

	// Update is called once per frame
	void Update () {
        
        //Loops on the Sun wearing shades if the player has won
        if (playerHasWon && movie.currentFrameNumber >= 178)
        {
            movie.gotoAndPlay(169);
        }
        //Loops on the sun making the kissing face
        if (!playerHasWon && movie.currentFrameNumber >= 138)
        {
            movie.gotoAndPlay(74);
        }

    }
}
