using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectGameModeController : MonoBehaviour {
    public GameObject tvSide;
    private RectTransform tvRect;
    public GameObject arcadeSide;
    private RectTransform arcadeRect;
    private bool currentlySliding;

    private RectTransform arcadeFont;
    private RectTransform tvFont;

    private RectTransform arcadeLogo;
    private RectTransform tvLogo;

    public int modeSelected;
    private readonly int INACTIVE = -1;
    private readonly int ARCADE = 0;
    private readonly int STORY = 1;

    private float fontScale = 1.5f;
    private float logoScale = 1.2f;



    /*Handles game mode selection*/
	void Start () {


        modeSelected = INACTIVE;
        currentlySliding = false;

        tvRect = tvSide.GetComponent<RectTransform>();
        arcadeRect = arcadeSide.GetComponent<RectTransform>();

        tvFont = GameObject.Find(tvSide.name + "/Font").GetComponent<RectTransform>();
        arcadeFont = GameObject.Find(arcadeSide.name + "/Font").GetComponent<RectTransform>();

        tvLogo = GameObject.Find(tvSide.name + "/Logo").GetComponent<RectTransform>();
        arcadeLogo = GameObject.Find(arcadeSide.name + "/Logo").GetComponent<RectTransform>();


    }

    //Lights up the panel the player has selected
    IEnumerator lightUpSelection(Image objectRect)
    {

        float slideTime = 0;
        float speed = Time.deltaTime*2;
        Color startingColor = objectRect.color;
        while (slideTime < 1)
        {
            slideTime += speed;
            objectRect.color = Color.Lerp(Color.white, startingColor, slideTime);
            yield return new WaitForEndOfFrame();
        }
    }

    bool onStartingPos = true;
    //Handles the sliding of panels
    public void slideToMode(string mode)
    {
        if (!currentlySliding)
        {
            
            currentlySliding = true;

            if (onStartingPos)
            {
                StartCoroutine(slideToFirstMode(mode));
            }
            else
            {
                bool willNewSceneBeLoaded = false;

                if (mode.Equals("TV"))
                {
                    if (modeSelected == STORY)
                    {
                        StartCoroutine(loadNewScene(mode));
                        willNewSceneBeLoaded = true;
                    }
                }else
                {
                    if(modeSelected == ARCADE)
                    {
                        StartCoroutine(loadNewScene(mode));
                        willNewSceneBeLoaded = true;
                    }
                }
                if (!willNewSceneBeLoaded)
                {
                    StartCoroutine(slideToCurrentGameMode(mode));
                }
            }
        }
        
    }
    //Loads to the new scene
    IEnumerator loadNewScene(string mode)
    {
        float slideTime = 0;
        float speed = 0;
        float speedIncrease = 0.003f;

        


        while(slideTime < 1)
        {
            slideTime += speed;
            speed += speedIncrease;

            if (mode.Equals("TV"))
            {
                tvRect.anchorMin = Vector2.Lerp(new Vector2(0.1f, 0), new Vector2(0, 0), slideTime);
                arcadeRect.anchorMax = Vector2.Lerp(new Vector2(0.1f, 1), new Vector2(0, 1), slideTime);
            }
            else
            {
                tvRect.anchorMin = Vector2.Lerp(new Vector2(0.9f, 0), new Vector2(1, 0), slideTime);
                arcadeRect.anchorMax = Vector2.Lerp(new Vector2(0.9f, 1), new Vector2(1, 1), slideTime);
            }


            yield return new WaitForEndOfFrame();
        }
        if (mode.Equals("TV"))
        {
            SceneManager.LoadScene("StoryMode");
        }
        else
        {
            SceneManager.LoadScene("ArcadeMode");
        }
    }



    Vector2 newFontPos = new Vector2(400, 50);
    //Handles the sliding for the first selection
    IEnumerator slideToFirstMode(string mode)
    {
        float slideTime = 0;
        float speed = 0;
        float speedIncrease = 0.006f;

        Vector2 arcadeFontPos = arcadeFont.transform.localPosition;
        Vector2 tvFontPos = tvFont.transform.localPosition;

        float center = 0.5f;

        if (mode.Equals("TV"))
        {
            StartCoroutine(lightUpSelection(tvSide.GetComponent<Image>()));
        }else
        {
            StartCoroutine(lightUpSelection(arcadeSide.GetComponent<Image>()));
        }



        while (slideTime < 1)
        {
            slideTime += speed;
            speed += speedIncrease;
            if (mode.Equals("TV"))
            {
                modeSelected = STORY;
                
                tvRect.anchorMin = Vector2.Lerp(new Vector2(center, 0), new Vector2(0.1f, 0), slideTime);
                arcadeRect.anchorMax = Vector2.Lerp(new Vector2(center, 1), new Vector2(0.1f, 1), slideTime);

                tvFont.transform.localPosition = Vector2.Lerp(tvFontPos, tvFontPos + new Vector2(0, newFontPos.y), slideTime);
                arcadeFont.transform.localPosition = Vector2.Lerp(arcadeFontPos, arcadeFontPos - new Vector2(newFontPos.x,0), slideTime);


                tvFont.localScale = Vector2.Lerp(Vector2.one, Vector2.one * fontScale, slideTime);
                tvLogo.localScale = Vector2.Lerp(Vector2.one, Vector2.one * logoScale, slideTime);
            }
            else
            {
                modeSelected = ARCADE;
                tvRect.anchorMin = Vector2.Lerp(new Vector2(center, 0), new Vector2(0.9f, 0), slideTime);
                arcadeRect.anchorMax = Vector2.Lerp(new Vector2(center, 1), new Vector2(0.9f, 1), slideTime);

                arcadeFont.transform.localPosition = Vector2.Lerp(arcadeFontPos, arcadeFontPos + new Vector2(0, newFontPos.y), slideTime);
                tvFont.transform.localPosition = Vector2.Lerp(tvFontPos, tvFontPos + new Vector2(newFontPos.x, 0), slideTime);


                arcadeFont.localScale = Vector2.Lerp(Vector2.one, Vector2.one * fontScale, slideTime);
                arcadeLogo.localScale = Vector2.Lerp(Vector2.one, Vector2.one * logoScale, slideTime);



            }
            yield return new WaitForEndOfFrame();
        }
        onStartingPos = false;
        currentlySliding = false;
    }

    //Slides to gamemode slected
    IEnumerator slideToCurrentGameMode(string mode)
    {
        Vector2 arcadeFontPos = arcadeFont.transform.localPosition;
        Vector2 tvFontPos = tvFont.transform.localPosition;

        float slideTime = 0;
        float speed = 0;
        float speedIncrease = 0.003f;


        if (mode.Equals("TV"))
        {
            StartCoroutine(lightUpSelection(tvSide.GetComponent<Image>()));
        }
        else
        {
            StartCoroutine(lightUpSelection(arcadeSide.GetComponent<Image>()));
        }


        while (slideTime < 1)
        {
            slideTime += speed;
            speed += speedIncrease;
            if (mode.Equals("TV"))
            {
                modeSelected = STORY;
               
                tvRect.anchorMin = Vector2.Lerp(new Vector2(0.9f, 0), new Vector2(0.1f, 0), slideTime);
                arcadeRect.anchorMax = Vector2.Lerp(new Vector2(0.9f, 1), new Vector2(0.1f, 1), slideTime);


                tvFont.localScale = Vector2.Lerp(Vector2.one, Vector2.one * fontScale, slideTime);
                tvLogo.localScale = Vector2.Lerp(Vector2.one, Vector2.one * logoScale, slideTime);


                tvFont.transform.localPosition = Vector2.Lerp(tvFontPos, tvFontPos + new Vector2(-newFontPos.x, newFontPos.y), slideTime);
                arcadeFont.transform.localPosition = Vector2.Lerp(arcadeFontPos, arcadeFontPos - new Vector2(newFontPos.x, newFontPos.y), slideTime);

                arcadeFont.localScale = Vector2.Lerp(Vector2.one* fontScale, Vector2.one, slideTime);
                arcadeLogo.localScale = Vector2.Lerp(Vector2.one * logoScale, Vector2.one, slideTime);


            }
            else
            {
                modeSelected = ARCADE;

                tvRect.anchorMin = Vector2.Lerp(new Vector2(0.1f, 0), new Vector2(0.9f, 0), slideTime);
                arcadeRect.anchorMax = Vector2.Lerp(new Vector2(0.1f, 1), new Vector2(0.9f, 1), slideTime);

                tvFont.localScale = Vector2.Lerp(Vector2.one* fontScale, Vector2.one, slideTime);
                tvLogo.localScale = Vector2.Lerp(Vector2.one* logoScale, Vector2.one, slideTime);


                tvFont.transform.localPosition = Vector2.Lerp(tvFontPos, tvFontPos + new Vector2(newFontPos.x, -newFontPos.y), slideTime);
                arcadeFont.transform.localPosition = Vector2.Lerp(arcadeFontPos, arcadeFontPos + new Vector2(newFontPos.x, newFontPos.y), slideTime);

                arcadeFont.localScale = Vector2.Lerp(Vector2.one, Vector2.one * fontScale, slideTime);
                arcadeLogo.localScale = Vector2.Lerp(Vector2.one, Vector2.one * logoScale, slideTime);

            }
            yield return new WaitForEndOfFrame();

        }
        currentlySliding = false;

        
    }
}
