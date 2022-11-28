using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingRocket : MonoBehaviour
{
    private GameObject player;
    private float aliveTimer;
    private PlayerHealth health;
    private float angle;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        aliveTimer = 20f;
        health = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.up = player.transform.position - transform.position;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, 2f * Time.deltaTime);
        aliveTimer -= Time.deltaTime;
        if(aliveTimer < 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            health.TakeDamage(25f);
            Destroy(gameObject);
        }

    }
}
