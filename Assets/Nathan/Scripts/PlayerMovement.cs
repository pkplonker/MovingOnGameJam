using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



enum MovementState
{
    eWalking,
    eSprinting,
    eWallRunning,
    eAir
}


[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    //Float jumpCooldownTime;

    //Private float currentVelocity;

    //Public float jumpForce;
    //Public float sprintModifier;
    [SerializeField]
    UnityEvent response;

    [SerializeField]
    FloatVariable playerHealth;

    public float jumpCooldown;
    float jumpCooldownTimer;
    public float jumpForce;
    public float airMultiplier;

    public LayerMask groundLayer;

    float playerSpeed;

    public float playerWalkSpeed;
    public float playerSprintSpeedMultiplier;

    public float playerSprintDuration;
    public float playerSprintCooldownMultiplier;

    bool sprintCooldownDone;
    float sprintCooldownTimer;

    public float playerMaxSpeed;

    public float playerJumpHeightCheck;

    public float groundDrag;

    [SerializeField]
    Vector3Variable playerPosition;

    MovementState playerMoveState;

    [SerializeField]
    QuaternionVariable playerRotation;
    
    Rigidbody rb;

    float verticalInput;
    float horizontalInput;

    bool jumpButtonPressed;
    KeyCode jumpButtonKey = KeyCode.Space;

    bool sprintButtonHeld;
    KeyCode sprintButtonKey = KeyCode.LeftShift;

    bool jumpCooldownDone;
    bool grounded;

    bool movementKeysPressed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void UpdateRotation()
    {
        transform.rotation = playerRotation.Get();
    }

    bool OnGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, playerJumpHeightCheck, groundLayer);
    }

    void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        jumpCooldownTimer = jumpCooldown;
    }

    void UserInput()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");
        movementKeysPressed = (verticalInput != 0) || (horizontalInput != 0);

        jumpButtonPressed = Input.GetKeyDown(jumpButtonKey);

        sprintButtonHeld = Input.GetKey(sprintButtonKey);
    }

    void ManageMovementState()
    {
        jumpCooldownDone = jumpCooldownTimer <= 0f;
        grounded = OnGround();

        bool canJump = grounded && jumpCooldownDone;
        bool sprintDurationRemaining = sprintCooldownTimer > 0f;

        bool canSprint = grounded && sprintDurationRemaining;

        if (movementKeysPressed && sprintButtonHeld && canSprint)
        {
            playerMoveState = MovementState.eSprinting;
            playerSpeed = playerWalkSpeed * playerSprintSpeedMultiplier;

            sprintCooldownTimer -= Time.deltaTime;

            if (sprintCooldownTimer < 0f) sprintCooldownTimer -= 0.3f;
        }
        else if (movementKeysPressed && grounded)
        {
            playerMoveState = MovementState.eWalking;
            playerSpeed = playerWalkSpeed;
        }
        else
        {
            playerMoveState = MovementState.eAir;
            playerSpeed = playerWalkSpeed * airMultiplier;
        }

        if (jumpButtonPressed && canJump)
        {
            Jump();
        }

        if (grounded) rb.drag = groundDrag;
        else rb.drag = 0;

        if (!jumpCooldownDone)
        {
            jumpCooldownTimer -= Time.deltaTime;
        }

        bool playerIsNotSprinting = playerMoveState != MovementState.eSprinting;
        bool playerSprintNotRefilled = sprintCooldownTimer < playerSprintDuration;

        if (playerIsNotSprinting && playerSprintNotRefilled)
        {
            sprintCooldownTimer += Time.deltaTime * playerSprintCooldownMultiplier;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UserInput();
        ManageMovementState();
    }

    void FixedUpdate()
    {
        Debug.Log(sprintCooldownTimer);
        bool movementKeysHaveBeenPressed = (verticalInput != 0) || (horizontalInput != 0);

        if (movementKeysHaveBeenPressed)
        {
            Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

            rb.AddForce(moveDirection.normalized * playerSpeed * 10f, ForceMode.Force);

            if (rb.velocity.magnitude > playerMaxSpeed) rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -playerMaxSpeed, playerMaxSpeed), rb.velocity.y, Mathf.Clamp(rb.velocity.z, -playerMaxSpeed, playerMaxSpeed));
        }

        bool playerHasMoved = playerPosition.Get() != transform.position;

        if (playerHasMoved) playerPosition.Set(transform.position);
    }
}
