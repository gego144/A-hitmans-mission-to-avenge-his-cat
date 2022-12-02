using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollBossAI : MonoBehaviour
{
    private GameObject Player;
    private PlayerHealth playersHealth;
    [SerializeField] private float AiHealth;
    private Animator animationPlayer;
    [SerializeField] private RuntimeAnimatorController[] animations;

    [SerializeField] private GameObject bomb;
    private float bombCoolDownTimer;
    private int bombsShot;
    private ArrayList bombs;
    private bool isShooting;

    private Vector2 startLocation;
    [SerializeField] private Vector2 Destination;
    private bool movingToDestination;
    private bool isFacingRight;
    private bool isRun;
    private int fullLaps;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playersHealth = Player.GetComponent<PlayerHealth>();
        animationPlayer = gameObject.GetComponent<Animator>();

        bombCoolDownTimer = 1f;
        bombsShot = 0;
        bombs = new ArrayList();
        isShooting = true;

        startLocation = gameObject.transform.position;
        isFacingRight = true;
        movingToDestination = true;
        isRun = false;
        fullLaps = 0;


    }

    // Update is called once per frame
    void Update()
    {

        if (bombsShot == 3 && !isBombsActive() && !isShooting)
        {
            switchStance();
        }

        if (fullLaps == 3 && !isRun)
        {
            switchStance();
        }

        // bomb shooting code
        if (bombCoolDownTimer <= 0 && bombsShot <= 2 && isShooting)
        {
            bombCoolDownTimer = 1.7f;
            StartCoroutine(QueueAnimation(animations[0], animations[1], "bomb"));
            bombsShot += 1;
            if(bombsShot == 3)
            {
                isShooting = false;
            }

        }

        // running code
        if (isRun && fullLaps <= 3)
        {
            if (movingToDestination)
            {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, Destination, 16f * Time.deltaTime);
                if (Vector2.Distance(gameObject.transform.position, Destination) < 0.2f)
                {
                    movingToDestination = false;
                    Flip();
                }
            }
            else
            {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, startLocation, 16f * Time.deltaTime);
                if (Vector3.Distance(gameObject.transform.position, startLocation) < 0.2f)
                {
                    fullLaps += 1;
                    if(fullLaps == 3)
                    {
                        isRun = false;
                    }
                    movingToDestination = true;
                    Flip();
                }
            }
        }


        bombCoolDownTimer -= Time.deltaTime;
    }
    private bool isBombsActive()
    {
        foreach (GameObject item in bombs)
        {
            if (item != null)
            {
                return true;
            }
        }
        return false;
    }

    private void switchStance()
    {
        if (bombsShot == 3 && !isBombsActive() && !isShooting)
        {
            bombsShot = 0;
            bombs.Clear();
            StartCoroutine(QueueAnimation(animations[2], animations[3], "run"));
        }
        if (fullLaps == 3 && !isRun)
        {
            fullLaps = 0;
            isShooting = true;
        }
    }

    public bool AiHealthDamage(float damage)
    {
        StartCoroutine(QueueAnimation(animations[4], animations[0], "hurt"));
        AiHealth -= damage;
        return AiHealth <= 0;
    }

    IEnumerator QueueAnimation(RuntimeAnimatorController firstClip, RuntimeAnimatorController secondClip, string anim)
    {

        if (anim == "bomb")
        {
            animationPlayer.runtimeAnimatorController = firstClip;
            yield return new WaitForSeconds(0.4f);
            // NEED TO CHANGE Y VALUE TO THE TOP OF BOSS ROOM WHAT EVER IT LOOKS LIKE
            bombs.Add(Instantiate(bomb, new Vector2(Player.transform.position.x, Player.transform.position.y + 5f), Quaternion.identity));
            animationPlayer.runtimeAnimatorController = secondClip;
        }
        else if(anim == "run")
        {
            animationPlayer.runtimeAnimatorController = firstClip;
            yield return new WaitForSeconds(0.5f);
            isRun = true;
            animationPlayer.runtimeAnimatorController = secondClip;
        }
        else if(anim == "hurt")
        {
            secondClip = animationPlayer.runtimeAnimatorController;
            animationPlayer.runtimeAnimatorController = firstClip;
            yield return new WaitForSeconds(0.167f);
            animationPlayer.runtimeAnimatorController = secondClip;
        }
    }

    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        isFacingRight = !isFacingRight;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.name == "Player" && isRun)
        {
            playersHealth.TakeDamage(25f);
            isRun = false;
            StartCoroutine(QueueAnimation(animations[2], animations[3], "run"));
        }
    }
}
