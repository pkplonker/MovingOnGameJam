using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public float playerSpeed;

    public float playerMaxSpeed;

    public float playerJumpHeightCheck;

    public float groundDrag;

    [SerializeField]
    Vector3Variable playerPosition;

    [SerializeField]
    QuaternionVariable playerRotation;
    
    Rigidbody rb;

    float verticalInput;
    float horizontalInput;

    bool jumpCooldownDone;
    bool grounded;

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

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        jumpCooldownDone = jumpCooldownTimer <= 0f;
        grounded = OnGround();

        bool canJump = grounded && jumpCooldownDone;

        if (grounded) rb.drag = groundDrag;
        else rb.drag = 0;

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        { 
            Jump();
        }

        if (!jumpCooldownDone)
        {
            jumpCooldownTimer -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if ((verticalInput != 0) || (horizontalInput != 0))
        {
            Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

            rb.AddForce(moveDirection.normalized * playerSpeed * 10f * (grounded ? 1f : airMultiplier), ForceMode.Force);

            if (rb.velocity.magnitude > playerMaxSpeed) rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -playerMaxSpeed, playerMaxSpeed), rb.velocity.y, Mathf.Clamp(rb.velocity.z, -playerMaxSpeed, playerMaxSpeed));
        }

        playerPosition.Set(transform.position);
    }
}
