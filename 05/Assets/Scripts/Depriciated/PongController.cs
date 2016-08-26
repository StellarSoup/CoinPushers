using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PongController : MonoBehaviour {

    private Camera main;
    private GameObject pongBall;

    private Vector2 cameraPos;
    private Vector2 cameraSize;
	// Use this for initialization
	void Start () {
        main = Camera.main;
        pongBall = GameObject.Find("Pong Ball");
        if (KeyDirectory.Games.Score.CountWins() == 0)
        {
            StartCoroutine(startNewGame());
        }else
        {
            InitGame();
            ChangeTextTime("");
            GameDetails.isPaused = false;
        }
    }

    IEnumerator startNewGame()
    {
        ChangeTextTime("");

        for(int i = 3; i > 0; i--)
        {
            ChangeTextTime(i.ToString());
            yield return new WaitForSeconds(1f);
        }
        ChangeTextTime("Go!");
        InitGame();
        yield return new WaitForSeconds(1f);
        ChangeTextTime("");
        GameDetails.isPaused = false;

    }

    private void ChangeTextTime(string time)
    {
        GameObject.Find("MidGameCanvas/Pong Count Down Timer/Text").GetComponent<Text>().text = time;
    }

    private void InitGame()
    {
        pongBall.transform.position = Vector3.zero;
        Rigidbody2D rigBod = pongBall.GetComponent<Rigidbody2D>();
        rigBod.velocity = Vector2.zero;
        float appliedForce = 90;
        rigBod.AddForce(new Vector2(4, (Random.value * 10)-5) * appliedForce);
    }

    private bool ballIsPastPaddle()
    {
        return pongBall.transform.position.x > 10 || pongBall.transform.position.x < -10;
    }

    // Update is called once per frame
    void Update () {
        if (ballIsPastPaddle())
        {
            InitGame();
        }
	}
}
