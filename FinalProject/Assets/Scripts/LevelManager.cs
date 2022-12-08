using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static bool isPaused;
    [SerializeField] private GameObject gadgetMenu;
    [SerializeField] private GameObject weaponMenu;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bossTrigger;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject DeadScreen;
    private Animator theAnimator;
    private Vector3 bossRespawnPoint;
    private bool respawning;
    private float playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        respawning = false;
        bossRespawnPoint = bossTrigger.transform.position;
        Time.timeScale = 0;
        isPaused = true;
        theAnimator = player.GetComponent<Animator>();
        StartCoroutine(gameStart());
           
    }

    // Update is called once per frame
    void Update()
    {

        playerHealth = player.GetComponent<PlayerHealth>().health;
        if (playerHealth <= 0 && bossTrigger.activeSelf) {
            DeadScreen.SetActive(true);
            player.GetComponent<SpriteRenderer>().forceRenderingOff = true;
            Time.timeScale = 0;
            
        }
        else if(playerHealth <= 0 && !respawning) {
            theAnimator.Play("Death");
            respawning = true;
            StartCoroutine(Respawn());
        }
    }

    IEnumerator gameStart() {

        while (gadgetMenu.activeSelf || weaponMenu.activeSelf) {
            yield return null;
        }
        Time.timeScale = 1;
        isPaused = false;
        yield return null;
    }

    IEnumerator Respawn() {
        Time.timeScale = 0;
        player.transform.position = new Vector3(bossRespawnPoint.x, bossRespawnPoint.y-3, bossRespawnPoint.z);
        player.GetComponent<PlayerHealth>().health = 100f;
        switch (boss.tag) {
            case "FlyBoss":
                boss.GetComponent<FlyBossAI>().AiHealth = boss.GetComponent<FlyBossAI>().maxHealth;
                break;
            case "ArmBoss":
                boss.GetComponent<ArmBossAI>().AiHealth = boss.GetComponent<ArmBossAI>().maxHealth;
                break;
            case "RollBoss":
                boss.GetComponent<RollBossAI>().AiHealth = boss.GetComponent<RollBossAI>().maxHealth;
                break;
        }
        
        new WaitForSeconds(3);
        Time.timeScale = 1;
        respawning = false;
        yield return null;
    }

}
