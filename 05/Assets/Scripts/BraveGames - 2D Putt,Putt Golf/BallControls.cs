using UnityEngine;
using System.Collections;

public class BallControls : BG_PuttPuttGolf_Menu
{

	//A multiplyer for the force applied to the ball
	public float speed = 1f;
    //Keeps track of when the ball is in the hole
    private bool inHole = false;

	struct DoubleVector{
		public Vector2 vecOne;
		public Vector2 vecTwo;
	}

    //Keeps track of what the ball is shooting towards
    private GameObject aim;

	//Used to hold the mouse pos in two states: Mouse down(first frame) and Mouse Up
	private DoubleVector mousePos;

	// Use this for initialization
	void Start () 
	{
        aim = transform.GetChild(1).gameObject;
        aim.SetActive(false);

    }


    //Only works for this object's collider
    void OnMouseDown(){
        //Get the first mouse Position
        mousePos.vecOne = transform.position;
        aim.SetActive(true);
	}
    //While the mouse is being dragged move the gameobject that controls the aim
    void OnMouseDrag()
    {
        mousePos.vecTwo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aim.transform.position = Vector3.LerpUnclamped(mousePos.vecTwo, mousePos.vecOne, 2f);
        GetComponent<LineRenderer>().enabled = true;
        LineRenderer line = GetComponent<LineRenderer>();
        line.SetPosition(0, aim.transform.position);
        line.SetPosition(1, transform.position);

    }

    //Only works for this object's collider
    void OnMouseUp(){
        //Shoots the ball at a constant speed
        BallController.SetContoller("CONSTANT");
        //Hides the aimer
        aim.SetActive(false);
        GetComponent<LineRenderer>().enabled = false;

    }
    //Handles ball controls
    class BallController{
        //Sets what controller to use
        public static void SetContoller(string mode)
        {
            if (mode.Equals("VARIED"))
            {
                ApplyVariedForce();
            }
            if (mode.Equals("CONSTANT"))
            {
                ApplyConstantForce();
            }
        }
        //A constant force is applied to the ball regardless of the distance between the ball and the mouse release
        private static void ApplyConstantForce()
        {
            GameObject ball = GameObject.Find("Ball");
            BallControls bCon = ball.GetComponent<BallControls>();
            //Calculates the force, and the direction to apply to the ball
            var direction = bCon.mousePos.vecOne - bCon.mousePos.vecTwo;
            direction.Normalize();
            ball.GetComponent<Rigidbody2D>().AddForce(direction * bCon.speed * 500);
        }
        //A varied force is applied to the ball dependent on the distance between the ball and the mouse release
        private static void ApplyVariedForce()
        {
            GameObject ball = GameObject.Find("Ball");
            BallControls bCon = ball.GetComponent<BallControls>();

            //Calculates the force, and the direction to apply to the ball
            var direction = bCon.mousePos.vecOne - bCon.mousePos.vecTwo;
            float dist = Mathf.Sqrt(Mathf.Pow((bCon.mousePos.vecOne.x - bCon.mousePos.vecTwo.x), 2.0f) + Mathf.Pow((float)(bCon.mousePos.vecOne.y - bCon.mousePos.vecTwo.y), 2.0f));
            direction.Normalize();
            ball.GetComponent<Rigidbody2D>().AddForce(direction * bCon.speed * dist);
        }
    }

	void OnTriggerEnter2D(Collider2D grav){
		//If the player enters the hole trigger they will win
		if (grav.transform.tag.Equals ("Hole")) {
            WinGame();
            StartCoroutine(ScaleDownBall());
            Destroy(GetComponent<Rigidbody2D>());
            inHole = true;
		}
	}

    void Update()
    {    
        //If the ball is in the hole it will move towards the middle
        if (inHole)
        {
            Gravitate(GameObject.FindGameObjectWithTag("Hole"));
        }
    }
    //Moves the current gameobject to the target object
    void Gravitate(GameObject target)
    {
        if (target != null)
        {
            transform.LookAt(target.transform);
            transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime*3);
            transform.eulerAngles = Vector3.zero;
        }
    }
    //Scales the ball down to look like it is falling in the hole
    IEnumerator ScaleDownBall()
    {
        Vector3 currScale = gameObject.transform.localScale;
        float time = 0;
        float speed = Time.deltaTime * 1;
        while(time  < 1)
        {
            gameObject.transform.localScale = Vector3.Lerp(currScale, Vector3.zero, time);
            time += speed;
            yield return new WaitForEndOfFrame();
        }
    }

}
