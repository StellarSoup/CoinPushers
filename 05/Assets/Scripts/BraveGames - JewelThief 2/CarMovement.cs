using UnityEngine;
using System.Collections;

public class CarMovement : BG_JewelThief2_Menu
{

    private SpriteRenderer carImage;
    private float speed;
    private float vertSpeed; 
    public GameObject carPointer;
    public GameObject initPointer;
    private bool playerhasWon;

    private float lengthToPoint;

    //Changes the direction of the car
    void ChangeDirection()
    {

        carImage.flipX = !(carImage.flipX);
        speed *= -1;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        //If the player collides with the diamond they win
        if (coll.transform.name.Equals("Diamond"))
        {
            if (!playerhasWon)
            {
                playerhasWon = true;
                WinGame();
                GameObject.Find("Game 5").GetComponent<BG_JewelThief2_Menu>().slideInGemBanner();
                Destroy(coll.gameObject);
                speed = 0;
                vertSpeed = 0;
            }
        }
        //If the player collides with the wall they lose
        if (coll.transform.tag.Equals("JT-Wall"))
        {
            Lose();
            speed = 0;
            vertSpeed = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        //If the player collides with the wall they lose
        if (coll.transform.tag.Equals("JT-Wall"))
        {
            Lose();
        }
    }

    void Start()
    {
        speed = Time.deltaTime * 5;
        vertSpeed = (speed / 2);
        carImage = GetComponent<SpriteRenderer>();
        setCarPointerPos();
    }
    void Update()
    {
        if (!playerhasWon)
        {
            Movement();
        }
    }
    //Set the starting point and the point that the car has to drive to
    void setCarPointerPos()
    {
        lengthToPoint = 0;
        //parametric Line Equation : px = ax + t(bx-ax)
        Vector3 a = transform.position;
        initPointer.transform.position = a;
        Vector3 b = transform.position + new Vector3(speed, vertSpeed,0);
        carPointer.transform.position = a + 100*(b - a);
    }
    //Controls the movement of the car
    void Movement()
    {
        float carSpeed = Time.deltaTime / 2;
        transform.position = Vector3.Lerp(initPointer.transform.position, carPointer.transform.position, lengthToPoint);
        lengthToPoint += carSpeed;
        if (Input.GetMouseButtonDown(0))
        {
            ChangeDirection();
            setCarPointerPos();
        }
        if(lengthToPoint >= 1)
        {
            setCarPointerPos();
            lengthToPoint = 0;
        }
    }
}
