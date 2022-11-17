using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //MovementVars
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private float accel;
    [SerializeField] private float decc;
    [SerializeField] private float velPower;

    //JumpingVars
    private float jumpCoyoteTime = 0.5f;
    private float jumpBufferTime = 0.5f;
    private float lastGroundTime;
    private float lastJumpTime;
    public bool isJumping;


    // groundCheckVars
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;


    private Rigidbody2D rb2d;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }





    void Update()
    {

        if (isGrounded())
        {

            lastGroundTime = jumpCoyoteTime;
        }


        if ((isGrounded()  || lastGroundTime > 0) && Input.GetButtonDown("Jump")) {
            Debug.Log(lastGroundTime);       
            isJumping = true;
            Jump();
        }


        move();

        lastJumpTime -= Time.deltaTime;
        lastGroundTime -= Time.deltaTime;

    }

    private void FixedUpdate()
    {
        if (isJumping) {
            isJumping = false;
            lastGroundTime = 0;
        }
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
        Debug.Log("hello");
        Debug.Log(isJumping);
        rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        lastJumpTime = jumpBufferTime;
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, whatIsGround);
    }



}
