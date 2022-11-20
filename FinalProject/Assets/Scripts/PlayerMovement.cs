using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{



    [Header("Move Variables")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private float accel;
    [SerializeField] private float decc;
    [SerializeField] private float velPower;

    [Header("Jump Variables")]
    [SerializeField] private float jumpCoyoteTime = 0.5f;
    [SerializeField] private float jumpBufferTime = 0.5f;
    [SerializeField] private float lastGroundTime;
    [SerializeField] private float lastJumpTime;
    [SerializeField] private float jumpCutMultiplier = 0.5f;
    [SerializeField] private float fallGravity;

    public bool isJumping;
    public bool jumpInputIsReleased;


    [Header("Ground Variables")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;


    private Rigidbody2D rb2d;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }





    void Update()
    {

        lastJumpTime -= Time.deltaTime;
        lastGroundTime -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpDownInput();
        }

        if (Input.GetKeyUp(KeyCode.Space)) {
            OnJumpUp();
        }



        if (isGrounded() && !isJumping)
        {
            lastGroundTime = jumpCoyoteTime;
        }

        if (isJumping && rb2d.velocity.y < 0)
        {
            isJumping = false;
            rb2d.gravityScale = gravity * fallGravity;

        }
        else {
            rb2d.gravityScale = gravity;
        }



        if (lastGroundTime > 0 && !isJumping && lastJumpTime > 0)
        {
            Jump();
        }

        move();

    }




    private void move()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        
        float targetSpeed = moveHorizontal * speed;
        
        float speedDifference = targetSpeed - rb2d.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? accel : decc;

        float movement = Mathf.Pow(Mathf.Abs(speedDifference) * accelRate, velPower) * Mathf.Sign(speedDifference);

        rb2d.AddForce(movement * Vector2.right);

    }


    private void Jump() {
        lastJumpTime = 0;
        lastGroundTime = 0;
        jumpInputIsReleased = false;
        isJumping = true;
        rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }


    private void OnJumpUp() {

        if (rb2d.velocity.y > 0 && isJumping) {
            rb2d.AddForce(Vector2.down * rb2d.velocity.y * (1 - jumpCutMultiplier), ForceMode2D.Impulse); 
        } 

        jumpInputIsReleased = true;
        lastJumpTime = 0;
    }




    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, whatIsGround);
    }

    public void jumpDownInput()
    {
        lastJumpTime = jumpBufferTime;
    }


}
