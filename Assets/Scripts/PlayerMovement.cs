using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 2.0f;
    public float playerBackSpeed = 1.0f;
    public float playerWalkSpeed = 2.0f;
    [SerializeField] private float dashSpeed = 5.0f;
    [SerializeField] private float dashTime = 0.5f; 
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
    [SerializeField] private Vector3 hitPoint;
    //[SerializeField] private float rotationSpeed = 5f;
    public RenderTexture renderTexture;
    private float initialSpeed;
    private bool isDashing = false;  // Flag to check if the player is currently dashing
    public float dashDuration = 0.5f;  // Duration of the dash in seconds

    private Animator animator;
    public float health = 100f;

    public float mouseSensitivity = 1f;

    private float currentMousePosition;
    public float rotationSpeed = 2f;

    private float rotationY = 0f;

    private Vector3 movementVector; // Store the movement vector

    public Transform respawnPoint;

    private bool _isInLight = false;
    [SerializeField] private float _sideWalkThreshhold = 0.5f;

    public Vector3 GetMovementVector()
    {
        return movementVector;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        initialSpeed = playerSpeed;
        health = 100f;
    }

    void FixedUpdate()
    {
        MoveWithInput();
        SecondaryMovement();

        RotateWithMouse();

        if (health <= 0)
        {
            Die();
        }
    }

    void RotateWithMouse()
    {
        // Get the horizontal mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;

        // Update the rotation around the Y-axis
        rotationY += mouseX * rotationSpeed;

        // Apply the rotation to the player's GameObject
        transform.rotation = Quaternion.Euler(0f, rotationY, 0f);

        // Hide the cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }


    void MoveWithInput ()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Move the character using CharacterController based on WASD input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(horizontal, 0, vertical);
        movementVector = new Vector3(horizontal, 0, vertical).normalized;

        // Check if the player is walking backward
        bool isWalkingBackward = Vector3.Dot(movementVector, transform.forward) < 0f;
        animator.SetBool("isWalkingBackwards", isWalkingBackward);

        bool isWalkingRight = false;
        bool isWalkingLeft = false;
        float dotProduct = Vector3.Dot(movementVector, transform.right);

        if (Mathf.Abs(dotProduct) > _sideWalkThreshhold)
        {
            // Player is walking sideways

            if (dotProduct > 0)
            {
                // Player is walking to the right
                Debug.Log("Walking to the right");
                isWalkingRight = true;
                isWalkingLeft = false;
            }
            else if (dotProduct < 0)
            {
                // Player is walking to the left
                Debug.Log("Walking to the left");
                isWalkingRight = false;
                isWalkingLeft = true;
            }
        }

        animator.SetBool("isWalkingRight", isWalkingRight);
        animator.SetBool("isWalkingLeft", isWalkingLeft);

        if (horizontal != 0 || vertical != 0)
        {
            animator.SetBool("isRunning", true);
        } else
        {
            animator.SetBool("isRunning", false);

        }

        controller.Move(movementVector * Time.deltaTime * (isWalkingBackward ? playerBackSpeed : playerSpeed));
        movementVector = new Vector3(horizontal, 0, vertical).normalized;

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            animator.SetBool("jump", true);
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        } else if (Input.GetButtonUp("Jump"))
        {
            animator.SetBool("jump", false);
        }


        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

            // Dash input
        if (Input.GetButtonDown("Dash") && !isDashing)
        {
            StartCoroutine(Dash());
        }

        // Apply dash speed if dashing, otherwise regular movement speed
        float speed = isDashing ? dashSpeed : playerSpeed;

    }

    IEnumerator Dash()
    {
        isDashing = true;

        // Save the current movement speed
        float originalMovementSpeed = playerSpeed;

        // Set a higher movement speed for the dash
        playerSpeed = dashSpeed;

        // Wait for the dash duration
        yield return new WaitForSeconds(dashDuration);

        // Restore the original movement speed
        playerSpeed = originalMovementSpeed;

        isDashing = false;
    }

    void SecondaryMovement()
    {
        if (Input.GetButtonDown("Walk"))
        {
            playerSpeed = playerWalkSpeed;
        } else if (Input.GetButtonUp("Walk"))
        {
            playerSpeed = initialSpeed;
        }
    }

    void Die()
    {
        transform.position = respawnPoint.position;
        health = 100f;
        Debug.Log("Death");
    }

    private void OnTriggerStay(Collider other)
    {
        _isInLight = other.CompareTag("FlashlightCone");
    }

    public bool isInLight ()
    {
        return _isInLight;
    }
}

