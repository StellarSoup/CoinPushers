using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;

public class StartNewGame : GameState {
    private string canvasName = "GameCanvas";

    private float currTime;

    private bool gameHasStarted;


    private GameObject instructionsPanel;
    // Use this for initialization
    void Start () {
        gameHasStarted = false;
    }
	
	// Update is called once per frame

    IEnumerator slideToNewGame()
    {
        Camera camera = Camera.main;
        float time = 0;
        Rect currentCam = camera.rect;
        BloomOptimized bloomO = camera.GetComponent<BloomOptimized>();
        float getCurrBloom = bloomO.intensity;
        RectTransform arcadeControls = GameObject.Find("AracdeControlsOverView").GetComponent<RectTransform>();
        RectTransform outsideCanvas = GameObject.Find("Arcade Cabinent Canvas/Center").GetComponent<RectTransform>();
        Vector3 outsideCanScale = outsideCanvas.localScale;
        float change = 0;
        //Zoom into the game camera and fade away the fisheye and bloom intensity
        while (time <= 1)
        {
            time += change;
            change += Time.deltaTime;
            camera.rect = new Rect(Mathf.Lerp(currentCam.x, 0, time), Mathf.Lerp(currentCam.y, 0, time),
                                    Mathf.Lerp(currentCam.width, 1, time), Mathf.Lerp(currentCam.height, 1, time));
            bloomO.intensity = Mathf.Lerp(getCurrBloom, 0, time);

            outsideCanvas.localScale = Vector3.Lerp(outsideCanScale, new Vector3(1.65f,1.65f,1.65f), time);
            arcadeControls.localScale = Vector3.Lerp(Vector3.one, new Vector3(3f, 3f, 3f), time);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.5f);
        
        StartGame();
    }

    public void StartNormalMode()
    {
        if (!gameHasStarted)
        {
            StartCoroutine(slideToNewGame());
            gameHasStarted = true;
        }
    }

    //Load the next game and initilise the character stats
    private void StartGame()
    {
        KeyDirectory.Games.Score.ResetScore();
        PlayerSettings.InitCharacterStats();
    }
}
