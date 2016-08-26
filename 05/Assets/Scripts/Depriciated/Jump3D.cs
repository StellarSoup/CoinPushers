using UnityEngine;
using System.Collections;

public class Jump3D : MonoBehaviour {
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    private Vector3 moveDirection = Vector3.zero;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = Vector3.zero;

            if (Input.GetMouseButtonDown(0))
            {
                moveDirection.y = jumpSpeed;
            }                       
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
	}
}
