using UnityEngine;
using System.Collections;

public class PaddleControls : MonoBehaviour {

    private bool beingDragged;

    //Keeps track of the two points the paddle can't move past
    Vector2 limits;

    void Start()
    {
        limits.x = -7.5f;
        limits.y = -limits.x;
        beingDragged = false;
    }

    //Moves the paddle but locks it between the two x points
    void OnMouseDrag()
    {
        beingDragged = true;
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        point.y = gameObject.transform.position.y;
        point.z = gameObject.transform.position.z;
        point.x = Mathf.Clamp(point.x, limits.x, limits.y);

        gameObject.transform.position = point;
    }
    void OnMouseUp()
    {
        beingDragged = false;
    }
}
