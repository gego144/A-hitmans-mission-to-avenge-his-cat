using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    private float timer = 2f;
    private bool isFacingRight;
    [SerializeField]
    private PlayerMovement playerMovementObj;
    private Vector3 leftSide;
    private PlayerHealth healthScript;
    [SerializeField] private float healthRestore;
    private float playerHealth;

    void Start()
    {
        playerMovementObj = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        healthScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        healthRestore = 20f;
        leftSide = new Vector3(-1.0f, 0, 0);
        isFacingRight = playerMovementObj.getFacingDirection();
        if (isFacingRight)
        {
            rb.velocity = transform.right * speed;
        }
        else
        {
            rb.velocity = leftSide * speed;
        }
        
    }
    void Update()
    {
        playerHealth = healthScript.health;
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            rb.velocity = new Vector2(0f, 0f);
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, 8f * Time.deltaTime);
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.layer);

        if (collision.gameObject.layer == 3)
        {
            Debug.Log("test");
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == 6)
        {
            bool killedAI = false;
            switch (collision.gameObject.tag)
            {
                case "RoamerEnemyAI":
                    killedAI = collision.gameObject.GetComponent<RoamerEnemyAI>().AiHealthDamage(50f);
                    break;
                case "RocketShooterAI":
                    killedAI = collision.gameObject.GetComponent<RocketShooterAI>().AiHealthDamage(50f);
                    break;
                case "DroneEnemyAI":
                    killedAI = collision.gameObject.GetComponent<DroneEnemyAI>().AiHealthDamage(50f);
                    break;
                case "ArmBoss":
                    killedAI = collision.gameObject.GetComponent<ArmBossAI>().AiHealthDamage(50f);
                    break;
                case "FlyBoss":
                    killedAI = collision.gameObject.GetComponent<FlyBossAI>().AiHealthDamage(50f);
                    break;
                case "RollBoss":
                    killedAI = collision.gameObject.GetComponent<RollBossAI>().AiHealthDamage(50f);
                    break;
            }
            if (killedAI)
            {
                Destroy(collision.gameObject);
                if (playerHealth + healthRestore > 100) {
                    healthScript.health = 100;
                }
                else {
                    healthScript.health += healthRestore;
                }
            }
        }
        else if (collision.tag == "Player")
        {

        }
        //Destroy(collision.transform.gameObject);
    }
}
