using UnityEngine;
using System.Collections;

public class BG_AnimalStacker_Menu : GameState {

    //Keeps track of how many blocks there are
    GameObject[] animalObject;
    //Keeps track of weather the player has won or not
    private bool playerHasWon;
    // Use this for initialization
    void Start () {
        playerHasWon = false;
        //Gets all blocks in the scene
        DragAnimalObject[] animal = FindObjectsOfType(typeof(DragAnimalObject)) as DragAnimalObject[];
        animalObject = new GameObject[animal.Length];
        //Returns their gameobjects
        for (int i = 0; i < animalObject.Length; i++)
        {
            animalObject[i] = animal[i].transform.gameObject;
        }
        TimerLose();
    }
	
	// Update is called once per frame
	void Update () {
        CheckIfWon();
	}

    //Checks if they blocks have reached a certain height
    //and they are not being dragged
    //and if they are stationary
    void CheckIfWon()
    {
        if (!playerHasWon)
        {
            for (int i = 0; i < animalObject.Length; i++)
            {
                bool highEnough = transform.position.y < animalObject[i].transform.position.y;
                bool beingDragged = animalObject[i].GetComponent<DragAnimalObject>().beingDragged;
                bool isGrounded = animalObject[i].GetComponent<DragAnimalObject>().isGrounded;
                if (highEnough && !beingDragged && isGrounded)
                {
                    
                    playerHasWon = true;
                    print(playerHasWon);
                    WinGame();
                    break;
                }
            }
        }
    }
}
