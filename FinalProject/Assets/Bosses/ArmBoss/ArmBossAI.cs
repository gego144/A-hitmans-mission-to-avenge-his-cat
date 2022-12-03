using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmBossAI : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;
    private PlayerHealth playersHealth;

    [SerializeField]
    private RuntimeAnimatorController[] animations;
    private Animator animationPlayer;

    private bool MovingToPlayer;
    private bool punching;
    private float timer;

    [SerializeField]
    private bool isFacingRight;
    private float AirTime;
    private Rigidbody2D rb2d;

    private bool jumpToPlayer;
    private Vector2 lastPlayerLocation;
    private float lastJumpTime;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float jumpSpeed;

    private BoxCollider2D bossCollider;
    private Collider2D playerCollider;
    private ContactFilter2D filter;


    [SerializeField] private GameObject spikes;
    private float spikeTimer;

    private float[] attackTurnTimer;

    public float AiHealth;
    public float maxHealth;
    // Start is called before the first frame update

    void Start()
    {
        maxHealth = AiHealth;
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        bossCollider = gameObject.GetComponent<BoxCollider2D>();
        playerCollider = Player.GetComponent<BoxCollider2D>();
        filter.layerMask = whatIsGround;
        isFacingRight = false;

        playersHealth = Player.GetComponent<PlayerHealth>();

        animationPlayer = gameObject.GetComponent<Animator>();
        timer = 0;

        MovingToPlayer = true;
        punching = false;


        jumpToPlayer = false;
        lastJumpTime = 0;

        spikeTimer = 2f;
        attackTurnTimer = new float[3];

        for(int i = 0; i <= 2; i++)
        {
            attackTurnTimer[i] = Random.Range(5f, 10f);
        }
    }

    // Update is called once per frame
    void Update()
    {


        if (isGrounded() && Player.transform.position.x > gameObject.transform.position.x && !isFacingRight)
        {
            Flip();
        }
        else if (isGrounded() && Player.transform.position.x < gameObject.transform.position.x && isFacingRight)
        {
            Flip();
        }

        // code where boss moves to player and if they in certain distance, swings (attack 2)
        if (attackTurnTimer[0] > 0)
        {
            if (MovingToPlayer && timer < 0)
            {
                
                transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, 6f * Time.deltaTime);
                if (Vector2.Distance(transform.position, Player.transform.position) < 1.8f)
                {
                    punching = true;
                    bossCollider.size = new Vector2(bossCollider.size.x + 0.1f, bossCollider.size.y);
                    timer = 0.9f;
                    StartCoroutine(QueueAnimation(animations[2], animations[0], "swing"));
                }
            }
            attackTurnTimer[0] -= Time.deltaTime;
        }

        // boss jumping attack (attack 4)
        else if (attackTurnTimer[1] > 0)
        {
            if (!jumpToPlayer && isGrounded() && lastJumpTime < 0)
            {
                if (isGrounded())
                {
                    jumpToPlayer = true;
                    rb2d.AddForce(Vector2.up * 200f, ForceMode2D.Impulse);
                }

                lastPlayerLocation = Player.transform.position;
                animationPlayer.runtimeAnimatorController = animations[0];
            }

            if (jumpToPlayer)
            {
                Debug.Log("jump");
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(lastPlayerLocation.x, 0), jumpSpeed * Time.deltaTime);
                lastJumpTime = 5f;
                if (Vector2.Distance(new Vector2(transform.position.x, 0), new Vector2(lastPlayerLocation.x, 0)) < 2.5f)
                {
                    Debug.Log("distance");
                    animationPlayer.runtimeAnimatorController = animations[3];
                    lastJumpTime = 1f;
                    jumpToPlayer = false;
                }
            }
            if (rb2d.velocity.x < 0.5f && !jumpToPlayer && !isGrounded())
            {
                Debug.Log("Force");
                rb2d.AddForce(Vector2.down * 15f * rb2d.mass);
            }
            if (isGrounded() && Vector2.Distance(transform.position, Player.transform.position) < 4f)
            {
                playersHealth.TakeDamage(15f);
            }
            attackTurnTimer[1] -= Time.deltaTime;
        }

        // boss spike attack (special)
        else if(attackTurnTimer[2] > 0)
        {
            if (spikeTimer < 0)
            {
                spikeTimer = 1f;
                Instantiate(spikes, new Vector3(Player.transform.position.x, 14.35f, Player.transform.position.z), gameObject.transform.rotation);
                StartCoroutine(QueueAnimation(animations[4], animations[0], "spikes"));

            }
            attackTurnTimer[2] -= Time.deltaTime;
        }
        else
        {
            if(animationPlayer.runtimeAnimatorController == animations[0])
            {
                animationPlayer.runtimeAnimatorController = animations[1];
            }
            
        }

        if(attackTurnTimer[2] < 0)
        {
            animationPlayer.runtimeAnimatorController = animations[1];
            for (int i = 0; i <= 2; i++)
            {
                attackTurnTimer[i] = Random.Range(5f, 10f);
            }
        }
        Debug.Log(isGrounded());
        //
        
        timer -= Time.deltaTime;
        lastJumpTime -= Time.deltaTime;
        spikeTimer -= Time.deltaTime;
    }

    public bool AiHealthDamage(float damage)
    {
        StartCoroutine(QueueAnimation(animations[5], animations[0], "hurt"));
        AiHealth -= damage;
        return AiHealth <= 0;
    }

    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        isFacingRight = !isFacingRight;
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, whatIsGround);
    }

    IEnumerator QueueAnimation(RuntimeAnimatorController firstClip, RuntimeAnimatorController secondClip, string anim)
    {

        if (anim == "hurt")
        {
            secondClip = animationPlayer.runtimeAnimatorController;
            animationPlayer.runtimeAnimatorController = firstClip;
            yield return new WaitForSeconds(0.167f);
            animationPlayer.runtimeAnimatorController = secondClip;
        }
        else if (anim == "spikes")
        {
            animationPlayer.runtimeAnimatorController = firstClip;
            yield return new WaitForSeconds(0.5f);
            if(attackTurnTimer[2] > 0)
            {
                animationPlayer.runtimeAnimatorController = secondClip;
            } 
        }
        else
        {
            bossCollider.size = new Vector2(bossCollider.size.x - 0.1f, bossCollider.size.y);
            animationPlayer.runtimeAnimatorController = firstClip;
            yield return new WaitForSeconds(0.5f);
            animationPlayer.runtimeAnimatorController = secondClip;
            punching = false;
            yield return new WaitForSeconds(0.5f);
            animationPlayer.runtimeAnimatorController = animations[1];
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            if (punching)
            {
                Debug.Log("hit");
                playersHealth.TakeDamage(15f);
            }
        }
    }

}
