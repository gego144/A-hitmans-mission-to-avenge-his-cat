using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBoss : MonoBehaviour
{
    public GameObject blocks;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            blocks.SetActive(true);
            startBoss();
            this.gameObject.SetActive(false);
        }
    }

    private void startBoss() {
        Debug.Log("BOSS STARTED");
    }
}
