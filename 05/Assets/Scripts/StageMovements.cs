using UnityEngine;
using System.Collections;

public class StageMovements : MonoBehaviour {
    /*Controls the stage in the background by moving the water, ducks and trees*/
    public GameObject firstWaterLane;
    public GameObject secondWaterLane;

    public GameObject treesLane;

    private Vector3 firstStartPos;
    private Vector3 secondStartPos;

    private float animationTimer;
    private bool timeAsceneding;

	// Use this for initialization
	void Start () {
        animationTimer = 0;
        timeAsceneding = true;

        firstStartPos = firstWaterLane.transform.localPosition;
        secondStartPos = secondWaterLane.transform.localPosition;
	}
	// Update is called once per frame
	void Update () {
        //Handles movement of the water
        Vector3 waterMovement = new Vector3(1, 0, 0);
        timerBehaviour(2);
        firstWaterLane.transform.localPosition = Vector3.Lerp(firstStartPos, firstStartPos + waterMovement, animationTimer);
        secondWaterLane.transform.localPosition = Vector3.Lerp(secondStartPos, secondStartPos - waterMovement, animationTimer);

        //Movement of trees
        treesLane.transform.GetChild(0).transform.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(-5,5,animationTimer));
        treesLane.transform.GetChild(1).transform.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(5, -5, animationTimer));

    }
    //Affects the speed and movement of the props
    void timerBehaviour(float speed)
    {
        float transSpeed = Time.deltaTime/speed;
        if (timeAsceneding)
        {
            animationTimer += transSpeed;
        }
        else
        {
            animationTimer -= transSpeed;
        }
        if (animationTimer < 0 || animationTimer > 1)
        {
            timeAsceneding = !timeAsceneding;
        }
    }
}
