using UnityEngine;
using System.Collections;

public class MatchTwoPairControls : BG_MatchShapes_Menu
{

	public bool piecesAreMatching;
    public bool randomPosition;

    public float GetDistance()
    {
        float dist = Vector2.Distance(transform.position, transform.GetChild(0).position);
        return dist;
    }
    void Start()
    {
        piecesAreMatching = false;
        if (randomPosition)
        {
            SetChildPosition();
        }
    }
    void Update()
    {
        MatchPiece();
    }
    //Moves the piece to a random spot
    void SetChildPosition()
    {
        GameObject matchingPiece = transform.GetChild(0).gameObject;
        matchingPiece.transform.localPosition = new Vector2(Random.Range(-1.5f,1.5f),Random.Range(0.5f,1.2f));
    }
    //If the piece is close to the hole it will be moved in
    private void MatchPiece()
    {
        //If piece is close to slot
        if (GetDistance() < 0.5f)
        {
            //Pieces become matched
            transform.GetChild(0).localPosition = new Vector3(0, 0, -1);
            piecesAreMatching = true;
        }
    }
}
