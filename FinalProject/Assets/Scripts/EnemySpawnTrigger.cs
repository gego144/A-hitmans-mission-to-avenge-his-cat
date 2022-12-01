using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Vector3 spawnPosition;


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            spawnEnemy(enemy, spawnPosition);
            this.gameObject.SetActive(false);
        }
    }

    public void spawnEnemy(GameObject e, Vector3 p) {
        Instantiate(e, p, e.transform.rotation);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(spawnPosition, new Vector3(enemy.GetComponent<SpriteRenderer>().bounds.size.x, enemy.GetComponent<SpriteRenderer>().bounds.size.y, enemy.GetComponent<SpriteRenderer>().bounds.size.z));
    }
}
