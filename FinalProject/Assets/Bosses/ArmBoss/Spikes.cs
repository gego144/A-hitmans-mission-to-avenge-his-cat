using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private GameObject Player;
    private Animator animationPlayer;
    [SerializeField] private float spikeSpeed;
    private float deathTimer;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        animationPlayer = gameObject.GetComponent<Animator>();
        animationPlayer.speed = spikeSpeed;
        deathTimer = 0.5f;

    }

    // Update is called once per frame
    void Update()
    {
        if(deathTimer < 0)
        {
            Destroy(gameObject);
        }
        deathTimer -= Time.deltaTime;
        animationPlayer.speed = spikeSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Player.GetComponent<PlayerHealth>().TakeDamage(10f);
        }
    }
}
