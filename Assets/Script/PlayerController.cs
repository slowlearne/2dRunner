using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("GameManager")]
    public GameManager gameManagerObj;

    [Header("Player Settings")]
    public float playerInitialSpeed = 5f;
    public float playerAcceleration = 0.5f;      // The rate at which the player speed increases over time   
    public float playerCurrentSpeed;               // Current speed of the player

    public float gravity;
    public float groundHeight;

    public float acceleration;
    public float maxAcceleration;
    public float distance;                       //value that is travelled by player is displayed as score.
    public float maxSideVelocity;               //maximum speed player can reach.
    public Vector2 velocity;
    public float jumpVelocity;                     //Initial Jump force

    public bool isPlayerGrounded;                   //to check if player is grounded.

    public bool isHoldingJump;
    public float maxTimeforJump;
    public float timerForJump;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip jumpSound, crashSound;

    void Start()
    {
        playerCurrentSpeed = playerInitialSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)                        //to Detect player collision with obstacle
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            audioSource.PlayOneShot(crashSound);
            gameManagerObj.gameOver();
        }
    }
    void Update()                                           //Update method is used for input only.
    {
        if (!gameManagerObj.isGameOver)                            //Until the game is not over the player can give input                      
        {
            if (isPlayerGrounded)                                 //to feed only one input jump at a time before hitting ground.
            {
                // Jump when the player presses the space bar
                if (Input.GetKeyDown(KeyCode.Space) || TouchBegan())              //Input from Keyboard as well as Touch
                {
                    isPlayerGrounded = false;
                    audioSource.PlayOneShot(jumpSound);
                    velocity.y = jumpVelocity;                                   //Initial Jump Velocity
                    isHoldingJump = true;
                }


            }
            if (Input.GetKeyUp(KeyCode.Space) || !TouchEnded())         //checking if the space key is up or Touch is off the screen.
            {
                isHoldingJump = false;
            }
        }

    }

    private void FixedUpdate()                                             //fixed Update is used for Movement calculation.
    {
        if (!gameManagerObj.isGameOver)
        {
            playerCurrentSpeed += playerAcceleration * Time.deltaTime;          // Increase the playerspeed over time
            float move = playerCurrentSpeed * Time.deltaTime;             // Calculate the playermovement along the x-axis
            transform.position += new Vector3(move, 0, 0);               // Apply the movement to the player position
            Vector2 playerPosition = transform.position;
            if (!isPlayerGrounded)
            {
                playerPosition.y += velocity.y * Time.fixedDeltaTime;              //Increment vertical position of player per frame
                velocity.y += gravity * Time.fixedDeltaTime;                      //-ve gravity makes player velocity negative when the player reaches the top for fall

                if (playerPosition.y <= groundHeight)
                {
                    playerPosition.y = groundHeight;
                    isPlayerGrounded = true;
                    timerForJump = 0f;
                }

            }

            //increase distance over time if on ground or jumping
            distance += velocity.x * Time.fixedDeltaTime;

            //only increase speed while on ground
            if (isPlayerGrounded)
            {
                // to accelerate slower over period of time
                float velocityRatio = velocity.x / maxSideVelocity;           //velocity ratio from 0 to 1
                acceleration = maxAcceleration * (1 - velocityRatio);

                velocity.x += acceleration * Time.fixedDeltaTime;


                if (velocity.x >= maxSideVelocity)
                {
                    velocity.x = maxSideVelocity;
                }
            }


            transform.position = playerPosition;
        }
    }

    bool TouchBegan()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Only return true if the touch just began
            if (touch.phase == TouchPhase.Began)
            {
                return true;
            }
        }
        return false;
    }

    // Function to detect if the touch has ended
    bool TouchEnded()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Return true if the touch has ended or was canceled
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                return true;
            }
        }
        return false;
    }
}
