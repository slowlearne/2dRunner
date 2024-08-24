using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManagerObj;
    public float playerInitialSpeed = 5f;

    // The rate at which the player speed increases over time 
    public float playerAcceleration = 0.5f;

    // Current speed of the player
    public float playerCurrentSpeed;

    public float gravity;
    public float groundHeight;

    public float acceleration;
    public float maxAcceleration;
    public float distance;
    public float maxSideVelocity;               //maximum speed player can reach
    public Vector2 velocity;
    public float jumpVelocity;
    
    public bool isPlayerGrounded; 
   
    public bool isHoldingJump;
    public float maxTimeforJump;
    public float timerForJump;

    public AudioSource audioSource;
    public AudioClip jumpSound, crashSound;
 
    void Start()
    {
        playerCurrentSpeed = playerInitialSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            audioSource.PlayOneShot(crashSound);
            gameManagerObj.gameOver();
        }
    }
    void Update()
    {
        if (!gameManagerObj.isGameOver)
        {
            if (isPlayerGrounded)
            {
                // Jump when the player presses the space bar
                if (Input.GetKeyDown(KeyCode.Space) || TouchBegan())
                {
                    isPlayerGrounded = false;
                    audioSource.PlayOneShot(jumpSound);
                    velocity.y = jumpVelocity;
                    isHoldingJump = true;
                }


            }
            if (Input.GetKeyUp(KeyCode.Space) || !TouchEnded())
            {
                isHoldingJump = false;
            }
        }
        
    }

    private void FixedUpdate()
    {
        if (!gameManagerObj.isGameOver)
        {
            // Increase the playerspeed over time
            playerCurrentSpeed += playerAcceleration * Time.deltaTime;

            // Calculate the playermovement along the x-axis
            float move = playerCurrentSpeed * Time.deltaTime;

            // Apply the movement to the player position
            transform.position += new Vector3(move, 0, 0);

            Vector2 playerPosition = transform.position;
            if (!isPlayerGrounded)
            {
                if (isHoldingJump)
                {
                    timerForJump += Time.fixedDeltaTime;
                    if (timerForJump >= maxTimeforJump)
                    {
                        isHoldingJump = false;
                    }
                }
                playerPosition.y += velocity.y * Time.fixedDeltaTime;
                if (!isHoldingJump)
                {
                    velocity.y += gravity * Time.fixedDeltaTime;
                }

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
