using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Jumping2D : MonoBehaviour {

    /*
     * Develpor Notes
     * 
     * During the game the player must maintain their lives till the end of the game.
     * When the player gets to the end they will play a level of an actual game, 
     * and must survive the level and reach the end of the game
     *
     * Each difficulty might have a different game but its important that the player understands 
     * when they have died or when they succedded
     */

    private Rigidbody rig;
    private CharacterController controller;
    private Vector3 respawnPoint;

    public float speed = 6;
    public float jumpSpeed = 8;
    public float gravity = 20;
    private Vector3 moveDirection = Vector3.zero;

    public bool playerIsActive;
    private bool playerWonLastGame;

    private bool autoJumpIsCalled;

    private GameObject currHealth;

    // Use this for initialization
    void Start()
    {
        if (KeyDirectory.Games.Score.doesPlayerHaveEnoughWins())
        {
            playerIsActive = true;
        }else
        {
            playerIsActive = false;
        }



        currHealth = GameObject.Find("CurrentHealth");
        
            currHealth.GetComponent<Text>().text = (PlayerSettings.CURRENT_HEALTH).ToString();
        
        autoJumpIsCalled = false;
        playerWonLastGame = KeyDirectory.Lives.getState();

            GameDetails.isPaused = false;
        

        respawnPoint = transform.position;
        rig = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        if (!playerIsActive)
        {
            if (playerWonLastGame)
            {
                StartCoroutine(jumpOverPit());
            }
            else
            {
                StartCoroutine(fallIntoPit());
            }
        }
    }
	
    IEnumerator fallIntoPit()
    {
        currHealth.GetComponent<Text>().text = (PlayerSettings.CURRENT_HEALTH + 1).ToString();
        yield return new WaitForSeconds(2f);
        currHealth.GetComponent<Text>().text = (PlayerSettings.CURRENT_HEALTH).ToString();


    }

    IEnumerator jumpOverPit(){
        print("Jump Called");
        
            yield return new WaitForSeconds(0.8f);
        autoJumpIsCalled = true;
        yield return new WaitForSeconds(1.2f);
        autoJumpIsCalled = true;
        print("JumpExecuted");
        }
	
	// Update is called once per frame
	void Update () {
        Movement();
        if(transform.position.y <= -10)
        {
            Death();
        }
        if (playerIsActive)
        {
            GameObject healthText = transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject;
            healthText.GetComponent<Text>().text = PlayerSettings.CURRENT_HEALTH.ToString();
        }

        
	}
    void Death()
    {
            transform.position = respawnPoint;
        if (playerIsActive)
        {
            KeyDirectory.Lives.wasLifeLost(true);
            PlayerSettings.LoseHealth();
            print("Currrent Health = " + PlayerSettings.CURRENT_HEALTH);

            if(PlayerSettings.CURRENT_HEALTH == 0)
            {
                print("You Lost");
                KeyDirectory.Games.Score.ResetScore();
                Destroy(GameObject.Find("Timer&MusicPlayer"));
                Destroy(GameObject.Find("GameCanvas"));
                SceneManager.LoadScene("Title Card");
            }
        }

    }
    private void Movement()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(1, 0, 0);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (playerIsActive)
            {
                if (Input.GetMouseButtonDown(0))
                {
                        Jump();
                }
            }else
            {
                if (autoJumpIsCalled)
                {
                    autoJumpIsCalled = false;
                    Jump();
                    
                }
            }
            

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
    private void Jump()
    {
        moveDirection.y = jumpSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("CheckPoint"))
        {
            respawnPoint = other.transform.position;
        }
        if (other.transform.name.Equals("Goal"))
        {
            print("You Win!");
            KeyDirectory.Games.Score.ResetScore();
            Destroy(GameObject.Find("Timer&MusicPlayer"));
            Destroy(GameObject.Find("GameCanvas"));
            SceneManager.LoadScene("Title Card");
            
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.Equals("Enemy"))
        {
            Death();
        }
    }
}
