using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AudioClip jumpSoundEffect;

    private CharacterController controller;
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
    private float sideMoveSpeed = 10f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        originalHeight = controller.height;
    }

    private void Update()
    {
        HandleMovement();

        // Check if the player is grounded
        //isGrounded = controller.isGrounded;
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

        // Jump
        if (isGrounded && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }

        controller.Move(velocity * Time.deltaTime);

        // Slide
        if (isGrounded && !isSliding && Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(SlideRoutine());
        }

        // Move Left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            sideMoveDirection = -1;
            currentLane = Mathf.Clamp(currentLane + sideMoveDirection, 0, 2);
        }

        // Move Right
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            sideMoveDirection = 1;
            currentLane = Mathf.Clamp(currentLane + sideMoveDirection, 0, 2);
        }

        SideMoving();

        controller.Move(move * Time.deltaTime);

        animator.SetFloat("Speed", forwardSpeed);
        animator.SetFloat("VerticalSpeed", velocity.y);
        animator.SetBool("IsGrounded", isGrounded);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            GameManager.Ins.GameOver = true; 

        }
    }

    void HandleMovement()
    {
        /*
        // Get input for movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Apply movement
        controller.Move(move * forwardSpeed * Time.deltaTime);
        */
        move.z = forwardSpeed;
    }

    private void Jump()
    {
        StopCoroutine(SlideRoutine());
        EndSlide();

        if (jumpSoundEffect != null)
        {
            AudioSource.PlayClipAtPoint(jumpSoundEffect, transform.position, 0.5f);
        }

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
        if (jumpSoundEffect != null)
        {
            AudioSource.PlayClipAtPoint(jumpSoundEffect, transform.position, 0.5f);
        }

        isSliding = true;
        animator.SetBool("IsSliding", true);
        move.z = slideSpeed;
        controller.height = slideHeight;
    }

    private void EndSlide()
    {
        isSliding = false;
        animator.SetBool("IsSliding", false);
        move.z = forwardSpeed;
        controller.height = originalHeight;
    }

    private void SideMoving()
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
            Vector3 moveDir = diff.normalized * 30 * Time.deltaTime;

            if (moveDir.sqrMagnitude < diff.magnitude)
            {
                controller.Move(moveDir);
            }
            else
            {
                controller.Move(diff);
            }
        }
    }
}
