using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{


    //Public int health;
    //Public int maxHealth;

    //Public float acceleration;
    //Public float maximumVelocity;

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

    LayerMask groundLayer;

    public float playerSpeed;

    public float playerMaxSpeed;

    public float playerJumpHeightCheck;

    [SerializeField]
    Vector3Variable playerPosition;

    [SerializeField]
    QuaternionVariable playerRotation;
    
    Rigidbody rb;

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
        return Physics.Raycast(transform.position, -transform.up, playerJumpHeightCheck, groundLayer);
    }

    void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        jumpCooldownTimer = jumpCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        bool jumpCooldownDone = jumpCooldownTimer <= 0f;
        bool grounded = OnGround();
        bool canJump = grounded && jumpCooldownDone;

        if ((verticalInput + horizontalInput) != 0)
        {
            Vector3 moveDirection = verticalInput * transform.forward + horizontalInput * transform.right;

            rb.AddForce(moveDirection.normalized * playerSpeed * 10f * (grounded ? 1f : airMultiplier), ForceMode.Force);
            
            if (rb.velocity.magnitude > playerMaxSpeed) rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -playerMaxSpeed, playerMaxSpeed), rb.velocity.y, Mathf.Clamp(rb.velocity.z, -playerMaxSpeed, playerMaxSpeed));
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump) 
        {
            Jump(); 
        }

        if (!jumpCooldownDone) jumpCooldownTimer -= Time.deltaTime;

        playerPosition.Set(transform.position);
    }
}
