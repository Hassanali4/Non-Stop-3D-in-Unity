// This script is responsible for controlling the player's movements, including their position, speed, and animation.
// The player can move forward and sideways, jump, and slide. The speed gradually increases over time.
// The player also makes running sounds, which vary depending on the gender of the character.

using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;// Reference to the CharacterController component
    private Vector3 move;// The movement vector for the player
    public float forwardSpeed;// The player's speed
    public float maxSpeed;// The maximum speed that the player can reach

    private int desiredLane = 1;// The player's current lane: 0 is left, 1 is middle, 2 is right 
    public float laneDistance = 2.5f;// The distance between lanes

    public bool isGrounded; // Whether the player is currently grounded
    public LayerMask groundLayer;// The layer to use for checking if the player is on the ground
    public Transform groundCheck;// The position to check if the player is on the ground

    public float gravity = -12f;// The strength of gravity
    public float jumpHeight = 2;// The height that the player can jump
    private Vector3 velocity;// The player's current velocity

    public Animator animator; // Reference to the Animator component
    private bool isSliding = false;// Whether the player is currently sliding   

    public float slideDuration = 1.5f;// The length of time that the player slides

    bool toggle = false;// A toggle variable used to gradually increase the speed and time scale
    public bool running;// Whether the player is currently running
    public bool man = false;// Whether the player character is male
    public bool women = false;// Whether the player character is female
    public bool tire = false;// Unused variable

    void Start()
    {
        controller = GetComponent<CharacterController>(); // Get the CharacterController component attached to the player GameObject
        Time.timeScale = 1.2f; // Increase the time scale slightly to make the game more challenging
    }

    private void FixedUpdate()
    {
        // If the game hasn't started or has ended, do nothing
        if (!PlayerManager.IsGameStarted)
            return;

        // Increase the speed gradually
        if (toggle)
        {
            toggle = false;
            if (forwardSpeed < maxSpeed)
                forwardSpeed += 0.1f * Time.fixedDeltaTime;
        }
        // Increase the time scale gradually
        else
        {
            toggle = true;
            if (Time.timeScale < 2f)
                Time.timeScale += 0.005f * Time.fixedDeltaTime;
        }

    }

    void Update()
    {
        // If the game hasn't started or has ended, do nothing
        if (!PlayerManager.IsGameStarted)
            return;

        animator.SetBool("isGameStarted", true);// Start the running animation
        move.z = forwardSpeed;

        // Check if the player is on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.17f, groundLayer);
        // Set the "isGrounded" parameter in the Animator component to the value of "isGrounded"
        animator.SetBool("isGrounded", isGrounded);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }

        // If the player is on the ground, they can jump or slide
        if (isGrounded)
        {
            // If the jump button or swipe up is pressed, call the Jump method
            if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || SwipeManager.swipeUp))
            {
                Jump();
            }

            // If the slide button or swipe down is pressed, call the Slide coroutine
            if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || SwipeManager.swipeDown) && !isSliding)
                StartCoroutine(Slide());
        }
        else
        {
            // Apply gravity when the player is in the air
            velocity.y += gravity * Time.deltaTime;
            // If the slide button or swipe down is pressed, call the Slide coroutine and set the y velocity to -10
            if (SwipeManager.swipeDown && !isSliding)
            {
                StartCoroutine(Slide());
                velocity.y = -10;
            }

        }

        // Move the player using the character controller's Move method
        controller.Move(velocity * Time.deltaTime);

        //Gather the inputs on which lane we should be
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) ||  SwipeManager.swipeRight))
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || SwipeManager.swipeLeft))
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }

        
        // Calculate where the player should be in the future   
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredLane == 0)
            targetPosition += Vector3.left * laneDistance;
        else if (desiredLane == 2)
            targetPosition += Vector3.right * laneDistance;

        // Move the player towards the target position using the character controller's Move method
        if (transform.position != targetPosition)
        {
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * 30 * Time.deltaTime;
            if (moveDir.sqrMagnitude < diff.magnitude)
                controller.Move(moveDir);
            else
                controller.Move(diff);
                
        }

        //Using Controllers Move Method to move the player
        controller.Move(move * Time.deltaTime);

        // Implement running sound logic
        RunningSound();


    }

    private void RunningSound()
    {
        // Checking if the player animation state is "Run"
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {

            if (running == false)
            {
                // Play running sound effect according to the selected gender
                if (man)
                {
                    FindObjectOfType<AudioManager>().Play("Running_FootSteps");
                }
                else if (women)
                {
                    FindObjectOfType<AudioManager>().Play("Women_Running");
                }
                running = true;
            }
        }
        else if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            if (running == true)
            {
                // Stop running sound effect according to the selected gender   
                if (man)
                    FindObjectOfType<AudioManager>().Stop("Running_FootSteps");
                else if (women)
                    FindObjectOfType<AudioManager>().Stop("Women_Running");
                running = false;
            }
        }

        //Stoping Running Sound if Game is Paused implementation logic
        if (PlayerManager._gamePause)
        {
            if (man)
                FindObjectOfType<AudioManager>().Stop("Running_FootSteps");
            if (women)
                FindObjectOfType<AudioManager>().Stop("Women_Running");
        }
    } // Running Sound method ended

    // This method handles the player jump action
    private void Jump()
    {
        StopCoroutine(Slide());
        animator.SetBool("isSliding", false);
        if (man)
        {
            // Play man jump sound effect
            //FindObjectOfType<AudioManager>().Stop("Running_FootSteps");
            FindObjectOfType<AudioManager>().Play("Jump_Sigh");
        }
        else if (women)
        {
            // Play woman jump sound effect
            FindObjectOfType<AudioManager>().Play("Women_Jump_Sigh");
        }
        else if (tire)
        {
                FindObjectOfType<AudioManager>().Play("Tire_Jump_Sound");
        }
        animator.SetTrigger("jump");
        controller.center = Vector3.zero;
        controller.height = 2;
        isSliding = false;

        velocity.y = Mathf.Sqrt(jumpHeight * 2 * -gravity);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            if (man)
            {
                FindObjectOfType<AudioManager>().Play("Man_In_Pain");
                FindObjectOfType<AudioManager>().Stop("Running_FootSteps");
            }
            else if (women)
            {
                FindObjectOfType<AudioManager>().Play("Woman_Pain");
                FindObjectOfType<AudioManager>().Stop("Women_Running");
            }
            else if(tire)
                FindObjectOfType<AudioManager>().Play("Crashing_Sound");
            PlayerManager._gameOver = true;
            FindObjectOfType<AudioManager>().Play("GameOver");
        }
    }

    // This method handles the player slide action
    private IEnumerator Slide()
    {
        if (man || women)
        {
            //FindObjectOfType<AudioManager>().Stop("Running_FootSteps");
            FindObjectOfType<AudioManager>().Play("Sliding");
        }
        else if (tire)
            FindObjectOfType<AudioManager>().Play("TireSliding");
        isSliding = true;
        animator.SetBool("isSliding", true);
        yield return new WaitForSeconds(0.25f / Time.timeScale);
        controller.center = new Vector3(0, -0.5f, 0);
        controller.height = 1;

        yield return new WaitForSeconds((slideDuration - 0.25f) / Time.timeScale);

        animator.SetBool("isSliding", false);

        controller.center = Vector3.zero;
        controller.height = 2;

        isSliding = false;
    }
}