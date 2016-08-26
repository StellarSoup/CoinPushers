using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

    /*Handles the music player*/

    //Master Volume
    public float INIT_MASTER_VOLUME;
    //SFX volume
    public float INIT_SFX_VOLUME;
    //Game volume
    public float INIT_MUSICVOLUME;
    public float CUR_MUSICVOLUME;

    public AudioSource musicPlayer;

    //Set the playerVolume and beginPlaying music on loop
    public void PlayMusic()
    {        
        musicPlayer.volume = CUR_MUSICVOLUME = Mathf.Clamp01(INIT_MASTER_VOLUME * INIT_MUSICVOLUME);
        musicPlayer.loop = true;
        musicPlayer.Play();
        
    }
    //Slows down the music and destroys the music player
    public void slowDownAndDestroy()
    {
        StartCoroutine(DestroyMusicPlayer());
    }

    //Destroys the music player
    IEnumerator DestroyMusicPlayer()
    {
        float duration = 1;
        //Fades out the pitch
        ChangePitch(duration, 0);
        yield return new WaitForSeconds(duration);
        //Destroys the music player
        Destroy(gameObject);
    }
    //Increases the pitch
    public void SpeedIncrease()
    {
        ChangePitch(1, musicPlayer.pitch + 0.025f);
    }
    //Fades the music to the new volume given
    IEnumerator MusicFadeChange(float time,float newLevel)
    {
        float startingVolume = CUR_MUSICVOLUME;
        float fadeTime = 0;

        while(fadeTime < time)
        {
            fadeTime += Time.deltaTime;

            musicPlayer.volume = CUR_MUSICVOLUME = Mathf.Lerp(startingVolume, newLevel, fadeTime/time);
            
            yield return new WaitForEndOfFrame();         
        }  
    }
    //Fades the pitch to the new pitch given
    IEnumerator PitchFadeChange(float time, float newPitch)
    {
        float startingPitch = musicPlayer.pitch;
        float fadeTime = 0;

        while (fadeTime < time)
        {
            fadeTime += Time.deltaTime;

            musicPlayer.pitch = Mathf.Lerp(startingPitch, newPitch, fadeTime/time);

            yield return new WaitForEndOfFrame();
        }
    }

    //Changes the volume over the duration and volume given
    public void ChangeVolume(float duration, float volume)
    {
        StartCoroutine(MusicFadeChange(duration, volume));
    }
    //Changes the pitch over the duration and pitch given
    private void ChangePitch(float duration,float newPitch)
    {
        StartCoroutine(PitchFadeChange(duration, newPitch));
    }

    

}
