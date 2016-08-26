using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    private bool playerCanInteract;

    private RectTransform arcadeRect;
    private RectTransform tvRect;

    private GameObject slideShowCanvas;

    private Vector2 startPos = new Vector2(0, Screen.height * 2.2f);

	// Use this for initialization
	void Start () {
        playerCanInteract = true;
        arcadeRect = GameObject.Find("Arcade-TV Side Select Canvas/ArcadeSide").GetComponent<RectTransform>();
        tvRect = GameObject.Find("Arcade-TV Side Select Canvas/TVSide").GetComponent<RectTransform>();

        arcadeRect.offsetMin = arcadeRect.offsetMax = tvRect.offsetMin = tvRect.offsetMax = startPos;

        slideShowCanvas = GameObject.Find("SlideShowCanvas");
        if(slideShowCanvas == null)
        {
            GameObject newSlideshow = Instantiate(Resources.Load("Prefabs/SlideShowCanvas") as GameObject);
            newSlideshow.name = "SlideShowCanvas";
        }

        StartCoroutine(slideInButtons());

    }
    //Loads the gameMode scene
    public void LoadToGameMode()
    {
        if (playerCanInteract)
        {
            print("Loading Game Mode");
            playerCanInteract = false;
            StartCoroutine(slideArcadeTvSides());
        }
    }
    //Loads options
    public void LoadOptions()
    {
        SceneManager.LoadScene("Options");
    }
    //Handles sliding in buttons
    IEnumerator slideInButtons()
    {
        float time = 0;
        float speed = 0;
        float speedUp = 0.0005f;

        GameObject buttons = GameObject.Find("Buttons");

        while(time < 1)
        {
            time += speed;
            speed += speedUp;
            buttons.transform.localPosition = Vector3.Lerp(new Vector3(0, Screen.height*2), Vector3.zero, time);
            yield return new WaitForEndOfFrame();
        }
    }

    //Controls the panel for the game modes
    IEnumerator slideArcadeTvSides()
    {
        float slideTime = 0;
        float speed = 0;
        float upSpeed = 0.005f;

        //Slides it from the bottom of the screen
        while(slideTime < 1)
        {
            slideTime += speed;
            speed += upSpeed;
            arcadeRect.offsetMin = arcadeRect.offsetMax = tvRect.offsetMin = tvRect.offsetMax = Vector2.Lerp(startPos, Vector2.zero,slideTime);
            yield return new WaitForEndOfFrame();
        }
        Destroy(GameObject.Find("SlideShowCanvas"));
        Destroy(GameObject.Find("SlideShowCanvas(Clone)"));
        SceneManager.LoadScene("Game Mode");
        
        Destroy(gameObject);

    }
}
