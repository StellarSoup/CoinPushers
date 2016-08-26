using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ArcadeModeSelectController : MonoBehaviour {

    private GameObject arcade;
    private GameObject buttons;


    Vector2 startPos = new Vector2(-(Screen.width +100f), 0);
    Vector2 endPos = Vector2.zero;

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(gameObject);
        //Gets the Logo and Font Child
        arcade = transform.GetChild(0).GetChild(0).gameObject;
        buttons = transform.GetChild(1).gameObject;

        //
        if (SceneManager.GetActiveScene().name.Equals("ArcadeMode"))
        {
            GameObject.Find("MenuController").GetComponent<SetModeForGame>().SetModes();
            slideOutAndDestroy();
        }else if (SceneManager.GetActiveScene().name.Equals("ArcadeLoseScreen"))
        {
            RectTransform arcadeSideRect = transform.GetChild(0).GetComponent<RectTransform>();
            arcadeSideRect.transform.localPosition = new Vector2(0, Screen.height * 2.21f);
        }
	}
    //Move to main menu
    public void mainMenu()
    {
        slideOutAndDestroy();
        SceneManager.LoadScene("Main Menu");
       
    }

    //Slideout arcade panel
    public void slideOutAndDestroy()
    {
        StartCoroutine(SlideOutArcade());
    }
    //Slides out arcade panel and destorys it
    IEnumerator SlideOutArcade()
    {
        RectTransform arcadeSideRect = transform.GetChild(0).GetComponent<RectTransform>();
        float slideTime = 0;
        float speed = 0;
        float speedUp = 0.003f;

        while(slideTime < 1)
        {
            slideTime += speed;
            speed += speedUp;
            arcadeSideRect.transform.localPosition = Vector2.Lerp(Vector2.zero, new Vector2(0, Screen.height*2.2f), slideTime);
            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }

    //Slides the buttons across from the left
    IEnumerator moveAllButtonsIn(bool slideIn,float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        for (int i = 0; i < buttons.transform.childCount; i++)
        {
            GameObject singleButton = buttons.transform.GetChild(i).gameObject;
            //Slides the button in
            if (slideIn)
            {
                StartCoroutine(MoveButtonIn(singleButton));
            }else
            //Slides the button out
            {
                StartCoroutine(MoveButtonOut(singleButton));
            }
            yield return new WaitForSeconds(0.1f);

        }
        if (!slideIn)
        {
            yield return new WaitForSeconds(1);
            StartCoroutine(SlideOutArcade());
        }
    }

    //Handles sliding buttons into position
    IEnumerator MoveButtonIn(GameObject button)
    {
        RectTransform buttonRect = button.GetComponent<RectTransform>();

        float slideTime = 0;
        float speed = 0;
        float speedIncrease = 0.003f;
        while (slideTime < 1)
        {
            slideTime += speed;
            speed += speedIncrease;
                buttonRect.localPosition = new Vector2(Mathf.Lerp(startPos.x, endPos.x, slideTime), buttonRect.transform.localPosition.y);

            yield return new WaitForEndOfFrame();
        }
    }
    //Handles sliding buttons out of position
    IEnumerator MoveButtonOut(GameObject button)
    {
        RectTransform buttonRect = button.GetComponent<RectTransform>();



        float slideTime = 0;
        float speed = 0;
        float speedIncrease = 0.003f;
        while (slideTime < 1)
        {
            slideTime += speed;
            speed += speedIncrease;
            buttonRect.localPosition = new Vector2(Mathf.Lerp(endPos.x, startPos.x, slideTime), buttonRect.transform.localPosition.y);


            yield return new WaitForEndOfFrame();
        }
    }

    //Handles arcade logo movement
    IEnumerator MoveArcadeIntoPosition(bool slideIn)
    {
        float slideTime = 0;
        float speed = 0;
        float speedUp = 0.005f;
        yield return new WaitForSeconds(0.5f);
        while(slideTime < 1)
        {
            slideTime += speed;
            speed += speedUp;
            if (slideIn)
            {
                arcade.GetComponent<RectTransform>().anchorMin = new Vector2(Mathf.Lerp(0, 0.5f, slideTime), 0);
            }else
            {
                arcade.GetComponent<RectTransform>().anchorMin = new Vector2(Mathf.Lerp(0.5f, 0, slideTime), 0);
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
