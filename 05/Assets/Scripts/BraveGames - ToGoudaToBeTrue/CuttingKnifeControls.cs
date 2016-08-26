using UnityEngine;
using System.Collections;

public class CuttingKnifeControls : BG_ToGoudaToBeTrue_Menu {

    //Keeps track of the current finger touches
    private Vector2 getFirstTouch;
    private Vector2 getCurrectTouch;
    public float KnifeYPosition;
    private Vector2 getKnifeStartingPos;
    float howMuchToCut = 0;

    RectTransform goudaMessageRect;

    private bool playerHasWon;

    void Start()
    {
        //Move the message off screen
        goudaMessageRect = GameObject.Find("GoudaForYou").GetComponent<RectTransform>();
        goudaMessageRect.offsetMin = goudaMessageRect.offsetMax = new Vector2(0, Screen.height);
        playerHasWon = false;
        KnifeYPosition = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update () {
        //When the player first clicks
        if (!playerHasWon)
        {
            if (Input.GetMouseButtonDown(0))
        {
            //Gets the first touch
            Vector2 newPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            getFirstTouch = new Vector2(newPoint.x, KnifeYPosition);
            getKnifeStartingPos = transform.localPosition;
        }        
            //Moves the knife while the finger is on the screen
            if (Input.GetMouseButton(0))
            {
                MoveKnife();
            }
            //Resets the positions of the finger on finger lift
            if (Input.GetMouseButtonUp(0))
            {
                ResetControls();
            }
            //If the player cuts the cheese they win
            if (transform.localPosition.y < -1.55f)
            {
                cutThroughCheese();
            }
        }
    }

    //Handles moving the knife back and forth
    private void MoveKnife()
    {
        Vector2 newPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        getCurrectTouch = new Vector2(newPoint.x, 0);

        float distanceFromKnife = Vector2.Distance(getFirstTouch, getCurrectTouch);
        if (distanceFromKnife != 0)
        {
            howMuchToCut += 0.05f;
        }

        Vector2 newKnifePos = new Vector2(newPoint.x, KnifeYPosition - howMuchToCut);
        transform.localPosition = newKnifePos;
        getFirstTouch = getCurrectTouch;
    }
    //Resets how much was cut
    private void ResetControls()
    {
        KnifeYPosition -= howMuchToCut;
        howMuchToCut = 0;
    }
    //If the player cuts through the cheese they win
    private void cutThroughCheese()
    {
        if (!playerHasWon)
        {
            playerHasWon = true;
            print("You Win");
            WinGame();
            StartCoroutine(moveAwayKnifeAndCheese());
        }
    }
    //Moves the knife and cheese off screen
    IEnumerator moveAwayKnifeAndCheese()
    {
        Vector3 startKnifePos = transform.localPosition;
        Vector3 endKnifePos = startKnifePos - new Vector3(40, 0, 0);

        GameObject cheese = GameObject.Find("Cheese/Front");
        Vector3 startCheesePos = cheese.transform.localPosition;
        Vector3 endCheesePos = cheese.transform.localPosition + new Vector3(10, 0, 0);
        Vector2 goudeMessageOffscale = goudaMessageRect.offsetMin;
        float time = 0;
        while(time < 1)
        {
            time += Time.deltaTime;
            goudaMessageRect.offsetMin = goudaMessageRect.offsetMax = Vector2.Lerp(goudeMessageOffscale, Vector2.zero, time);
            transform.localPosition = Vector3.Lerp(startKnifePos, endKnifePos, time);
            cheese.transform.localPosition = Vector3.Lerp(startCheesePos, endCheesePos, time);
            yield return new WaitForEndOfFrame();
        }
    }

    
    
}
