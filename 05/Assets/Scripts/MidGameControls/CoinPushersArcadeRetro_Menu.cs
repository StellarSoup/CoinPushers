using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CoinPushersArcadeRetro_Menu : MonoBehaviour {

    Vector3 startSexyPos, endSexyPos, startNormalPos, endNormalPos;

    private bool playerCanInteract;

    // Use this for initialization
    void Start() {
        playerCanInteract = true;
        //Moves everything that will display on the arcade screen off screen
        GameObject mainMenu = GameObject.Find("Mode Select/Main Menu");
        GameObject start = GameObject.Find("Mode Select/Start");
        startSexyPos = new Vector3(Screen.width * 2, mainMenu.transform.localPosition.y, mainMenu.transform.localPosition.z);
        endSexyPos = new Vector3(0, mainMenu.transform.localPosition.y, mainMenu.transform.localPosition.z);
        startNormalPos = new Vector3(Screen.width * 2 * -1, start.transform.localPosition.y, start.transform.localPosition.z);
        endNormalPos = new Vector3(0, start.transform.localPosition.y, start.transform.localPosition.z);

        GameObject.Find("HighScore/Score").GetComponent<Text>().text = KeyDirectory.Games.HighScore.getHighScore(0).ToString();
        //Slides int the title and buttons
        StartCoroutine(slideArcadeSelect(true));
        StartCoroutine(slideInHighScoreAndCredits(true));
        StartCoroutine(slideInCopyrightInfo(true));
    }
    private void changeTextColor(GameObject textObj, Color newColor)
    {
        textObj.GetComponent<Text>().color = newColor;
    }
    //Starts the arcade mode
    public void StartArcadeMode()
    {
        if (playerCanInteract)
        {
            playerCanInteract = false;
            StartCoroutine(slideArcadeSelect(false));
            StartCoroutine(slideInHighScoreAndCredits(false));
            StartCoroutine(slideInCopyrightInfo(false));
        }
    }
    //Moves to main menu
    public void MainMenu()
    {
        if (playerCanInteract)
        {
            playerCanInteract = false;
            SceneManager.LoadScene("Main Menu");
            Destroy(GameObject.Find("Timer&MusicPlayer"));
            Destroy(GameObject.Find("GameCanvas"));
        }
    }
    //Slides in (fake) copyright info
    IEnumerator slideInCopyrightInfo(bool state)
    {
        GameObject copyright = GameObject.Find("CopyRight Info");

        Vector3 endPos = copyright.transform.localPosition;
        Vector3 startPos;

        if (state)
        {
            startPos = endPos - new Vector3(0, 100, 0);
        }
        else
        {
            startPos = endPos + new Vector3(0, 100, 0);
        }

        float time = 0;

        while (time < 1)
        {
            time += Time.deltaTime;
            if (state)
            {
                copyright.transform.localPosition = Vector3.Lerp(startPos, endPos, time);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    //Slides in highscore and credits
    IEnumerator slideInHighScoreAndCredits(bool state)
    {
        GameObject highScoreAndCredits = GameObject.Find("HighScore and Credit");
        Vector3 endPos = highScoreAndCredits.transform.localPosition;
        Vector3 startPos;

        if (state)
        {
            startPos = endPos + new Vector3(0, 100, 0);
        }else
        {
            startPos = endPos - new Vector3(0, 100, 0);
        }

        float time = 0;

        while (time < 1)
        {
            time += Time.deltaTime;
            if (state)
            {
                highScoreAndCredits.transform.localPosition = Vector3.Lerp(startPos, endPos, time);
            }
            yield return new WaitForEndOfFrame();
        }
    }
    //Slides in the main components to the screen
    IEnumerator slideArcadeSelect(bool state)
    {
        GameObject logoImage = GameObject.Find("Game Logo/Image");
        GameObject mainMenu = GameObject.Find("Mode Select/Main Menu");
        GameObject start = GameObject.Find("Mode Select/Start");

        Vector3 startingLogoPos = new Vector3(0, Screen.height * 2, 0);

        if (state)
        {
            logoImage.GetComponent<RectTransform>().localPosition = startingLogoPos;
            mainMenu.GetComponent<RectTransform>().localPosition = startSexyPos;
            start.GetComponent<RectTransform>().localPosition = startNormalPos;
        }

        float logoImageTime = 0;
        yield return new WaitForSeconds(1f);
        while (logoImageTime < 1)
        {
            float imageMovementSpeed = Time.deltaTime / 2;
            logoImageTime += imageMovementSpeed;
            if (state)
            {
                logoImage.GetComponent<RectTransform>().localPosition = Vector3.Lerp(startingLogoPos, Vector3.zero, logoImageTime);
                mainMenu.GetComponent<RectTransform>().localPosition = Vector3.Lerp(startSexyPos, endSexyPos, logoImageTime);
                start.GetComponent<RectTransform>().localPosition = Vector3.Lerp(startNormalPos, endNormalPos, logoImageTime);
            }else
            {
                logoImage.GetComponent<RectTransform>().localPosition = Vector3.Lerp(Vector3.zero,startingLogoPos,logoImageTime);
                mainMenu.GetComponent<RectTransform>().localPosition = Vector3.Lerp(endSexyPos, startSexyPos, logoImageTime);
                start.GetComponent<RectTransform>().localPosition = Vector3.Lerp(endNormalPos, startNormalPos, logoImageTime);

                mainMenu.GetComponent<RectTransform>().offsetMin = new Vector2(mainMenu.GetComponent<RectTransform>().offsetMin.x, 0);
                mainMenu.GetComponent<RectTransform>().offsetMax = new Vector2(mainMenu.GetComponent<RectTransform>().offsetMax.x, 0);

                start.GetComponent<RectTransform>().offsetMin = new Vector2(start.GetComponent<RectTransform>().offsetMin.x, 0);
                start.GetComponent<RectTransform>().offsetMax = new Vector2(start.GetComponent<RectTransform>().offsetMax.x, 0);


            }

            yield return new WaitForEndOfFrame();
        }
        if (!state)
        {
            SceneManager.LoadScene("ArcadeMidGame");
        }
    }
}
