using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBullet : MonoBehaviour
{
    private GameObject Player;
    private Vector3 PlayerLocation;
    private PlayerHealth health;
    [SerializeField]
    private ParticleSystem explosion;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private AudioSource bombSE;
    private bool exploding;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerLocation = Player.transform.position;
        health = Player.GetComponent<PlayerHealth>();
        exploding = false;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, PlayerLocation, projectileSpeed * Time.deltaTime);
        if(Vector3.Distance(gameObject.transform.position, PlayerLocation) < 0.5f && !exploding)
        {
            StartCoroutine(Explode());
        }
/*        if(Vector3.Distance(gameObject.transform.position, PlayerLocation) < 0.1f)
        {
            Destroy(gameObject);
        }*/
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !exploding)
        {
            health.TakeDamage(15f);
            StartCoroutine(Explode());
        }

    }

    IEnumerator Explode() {
        exploding = true;
        explosion.Play();
        bombSE.Play();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

}
