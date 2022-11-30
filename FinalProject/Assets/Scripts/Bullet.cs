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
    private Vector3 leftSide;
    private float damage;

    void Start()
    {
        playerMovementObj = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
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
            bool killedAI = false;
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
                    Debug.Log("ok");
                    killedAI = collision.gameObject.GetComponent<ArmBossAI>().AiHealthDamage(50f);
                    break;
            }
            if (killedAI)
            {
                Destroy(collision.gameObject);
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
