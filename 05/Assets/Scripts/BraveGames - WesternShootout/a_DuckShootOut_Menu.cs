using UnityEngine;
using System.Collections;

public class a_DuckShootOut_Menu : GameState {

    private int numberOfDucks;//Keeps track of current number of ducks in the game
    private bool playerHasWon;

	// Use this for initialization
	void Start () {
        playerHasWon = false;
        //Creates a random number of ducks
        numberOfDucks = Random.Range(5,8);
        CreateDucks(numberOfDucks);
        TimerLose();
	}
	
	// Update is called once per frame
	void Update () {
        //If a duck is pressed, it will dissapear
        ShootDuck();
        //When there are no more ducks they player will win
        if(numberOfDucks == 0 && !playerHasWon)
        {
            playerHasWon = true;
            WinGame();
        }
    }
    //Create ducks with different positions
    void CreateDucks(int numberOfDucks)
    {
        float[] startXPos = { 9, 15 };
        float[] startYPos = { 1, 3, 4.5f };
        float[] startZPos = { 1, 3, 5 };


        for (int i = 0; i < numberOfDucks; i++)
        {
            int moveFromLeft = Random.Range(0, 2);

            int chooseStartingPos = (int)(Random.value * 3);
            //Load in the ducks
            GameObject newDuck = Instantiate(Resources.Load("11-BraveGames-DuckShootOut/Duck Rotation", typeof(GameObject))) as GameObject;
            newDuck.transform.SetParent(GameObject.Find("Scenery").transform);
            newDuck.transform.localScale = new Vector3(2.5f, 2.5f, 1);
            if (moveFromLeft == 0)
            {
                newDuck.transform.localPosition = new Vector3(Random.Range(-startXPos[0], -startXPos[1]), startYPos[chooseStartingPos], startZPos[chooseStartingPos]);
            }else
            {
                newDuck.transform.localPosition = new Vector3(Random.Range(startXPos[0], startXPos[1]), startYPos[chooseStartingPos], startZPos[chooseStartingPos]);
            }
        }
    }
    //Handles shooting of ducks
    void ShootDuck()
    {
        //If the player presses on a duck
        if (Input.GetMouseButtonDown(0))
        {
            Camera camera = Camera.main;
            RaycastHit2D ray = Physics2D.Raycast(new Vector2(camera.ScreenToWorldPoint(Input.mousePosition).x, camera.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 0f);

            DuckBehaviour duck = ray.transform.parent.GetComponent<DuckBehaviour>();
            if (duck != null)
            {
                //Remove the duck from play
                Destroy(ray.transform.parent.gameObject);
                numberOfDucks--;
            }
        }
    }
}
