using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;

    private Rigidbody2D rb2d;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform ceilingCheck;




   

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");

        rb2d.velocity = new Vector2(moveHorizontal * speed, 0);

        if (Input.GetButton("Jump")) {
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }



    }


}
