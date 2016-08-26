using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleCard : MonoBehaviour {

    private bool playerCanInteract;

    void Awake()
    {
        //Set the target framerate to 60fps
        Application.targetFrameRate = 60;
        
    }
    void Start()
    {
        playerCanInteract = true;
        //Slides in title and information
        StartCoroutine(slideDetails(false));
    }

    // Update is called once per frame
    void Update () {
        //If the player taps the screen player will be moved to main menu
        if (Input.GetMouseButtonDown(0) && playerCanInteract)
        {
            playerCanInteract = false;
            StartCoroutine(slideDetails(true));
        }
        //Quits the application
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            print("Quit Coin Pushers");
            Application.Quit();
        }
	}

    //handles sliding of logo and info
    IEnumerator slideDetails(bool slideOut)
    {
        float time = 0;
        float speed = 0;
        float speedUp = 0.005f;
        GameObject instruct = GameObject.Find("Details");
        GameObject title = GameObject.Find("Title/Image");

        Vector3 instructStartPos = new Vector2(instruct.transform.localPosition.x, -Screen.height);
        Vector3 instructEndPos = instruct.transform.localPosition;

        while (time < 1)
        {
            time += speed;
            speed += speedUp;
            if (slideOut)
            {
                title.transform.localPosition = Vector3.Lerp(Vector3.zero, new Vector3(0, Screen.height), time);
                instruct.transform.localPosition = Vector3.Lerp(instructEndPos, instructStartPos, time);
            }else
            {
                title.transform.localPosition = Vector3.Lerp(new Vector3(0, Screen.height), Vector3.zero, time);
                instruct.transform.localPosition = Vector3.Lerp(instructStartPos, instructEndPos, time);
            }
            yield return new WaitForEndOfFrame();
        }
        if (slideOut)
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
