using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardContact : MonoBehaviour
{
    private GameObject player;
    private PlayerHealth health;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        health = player.GetComponent<PlayerHealth>();
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") {
            health.TakeDamage(100f);
        }
    }
}
