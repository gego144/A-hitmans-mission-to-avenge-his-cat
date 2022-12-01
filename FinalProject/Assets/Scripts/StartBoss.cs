using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBoss : MonoBehaviour
{
    public GameObject blocks;
    private GameObject enemies;

    private void Start() {
        enemies = GameObject.FindGameObjectWithTag("Enemies");
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            blocks.SetActive(true);
            enemies.SetActive(false);
            startBoss();
            this.gameObject.SetActive(false);
        }
    }

    private void startBoss() {
        Debug.Log("BOSS STARTED");
    }
}
