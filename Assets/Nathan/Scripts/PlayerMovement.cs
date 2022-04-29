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
    public LayerMask wallLayer;

    float playerSpeed;

    public float playerWalkSpeed;
    public float playerSprintSpeedMultiplier;

    public float playerSprintDuration;
    public float playerSprintCooldownMultiplier;

    public float playerWallRunningConsumptionMultiplier;

    public float playerMaxSlopeAngle;

    public float wallJumpUpForce;
    public float wallJumpSideForce;

    public float wallRunningCooldown;
    float wallRunningCooldownTimer;

    RaycastHit slopeHit;

    bool sprintCooldownDone;
    float sprintCooldownTimer;

    public float playerMaxSpeed;

    public float playerJumpHeightCheck;
    public float playerWallDepthCheck;

    public float groundDrag;

    public Vector3 playerActualSpeed;

    [SerializeField]
    FloatVariable playerSprint;

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

    bool crouchButtonHeld;
    bool crouchButtonPrevious;
    KeyCode crouchButtonKey = KeyCode.LeftControl;

    bool jumpCooldownDone;
    bool grounded;

    RaycastHit leftWallRaycastHit;
    RaycastHit rightWallRaycastHit;

    bool leftWallHit;
    bool rightWallHit;

    bool verticalKeysPressed;
    bool horizontalKeysPressed;
    bool movementKeysPressed;

    public float crouchScale;
    public float crouchMultiplier;
    bool crouchTransition;
    public float crouchCooldown;
    float crouchCooldownTimer;

    public float currentSlopeAngle;

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

    bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerJumpHeightCheck, groundLayer))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            currentSlopeAngle = angle;
            return angle < playerMaxSlopeAngle && angle != 0;
        }

        return false;
    }

    bool OnWall()
    {
        rightWallHit = Physics.Raycast(transform.position, transform.right, out rightWallRaycastHit, playerWallDepthCheck, wallLayer);
        leftWallHit = Physics.Raycast(transform.position, -transform.right, out leftWallRaycastHit, playerWallDepthCheck, wallLayer);

        bool wallHit = (rightWallHit || leftWallHit);
        bool wallRunningCooldownDone = wallRunningCooldownTimer <= 0f;

        return wallHit && wallRunningCooldownDone;
    }

    Vector3 SlopeMovementDirection(Vector3 moveDirection, Vector3 slopeNormal)
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeNormal).normalized;
    }

    void Jump()
    {
        rb.AddForce(transform.up * jumpForce * (crouchButtonHeld ? crouchMultiplier : 1f), ForceMode.Impulse);
        jumpCooldownTimer = jumpCooldown;
    }
    void WallJump()
    {
        Vector3 wallNormal = rightWallHit ? rightWallRaycastHit.normal : leftWallRaycastHit.normal;


        Vector3 jumpForce = transform.up * wallJumpUpForce + wallNormal * wallJumpSideForce;

        rb.AddForce(jumpForce, ForceMode.Impulse);
        wallRunningCooldownTimer = wallRunningCooldown;
        jumpCooldownTimer = jumpCooldown;
    }

    void UserInput()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        verticalKeysPressed = verticalInput != 0;
        horizontalKeysPressed = horizontalInput != 0;
        movementKeysPressed = verticalKeysPressed || horizontalKeysPressed;

        jumpButtonPressed = Input.GetKeyDown(jumpButtonKey);

        sprintButtonHeld = Input.GetKey(sprintButtonKey);

        crouchButtonHeld = Input.GetKey(crouchButtonKey);

        bool crouchCooldownDone = crouchCooldownTimer <= 0f;

        if (crouchCooldownDone)
        {
            if (crouchButtonHeld != crouchButtonPrevious)
            {
                crouchTransition = true;
            }

            crouchButtonPrevious = crouchButtonHeld;
        }
    }

    void ManageMovementState()
    {
        jumpCooldownDone = jumpCooldownTimer <= 0f;
        
        grounded = OnGround();
        bool sprintDurationRemaining = sprintCooldownTimer > 0f;
        bool onWall = OnWall();

        bool canSprint = grounded && sprintDurationRemaining;
        bool canJump = (grounded || onWall) && jumpCooldownDone;

        bool playerSprinting = movementKeysPressed && sprintButtonHeld && canSprint;
        bool playerWalking = movementKeysPressed && grounded;
        bool playerJumping = jumpButtonPressed && canJump;
        bool playerWallRunning = horizontalKeysPressed && verticalKeysPressed && onWall && sprintDurationRemaining && !grounded && !crouchButtonHeld;

        if (playerWallRunning)
        {
            playerMoveState = MovementState.eWallRunning;
            playerSpeed = playerWalkSpeed;

            sprintCooldownTimer -= Time.deltaTime * playerWallRunningConsumptionMultiplier;

            if (sprintCooldownTimer < 0f) sprintCooldownTimer -= 0.3f;
        }
        else if (playerSprinting)
        {
            playerMoveState = MovementState.eSprinting;
            playerSpeed = playerWalkSpeed * playerSprintSpeedMultiplier * (crouchButtonHeld ? crouchMultiplier : 1f);

            sprintCooldownTimer -= Time.deltaTime;

            if (sprintCooldownTimer < 0f) sprintCooldownTimer -= 0.3f;
        }
        else if (playerWalking)
        {
            playerMoveState = MovementState.eWalking;
            playerSpeed = playerWalkSpeed * (crouchButtonHeld ? crouchMultiplier : 1f);
        }
        else
        {
            playerMoveState = MovementState.eAir;
            playerSpeed = playerWalkSpeed * airMultiplier;
        }

        if (playerJumping)
        {
            if (playerWallRunning) WallJump();
            else Jump();
        }

        if (grounded) rb.drag = groundDrag;
        else rb.drag = 0;

        if (!jumpCooldownDone)
        {
            jumpCooldownTimer -= Time.deltaTime;
        }

        if (wallRunningCooldownTimer > 0f) wallRunningCooldownTimer -= Time.deltaTime;
        if (crouchCooldownTimer > 0f) crouchCooldownTimer -= Time.deltaTime;

        bool playerIsNotSprinting = playerMoveState != MovementState.eSprinting;
        bool playerSprintNotRefilled = sprintCooldownTimer < playerSprintDuration;
        bool playerRecoversSprint = playerIsNotSprinting && playerSprintNotRefilled;

        if (playerRecoversSprint)
        {
            sprintCooldownTimer += Time.deltaTime * playerSprintCooldownMultiplier;
        }

        playerSprint.Set((sprintCooldownTimer) / playerSprintDuration);
    }

    // Update is called once per frame
    void Update()
    {
        UserInput();
        ManageMovementState();
    }

    void FixedUpdate()
    {
        rb.useGravity = true;

        if (crouchTransition)
        {

            transform.localScale = new Vector3(transform.localScale.x, (crouchButtonHeld ? crouchScale : 1f), transform.localScale.z);
            if (grounded) rb.AddForce(Vector3.down * 10f, ForceMode.Impulse);
            crouchTransition = false;
            crouchCooldownTimer += crouchCooldown;
        }

        if (movementKeysPressed)
        {
            Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

            float verticalVelocity = rb.velocity.y;

            if (playerMoveState == MovementState.eWallRunning)
            {
                Vector3 wallNormal = rightWallHit ? rightWallRaycastHit.normal : leftWallRaycastHit.normal;

                Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

                if ((transform.forward - wallForward).magnitude > (transform.forward + wallForward).magnitude) wallForward = -wallForward;

                moveDirection = wallForward;

                rb.useGravity = false;
                verticalVelocity = 0f;
            }
            else if (OnSlope())
            { 
                moveDirection = SlopeMovementDirection(moveDirection, slopeHit.normal);
            }

            rb.AddForce(moveDirection.normalized * playerSpeed * 10f, ForceMode.Force);

            float maximumAllowedSpeed = playerMaxSpeed;

            if (playerMoveState == MovementState.eSprinting) maximumAllowedSpeed *= playerSprintSpeedMultiplier;

            if (rb.velocity.magnitude > maximumAllowedSpeed) rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -maximumAllowedSpeed, maximumAllowedSpeed), verticalVelocity, Mathf.Clamp(rb.velocity.z, -maximumAllowedSpeed, maximumAllowedSpeed));
        }

        bool playerHasMoved = playerPosition.Get() != transform.position;

        if (playerHasMoved) playerPosition.Set(transform.position);

        playerActualSpeed = rb.velocity;
    }

   
}
