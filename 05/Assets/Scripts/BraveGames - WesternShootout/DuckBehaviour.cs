using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DuckBehaviour : a_DuckShootOut_Menu {

    /*Handles how the duck moves*/

    //Used to keep track of the animation for the duck
    private float animationTimer;
    private bool timeAsceneding;

    //Initial y pos
    private float startYPos;

    //Keeps track of if the duck is moving to the right
    private bool moveToTheRight;

    // Use this for initialization
    void Start()
    {
        //If the duck is to the right
        if (transform.localPosition.x > 0)
        {
            //Move left
            moveToTheRight = false;
            transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
        }else
        //else
        {
            //Move right
            moveToTheRight = true;
        }
        animationTimer = 0;
        timeAsceneding = true;
        startYPos = transform.localPosition.y;
    }
    //Affects the animation of the duck
    void timerBehaviour(float speed)
    {
        float transSpeed = Time.deltaTime / speed;
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
    // Update is called once per frame
    void Update () {
        timerBehaviour(2);
        MoveDuck (-5, 5);
	}
    //Handles how the duck will move
    private void MoveDuck(float min,float max)
    {
        float duckSpeed = Time.deltaTime * 3;
        //Move left if duck is required to
        if (!moveToTheRight)
        {
            duckSpeed *= -1;
        }
        //Move to new position
        transform.localPosition =  new Vector3(transform.localPosition.x + duckSpeed, startYPos +  Mathf.Lerp(0, 0.5f, animationTimer),transform.localPosition.z);
        //While the duck is moving rotate the body relative to the animation timer
        transform.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(min, max, animationTimer));
    }
}
