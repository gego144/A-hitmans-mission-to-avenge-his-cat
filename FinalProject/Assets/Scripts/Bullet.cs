using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    private float timer = 2f;
    private bool isFacingRight;
    [SerializeField]
    private PlayerMovement playerMovementObj;
    private PlayerHealth healthScript;
    private float playerHealth;
    private Vector3 leftSide;
    private float damage;
    [SerializeField] private float healthRestore;
    [SerializeField] private AudioSource hitSE;

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
            Destroy(gameObject);
        }
    }
    

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3)
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == 6)
        {
            hitSE.Play();
            bool killedAI = false;
            Destroy(gameObject);
            switch (collision.gameObject.tag)
            {
                case "RoamerEnemyAI":
                    killedAI = collision.gameObject.GetComponent<RoamerEnemyAI>().AiHealthDamage(damage);
                    break;
                case "RocketShooterAI":
                    killedAI = collision.gameObject.GetComponent<RocketShooterAI>().AiHealthDamage(damage);
                    break;
                case "DroneEnemyAI":
                    killedAI = collision.gameObject.GetComponent<DroneEnemyAI>().AiHealthDamage(damage);
                    break;
                case "ArmBoss":
                    killedAI = collision.gameObject.GetComponent<ArmBossAI>().AiHealthDamage(damage);
                    break;
                case "FlyBoss":
                    killedAI = collision.gameObject.GetComponent<FlyBossAI>().AiHealthDamage(damage);
                    break;
                case "RollBoss":
                    killedAI = collision.gameObject.GetComponent<RollBossAI>().AiHealthDamage(50f);
                    break;
            }
            if (killedAI)
            {
                
                Destroy(collision.gameObject);
                if(playerHealth + healthRestore > 100) {
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
        else
        {
        }

    }
    public void setDamage(float damage)
    {
        this.damage = damage;
    }
}
