using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingRocket : MonoBehaviour
{
    private GameObject player;
    private float aliveTimer;
    private PlayerHealth health;
    [SerializeField] private float rocketHealth;
    [SerializeField] private float rocketSpeed;
    [SerializeField] private AudioSource homingSE;
    [SerializeField] private AudioSource explosionSE;
    private float angle;
    private bool exploding;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        homingSE.Play();
        aliveTimer = 20f;
        health = player.GetComponent<PlayerHealth>();
        exploding = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.up = player.transform.position - transform.position;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, rocketSpeed * Time.deltaTime);
        if(aliveTimer < 0 && !exploding)
        {
            exploding = true;
            StartCoroutine(Explode());
        }
        aliveTimer -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Player" && !exploding) {
            health.TakeDamage(25f);
            StartCoroutine(Explode());
        }
        else if (collision.gameObject.tag == "Terrain" && !exploding) {
            StartCoroutine(Explode());
        }
    }

    IEnumerator Explode() {
        explosionSE.Play();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<TrailRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        homingSE.Pause();
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

}
