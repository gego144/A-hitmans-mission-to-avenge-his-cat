using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBossRocket : MonoBehaviour
{
    private GameObject player;
    private float aliveTimer;
    private PlayerHealth health;
    private float rocketSpeed;
    [SerializeField] private AudioSource missleSE;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        aliveTimer = 20f;
        health = player.GetComponent<PlayerHealth>();
        rocketSpeed = Random.Range(2.5f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.up = player.transform.position - transform.position;
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, player.transform.position, rocketSpeed * Time.deltaTime);
        aliveTimer -= Time.deltaTime;
        if (aliveTimer < 0)
        {
            StartCoroutine(Explode());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            health.TakeDamage(25f);
            StartCoroutine(Explode());
        }
        else if (collision.gameObject.tag == "Terrain")
        {
            StartCoroutine(Explode());
        }
    }


    IEnumerator Explode() {
        missleSE.Play();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}

