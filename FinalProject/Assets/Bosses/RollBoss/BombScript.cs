using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    private GameObject Player;
    private Rigidbody2D rb2d;
    private Animator animationPlayer;
    [SerializeField] private RuntimeAnimatorController[] animations;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;
    private bool setTimer;
    private BoxCollider2D theCollider;
    private float startTimer;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        theCollider = gameObject.GetComponent<BoxCollider2D>();
        animationPlayer = gameObject.GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
        setTimer = false;
        startTimer = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGrounded())
        {
            rb2d.AddForce(Vector2.down * 1.5f * rb2d.mass);
        }
        else if(isGrounded() && !setTimer || startTimer < 0)
        {
            setTimer = true;
            
            StartCoroutine(QueueAnimation(animations[0], animations[1])); 
        }
        startTimer -= Time.deltaTime;
    }

    IEnumerator QueueAnimation(RuntimeAnimatorController firstClip, RuntimeAnimatorController secondClip)
    {
        yield return new WaitForSeconds(1f);
        animationPlayer.runtimeAnimatorController = firstClip;
        yield return new WaitForSeconds(0.5f);
        animationPlayer.runtimeAnimatorController = secondClip;
        theCollider.isTrigger = true;
        gameObject.transform.localScale = new Vector3(10f, 10f);
        rb2d.bodyType = RigidbodyType2D.Kinematic;
        theCollider.size = new Vector2(theCollider.size.x, theCollider.size.y - 0.3f);
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.9f);
        if(Vector2.Distance(gameObject.transform.position, Player.transform.position) <= 2.5f)
        {
            Player.GetComponent<PlayerHealth>().TakeDamage(40f);
        }
        yield return new WaitForSeconds(0.667f);
        Destroy(gameObject);
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, whatIsGround);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.name == "Player")
        {
            Debug.Log(collision.transform.name);
        }
        
    }
}
