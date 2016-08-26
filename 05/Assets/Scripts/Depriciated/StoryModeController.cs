using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StoryModeController : MonoBehaviour {

    RectTransform storyFontLogo;
    RectTransform buttons;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        buttons = transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
        storyFontLogo = transform.GetChild(0).GetChild(1).GetComponent<RectTransform>();

        GameObject font = storyFontLogo.transform.GetChild(0).gameObject;
        font.transform.SetParent(storyFontLogo.transform.parent);

        StartCoroutine(slideLogoAndFontIntoPositon(true));
	}

    IEnumerator slideOutAndDestroy()
    {
        StartCoroutine(slideLogoAndFontIntoPositon(false));
        yield return new WaitForSeconds(1f);
        StartCoroutine(slideUpTVside());
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
    IEnumerator slideUpTVside()
    {
        float slideTime = 0;
        float speed = 0;
        float speedUp = 0.005f;

        RectTransform tvSide = storyFontLogo.transform.parent.GetComponent<RectTransform>();
        

        while(slideTime < 1)
        {
            slideTime += speed;
            speed += speedUp;
            tvSide.offsetMin = tvSide.offsetMax = Vector2.Lerp(Vector2.zero, new Vector2(0, Screen.height*2.5f),slideTime);
            yield return new WaitForEndOfFrame();
        }

    }

    public void LoadNewGame()
    {
        StartCoroutine(slideOutAndDestroy());
    }

    public void MainMenu()
    {
        StartCoroutine(slideOutAndDestroy());
        SceneManager.LoadScene("Main Menu");
    }
	
    IEnumerator slideLogoAndFontIntoPositon(bool state)
    {
        yield return new WaitForSeconds(0);
        float slideTime = 0;
        float speed = 0;
        float speedUp = 0.005f;

        buttons.offsetMin = buttons.offsetMax = new Vector2(Screen.width * 2, 0);

        

        Vector2 startPos = new Vector2(Screen.width * 2, 0);

        while (slideTime < 1)
        {
            slideTime += speed;
            speed += speedUp;
            if (state)
            {
                buttons.offsetMin = buttons.offsetMax = Vector2.Lerp(startPos, Vector2.zero, slideTime);
                storyFontLogo.offsetMin = storyFontLogo.offsetMax = Vector2.Lerp(Vector2.zero, -startPos, slideTime);
            }else
            {
                buttons.offsetMin = buttons.offsetMax = Vector2.Lerp(Vector2.zero, startPos, slideTime);
                storyFontLogo.offsetMin = storyFontLogo.offsetMax = Vector2.Lerp(-startPos,Vector2.zero, slideTime);
            }

            yield return new WaitForEndOfFrame();
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
