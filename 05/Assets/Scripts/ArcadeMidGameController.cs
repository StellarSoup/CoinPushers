using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ArcadeMidGameController : MonoBehaviour {

    public GameObject canvas;

    private GameObject infoPanel;
    private GameObject speedUp;
    private GameObject buttons;
    private RectTransform speedText;

    // Use this for initialization
    void Start()
    {
        infoPanel = GameObject.Find(canvas.name + "/Information");
        buttons = GameObject.Find(canvas.name + "/Buttons");
        speedUp = GameObject.Find(infoPanel.name + "/Speed");
        speedText = GameObject.Find(speedUp.name + "/Text").GetComponent<RectTransform>();

        //Starts beating heart animation
        StartCoroutine(beatingHeart());

        infoPanel.transform.localPosition = infoPanel.transform.localPosition + new Vector3(0, Screen.height * 2, 0);
        buttons.transform.localPosition = buttons.transform.localPosition - new Vector3(0, Screen.height, 0);
        StartCoroutine(slideInFormation());

        //Displays current score
        ChangeText(GameObject.Find(infoPanel.name + "/Score/Text"), KeyDirectory.Games.Score.CountWins().ToString());

        
    }
    //Changes the text of a text obects
    private void ChangeText(GameObject textObject, string newText)
    {
        textObject.GetComponent<Text>().text = newText;
    }

    void Update()
    {
        //Displays how much longer the player has mid game
       ChangeText(GameObject.Find("Timer/Text"), "Next Game in " + GameDetails.timeLeft);
    }

    //Creates a beating heart effect
    IEnumerator beatingHeart()
    {
        GameObject heartImage = GameObject.Find("Heart/Image");

        
        while (true)
        {
            float time = 0;
            while (time < 1)
            {
                time += Time.deltaTime;
                heartImage.transform.localScale = Vector3.Lerp(Vector3.one * 1.2f, Vector3.one, time);
                yield return new WaitForEndOfFrame();
            }
        }
    }

    //Slide mid game information into place
    IEnumerator slideInFormation()
    {
        Vector3 startInfoPos = infoPanel.transform.localPosition;
        Vector3 endInfoPos = startInfoPos - new Vector3(0, Screen.height * 2, 0);
        Vector3 startButtonPos = buttons.transform.localPosition;
        Vector3 endButtonPos = startButtonPos + new Vector3(0, Screen.height, 0);

        float slideTime = 0;
        while (slideTime < 1)
        {
            slideTime += Time.deltaTime;
            infoPanel.transform.localPosition = Vector3.Lerp(startInfoPos, endInfoPos, slideTime);
            buttons.transform.localPosition = Vector3.Lerp(startButtonPos, endButtonPos, slideTime);
            yield return new WaitForEndOfFrame();
        }
        if (KeyDirectory.Timer.isTimerSpeedingUp())
        {
            StartCoroutine(displaySpeedUp());
        }else
        {
            GameDetails.setPaused();
        }

    }

    //When the game starts to speed up an animation will play
    IEnumerator displaySpeedUp()
    {
        float speed = Time.deltaTime*2;
        RectTransform speedUpRect = speedUp.GetComponent<RectTransform>();
        Image darkenerImage = GameObject.Find("Darkener").GetComponent<Image>();
        //--------------------------Slide to center and Enlarge
        float slideTime = 0;

        while(slideTime < 1)
        {
            slideTime += speed;
            speedUpRect.anchorMin = new Vector2(Mathf.Lerp(0.67f, 0, slideTime),0);
            speedUpRect.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 2,slideTime);
            darkenerImage.color = new Color(0, 0, 0, Mathf.Lerp(0, 0.8f, slideTime));
            yield return new WaitForEndOfFrame();
        }


        //---------------------------Slide in Text
        Vector3 startPos = speedText.transform.localPosition;
        Vector3 endPos = startPos + new Vector3(0, 100, 0);

        slideTime = 0;
        while(slideTime < 1)
        {
            slideTime += speed;
            speedText.transform.localPosition = Vector3.Lerp(startPos, endPos, slideTime);
            speedText.localScale = Vector3.Lerp(Vector3.one * 0.5f, Vector3.one, slideTime);         
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1.5f);
        //--------------------------Slide to center and Enlarge
        slideTime = 0;

        while (slideTime < 1)
        {
            slideTime += speed;
            speedUpRect.anchorMin = new Vector2(Mathf.Lerp(0, 0.67f, slideTime), 0);
            speedUpRect.localScale = Vector3.Lerp(Vector3.one*2, Vector3.one, slideTime);
            darkenerImage.color = new Color(0, 0, 0, Mathf.Lerp(0.8f, 0, slideTime));
            yield return new WaitForEndOfFrame();
        }

        GameDetails.setPaused();
       
    }


}
