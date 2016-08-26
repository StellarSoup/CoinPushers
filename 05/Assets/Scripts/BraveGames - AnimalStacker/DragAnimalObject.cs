using UnityEngine;
using System.Collections;

public class DragAnimalObject : GameState
{ 
    //Keeps track if the block is being dragged and is grounded
    public bool beingDragged;
    public bool isGrounded;


    void Start()
    {
        beingDragged = false;
    }

    void Update()
    {
        //Checks if the block is stationary
        if(transform.GetComponent<Rigidbody2D>().velocity.y >= 0)
        {
            isGrounded = true;
        }else
        {
            isGrounded = false;
        }
    }
    //Moves the object on mouse drag
    void OnMouseDrag()
    {
        beingDragged = true;
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        point.z = gameObject.transform.position.z;
        gameObject.transform.position = point;
    }
    void OnMouseUp()
    {
        beingDragged = false;
    }
}
