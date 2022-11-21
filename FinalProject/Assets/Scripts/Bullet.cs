using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    private float timer = 2f;
    private bool isFacingRight;
    [SerializeField]
    private PlayerMovement playerMovementObj;
    private Vector3 leftSide;

    void Start()
    {
        playerMovementObj = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        leftSide = new Vector3(-1.0f, 0, 0);
        isFacingRight = playerMovementObj.getFacingDirection();
        if (isFacingRight)
        {
            rb.velocity = transform.right * speed;
        }
        else
        {
            rb.velocity = leftSide * speed;
        }
    }
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            Destroy(gameObject);
        }
    }
    

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3)
        {
            Destroy(gameObject);
        }
        else if (collision.tag == "Player")
        {

        }
        else
        {
            Destroy(gameObject);
            //Destroy(collision.transform.gameObject);
        }

    }
}
