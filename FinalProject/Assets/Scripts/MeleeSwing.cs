using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSwing : MonoBehaviour
{
    public float swingRange = 0.5f;
    public LayerMask enemyLayers;

    public float swingRate = 0.5f;
    float nextAttackTime = 0f;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

        if(Time.time >= nextAttackTime) { 
            if (Input.GetButtonDown("Fire1"))
            {
                Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(gameObject.transform.position, swingRange, enemyLayers);
                foreach(Collider2D enemy in enemiesHit)
                {
                    if (enemy.gameObject.layer == 6)
                    {
                        bool killedAI = false;
                        switch (enemy.gameObject.tag)
                        {
                            case "RoamerEnemyAI":
                                killedAI = enemy.gameObject.GetComponent<RoamerEnemyAI>().AiHealthDamage(50f);
                                break;
                            case "RocketShooterAI":
                                killedAI = enemy.gameObject.GetComponent<RocketShooterAI>().AiHealthDamage(50f);
                                break;
                            case "DroneEnemyAI":
                                killedAI = enemy.gameObject.GetComponent<DroneEnemyAI>().AiHealthDamage(50f);
                                break;
                            case "ArmBoss":
                                killedAI = enemy.gameObject.GetComponent<ArmBossAI>().AiHealthDamage(50f);
                                break;
                            case "FlyBoss":
                                killedAI = enemy.gameObject.GetComponent<FlyBossAI>().AiHealthDamage(50f);
                                break;
                        }
                        if (killedAI)
                        {
                            Destroy(enemy.gameObject);
                        }
                    }
                }
                nextAttackTime = Time.time + swingRate;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, swingRange);
    }
}
