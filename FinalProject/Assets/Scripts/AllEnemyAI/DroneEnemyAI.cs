using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneEnemyAI : MonoBehaviour
{
    private GameObject Player;
    private GameObject bullet;
    [SerializeField]
    private GameObject droneBulletPrefab;
    private bool movingToPlayer;
    private Vector3 playerLocation;
    private GameObject isInvisible;
    [SerializeField]
    private float AiHealth;
    [SerializeField]
    private ParticleSystem bloodSplat;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        isInvisible = Player.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        bullet = Instantiate(droneBulletPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.2f, gameObject.transform.position.z), gameObject.transform.rotation);
        movingToPlayer = false;
        AiHealth = 50f;
    }

    // Update is called once per frame
    void Update()
    {
        if(bullet == null && !movingToPlayer && !isInvisible.activeSelf)
        {
            playerLocation = Player.transform.position;
            movingToPlayer = !movingToPlayer;
        }
        if(bullet == null && Vector3.Distance(gameObject.transform.position, playerLocation) < 1f && !isInvisible.activeSelf)
        {
            bullet = Instantiate(droneBulletPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.2f, gameObject.transform.position.z), gameObject.transform.rotation);
            movingToPlayer = !movingToPlayer;
        }

        if (movingToPlayer && !isInvisible.activeSelf)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, playerLocation, 5f * Time.deltaTime);
        }
        
    }
    public bool AiHealthDamage(float damage)
    {
        bloodSplat.Play();
        AiHealth -= damage;
        return AiHealth <= 0;
    }
}
