using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class StartBoss : MonoBehaviour
{
    public GameObject blocks;
    private GameObject enemies;
    [SerializeField] public GameObject boss;
    [SerializeField] private CinemachineVirtualCamera mainCam;
    [SerializeField] private CinemachineVirtualCamera bossCam;
    [SerializeField] public GameObject bossHealth;
    public bool bossFighting;

    private void Start() {
        bossFighting = false;
        enemies = GameObject.FindGameObjectWithTag("Enemies");
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            blocks.SetActive(true);
            enemies.SetActive(false);
            bossCam.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 10;
            startBoss();
            this.gameObject.SetActive(false);
            
        }
    }

    private void startBoss() {
        Debug.Log("BOSS STARTED");
        bossFighting = true;
        boss.SetActive(true);
        bossHealth.SetActive(true);
    }
}
