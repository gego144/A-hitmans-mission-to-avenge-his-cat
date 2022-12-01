using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamerEnemyAI : MonoBehaviour
{
    private Vector3 StartLocation;
    public Vector3 Destination;
    private bool movingToDestination;

    private GameObject Player;
    public bool movingToPlayer;
    private PlayerHealth health;
    private GameObject isInvisible;
    [SerializeField]
    private float AiHealth;
    [SerializeField]
    private ParticleSystem bloodSplat;
    [SerializeField]
    private RuntimeAnimatorController[] clips;
    [SerializeField]
    private bool isFacingRight;
    private Animator theAnimator;
    private float attackTimer = 0.7f;
    [SerializeField] private float detectionDistance;
    [SerializeField] private float safeDistance;
    private float endTimer = 0.25f;

    void Start()
    {
        StartLocation = gameObject.transform.position;
        Player = GameObject.FindGameObjectWithTag("Player");
        isInvisible = Player.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        movingToDestination = true;
        movingToPlayer = false;
        health = Player.GetComponent<PlayerHealth>();
        AiHealth = 100f;
        theAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(gameObject.transform.position, Player.transform.position) < detectionDistance)
        {
            movingToPlayer = true;

        }

        if (movingToPlayer && !isInvisible.activeSelf)
        {
            // Make sure he is walking facing in the correct direction
            if(Player.transform.position.x < gameObject.transform.position.x && isFacingRight)
            {
                Flip();
            }
            else if (Player.transform.position.x > gameObject.transform.position.x && !isFacingRight)
            {
                Flip();
            }
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, Player.transform.position, 2.3f * Time.deltaTime);
            if(Vector3.Distance(gameObject.transform.position, Player.transform.position) < 0.25*detectionDistance && attackTimer <= 0)
            {
                attackTimer = 1f;
                StartCoroutine(QueueAnimation(clips[1], clips[0]));
                health.TakeDamage(20f);
                movingToPlayer = false;
            }
        }
        else if (movingToDestination)
        {
            // Make sure he is walking facing in the correct direction
            if (Destination.x < gameObject.transform.position.x && isFacingRight)
            {
                Flip();
            }
            else if (Destination.x > gameObject.transform.position.x && !isFacingRight)
            {
                Flip();
            }
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, Destination, 2f * Time.deltaTime);
        }
        else
        {
            // Make sure he is walking facing in the correct direction
            if (StartLocation.x < gameObject.transform.position.x && isFacingRight)
            {
                Flip();
            }
            else if (StartLocation.x > gameObject.transform.position.x && !isFacingRight)
            {
                Flip();
            }
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, StartLocation, 2f * Time.deltaTime);
        }

        if (endTimer <= 0 && !LevelManager.isPaused && (Vector3.Distance(gameObject.transform.position, Destination) < 0.1f || Vector3.Distance(gameObject.transform.position, StartLocation) < 0.1f))
        {
            Flip();
            movingToDestination = !movingToDestination;
            endTimer = 0.25f;
        }
        attackTimer -= Time.deltaTime;
        endTimer -= Time.deltaTime;

        if(Vector3.Distance(gameObject.transform.position, Player.transform.position) >= safeDistance) {
            movingToPlayer = false;
        }

    }

    public bool AiHealthDamage(float damage)
    {
        bloodSplat.Play();
        AiHealth -= damage;
        return AiHealth <= 0;
    }

    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        isFacingRight = !isFacingRight;
    }

    IEnumerator QueueAnimation(RuntimeAnimatorController firstClip, RuntimeAnimatorController secondClip) {

        theAnimator.runtimeAnimatorController = firstClip;
        yield return new WaitForSeconds(0.7f);
        theAnimator.runtimeAnimatorController = secondClip;
    }

}

