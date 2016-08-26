using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneTransitionController : MonoBehaviour {

    /*Handles transition between minigames*/
    private GameObject doorTrans;
    private RectTransform dtLeft, dtRight;
    // Use this for initialization
    void Start () {
        doorTrans = GameObject.Find("DoorTransition");
        dtLeft = doorTrans.transform.GetChild(0).GetComponent<RectTransform>();
        dtRight = doorTrans.transform.GetChild(1).GetComponent<RectTransform>();
        
        dtLeft.anchorMax = new Vector2(0, 1);
        dtRight.anchorMin = new Vector2(1, 0);
    }
    public void slideTrans(string levelName)
    {
        StartCoroutine(loadLevel(levelName));
    }
    //Handles level loading
    IEnumerator loadLevel(string newScene)
    {
        
        float slide = 0;
        float speed = 0;
        float speedUp = 0.005f;

        //Slides in two black bars completely covering the screen
        while (slide < 1)
        {
            slide += speed;
            speed += speedUp;
            dtLeft.anchorMax = new Vector2(Mathf.Lerp(0, 0.5f, slide), 1);
            dtRight.anchorMin = new Vector2(Mathf.Lerp(1, 0.5f, slide), 0);
            yield return new WaitForEndOfFrame();
        }

        slide = 0;
        speed = 0;
        //Changes the level
        YouWinControls.breakTrans = true;
        SceneManager.LoadScene(newScene);

        //If the player loses they are taken to the game over screen
        if (PlayerSettings.CURRENT_HEALTH == 0)
        {
            GameObject.Find("Timer&MusicPlayer").GetComponent<MusicPlayer>().slowDownAndDestroy();
            Destroy(transform.parent.gameObject);
        }
        dtLeft.anchorMax = new Vector2(0, 1);
        dtRight.anchorMin = new Vector2(1, 0);
    }
}
