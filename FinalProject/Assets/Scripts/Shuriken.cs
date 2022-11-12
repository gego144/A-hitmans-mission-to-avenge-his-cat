using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    private float timer = 2f;
    void Start()
    {
        rb.velocity = transform.right * speed;
    }
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            rb.velocity = new Vector2(0f, 0f);
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, 8f * Time.deltaTime);
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        //Destroy(collision.transform.gameObject);
    }
}
