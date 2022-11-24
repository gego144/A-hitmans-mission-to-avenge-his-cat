using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamerEnemyAI : MonoBehaviour
{
    private Vector3 StartLocation;
    public Vector3 Destination;
    private bool movingToDestination;

    private GameObject Player;
    private bool movingToPlayer;
    private PlayerHealth health;
    [SerializeField]
    private GameObject isInvisible;
    [SerializeField]
    private float AiHealth;
    [SerializeField]
    private ParticleSystem bloodSplat;

    void Start()
    {
        StartLocation = gameObject.transform.position;
        Player = GameObject.FindGameObjectWithTag("Player");
        movingToDestination = true;
        movingToPlayer = false;
        health = Player.GetComponent<PlayerHealth>();
        AiHealth = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(gameObject.transform.position, Player.transform.position) < 5f)
        {
            movingToPlayer = true;
        }
        else
        {
            movingToPlayer = false;
        }

        if (movingToPlayer && !isInvisible.activeSelf)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, Player.transform.position, 3f * Time.deltaTime);
            if(Vector3.Distance(gameObject.transform.position, Player.transform.position) < 0.8f)
            {
                health.TakeDamage(20f);
                movingToPlayer = false;
            }
        }
        else if (movingToDestination)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, Destination, 5f * Time.deltaTime);
        }
        else
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, StartLocation, 5f * Time.deltaTime);
        }

        if (Vector3.Distance(gameObject.transform.position, Destination) < 0.1f || Vector3.Distance(gameObject.transform.position, StartLocation) < 0.1f)
        {
            movingToDestination = !movingToDestination;
        }
    }

    public bool AiHealthDamage(float damage)
    {
        bloodSplat.Play();
        AiHealth -= damage;
        return AiHealth <= 0;
    }

}
