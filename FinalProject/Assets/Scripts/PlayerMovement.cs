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


    [Header("Dashing Variables")]
    public bool canDash = true;
    public bool isDashing;
    [SerializeField] private float dashingPower;
    [SerializeField] private float dashingTime;
    [SerializeField] private float dashingCoolDown;



    [Header("Misc")]
    [SerializeField] private bool isFacingRight;
    private Rigidbody2D rb2d;
    [SerializeField] private TrailRenderer tr;
    private Animator theAnimator;
    [SerializeField] private RuntimeAnimatorController[] animations;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        isFacingRight = true;
        theAnimator = gameObject.GetComponent<Animator>();

    }



    void Update()
    {


        lastJumpTime -= Time.deltaTime;
        lastGroundTime -= Time.deltaTime;

        if (isDashing) {
            return;
        }


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


        if (Input.GetButtonDown("Fire3") && canDash) {
            StartCoroutine(Dash());
        }
        if (!LevelManager.isPaused) {
            move();
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

        if (moveHorizontal > 0 && !isFacingRight) {
            Flip();
        }

        if (moveHorizontal < 0 && isFacingRight)
        {
            Flip();
        }


        if (moveHorizontal == 0 && !isJumping)
        {
            theAnimator.runtimeAnimatorController = animations[0];
        }

        if (moveHorizontal != 0 && !isJumping) {
            theAnimator.runtimeAnimatorController = animations[1];
        }



    }


    private void Jump() {
        lastJumpTime = 0;
        lastGroundTime = 0;
        jumpInputIsReleased = false;
        isJumping = true;
        rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        theAnimator.runtimeAnimatorController = animations[2];
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

    private void jumpDownInput()
    {
        lastJumpTime = jumpBufferTime;
    }

    public void rageModeOn()
    {
        speed += 7f;
        jumpForce += 15f;
        gravity += 3f;
        accel += 4f;
        decc -= 2f;
    }
    public void rageModeOff()
    {
        speed -= 7f;
        jumpForce -= 15f;
        gravity -= 3f;
        accel -= 4f;
        decc += 2f;
    }


    private void Flip() {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        isFacingRight = !isFacingRight;
    }


    IEnumerator Dash() {
        canDash = false;
        isDashing = true;
        float originalGravity = rb2d.gravityScale;
        rb2d.gravityScale = 0;
        rb2d.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb2d.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCoolDown);
        canDash = true;
    }

    public bool getFacingDirection()
    {
        return isFacingRight;
    }




}
