using System.Collections;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private PolyCityUISystemController ui;

    private CharacterController characterController;
    private Animator animator;
    private Vector3 velocity;
    private Vector3 move;
    private Vector3 targetPosition;
    public LayerMask groundLayer;
    public Transform groundCheck;
    private bool isGrounded;
    private bool isSliding;

    private float forwardSpeed = 10f; // 10f ~ 20f
    private float gravity = -20f;
    private float jumpHeight = 3f;
    private float slideSpeed = 15f; // 15 ~ 30
    private float slideDuration = 1f;
    private float slideHeight = 0.5f; // height of the character during slide
    private float originalHeight;
    private int currentLane = 1; // target position to move to
    private float laneDistance = 2.5f;
    private int sideMoveDirection = 0;
    private float sideMoveSpeed = 30f;
    private Vector3 startPosition;
    private float zPosition;
    private float totalZDistanceMoved = 0f;
    private int coin = 0;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        originalHeight = characterController.height;
        startPosition = transform.position;
        zPosition = transform.position.z;
        coin = 0;
    }

    private void Update()
    {
        if (GameManager.Ins.IsGameOver)
        {
            return;
        }

        if (!GameManager.Ins.IsGameStarted)
        {
            return;
        }

        CalculateMoveDistance();
        HandleMovement();

        // Check if the player is grounded
        //isGrounded = characterController.isGrounded;
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.17f, groundLayer);

        // Gravity
        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // small negative value to keep the player grounded
        }
        ;
        // Jump
        //if (isGrounded && Input.GetKeyDown(KeyCode.UpArrow))
        if (isGrounded && MyInputManager.Ins.swipeUp)
        {
            Jump();
        }

        characterController.Move(velocity * Time.deltaTime);

        // Slide
        //if (isGrounded && !isSliding && Input.GetKeyDown(KeyCode.DownArrow))
        if (isGrounded && !isSliding && MyInputManager.Ins.swipeDown)
        {
            StartCoroutine(SlideRoutine());
        }

        // Move Left
        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        if (MyInputManager.Ins.swipeLeft)
        {
            sideMoveDirection = -1;
            currentLane = Mathf.Clamp(currentLane + sideMoveDirection, 0, 2);
        }

        // Move Right
        //if (Input.GetKeyDown(KeyCode.RightArrow))
        if (MyInputManager.Ins.swipeRight)
        {
            sideMoveDirection = 1;
            currentLane = Mathf.Clamp(currentLane + sideMoveDirection, 0, 2);
        }

        SideMove();

        characterController.Move(move * Time.deltaTime);

        animator.SetFloat("Speed", forwardSpeed);
        animator.SetFloat("VerticalSpeed", velocity.y);
        animator.SetBool("IsGrounded", isGrounded);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            ui.GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Coin")
        {
            coin++;

            Destroy(other.transform.gameObject);

            AudioManager.Ins.PlaySound("Coin");
        }
    }

    public void ResetPlayer()
    {
        transform.position = startPosition;
        totalZDistanceMoved = 0;
        coin = 0;
        GameManager.Ins.Score = 0;
        move = Vector3.zero;
        velocity = Vector3.zero;
        animator.SetFloat("Speed", 0);
        animator.SetFloat("VerticalSpeed", 0);
        animator.SetBool("IsGrounded", true);
        ui = GameObject.Find("PolyCityUISystem").GetComponent<PolyCityUISystemController>();
    }

    private void HandleMovement()
    {
        /*
        // Get input for movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Apply movement
        characterController.Move(move * forwardSpeed * Time.deltaTime);
        */
        move.z = forwardSpeed;
    }

    private void CalculateMoveDistance()
    {
        // Calculate the distance moved along the Z axis since the last frame
        float zDistanceMoved = Mathf.Abs(transform.position.z - zPosition);

        // Update the total Z distance moved
        totalZDistanceMoved += zDistanceMoved;

        GameManager.Ins.Score = (int)totalZDistanceMoved / 10 + coin * 2;

        // Update the last Z position to the current Z position
        zPosition = transform.position.z;
    }

    private void Jump()
    {
        StopCoroutine(SlideRoutine());
        EndSlide();

        AudioManager.Ins.PlaySound("Jump");
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    private IEnumerator SlideRoutine()
    {
        StartSlide();

        yield return new WaitForSeconds(slideDuration);

        EndSlide();
    }

    private void StartSlide()
    {
        AudioManager.Ins.PlaySound("Slide");
        isSliding = true;
        animator.SetBool("IsSliding", true);
        move.z = slideSpeed;
        characterController.height = slideHeight;
    }

    private void EndSlide()
    {
        isSliding = false;
        animator.SetBool("IsSliding", false);
        move.z = forwardSpeed;
        characterController.height = originalHeight;
    }

    private void SideMove()
    {
        targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (currentLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (currentLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        //transform.position = targetPosition;
        //if (transform.position != targetPosition)
        if (Vector3.Distance(transform.position, targetPosition) > 0.05f)
        {
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * sideMoveSpeed * Time.deltaTime;

            if (moveDir.sqrMagnitude < diff.magnitude)
            {
                characterController.Move(moveDir);
            }
            else
            {
                characterController.Move(diff);
            }
        }
    }
}
