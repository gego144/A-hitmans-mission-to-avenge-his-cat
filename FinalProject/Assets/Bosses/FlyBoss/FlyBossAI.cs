using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlyBossAI : MonoBehaviour
{
    [SerializeField]
    private RuntimeAnimatorController[] animations;
    private Animator animationPlayer;

    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject IceBlast;
    [SerializeField] private Transform weaponTip;
    [SerializeField] private TextMeshProUGUI bossHealth;
    private bool isFacingRight;

    private float shootTimer;
    private int shotsTaken;
    private Vector2 StartLocation;
    private Vector2 Destination;
    private bool shooting;
    private bool movingToDestination;

    [SerializeField] private GameObject Rocket;
    [SerializeField] private Transform flyingBoardTip;
    private float rocketShotTimer;
    private bool shootingRocket;
    private int rocketAmountShot;
    private bool rocketBarrageOn;

    private float[] attackTurnTimer;
    public float maxHealth;
    [SerializeField] public float AiHealth;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = AiHealth;
        shotsTaken = 0;
        isFacingRight = true;
        animationPlayer = gameObject.GetComponent<Animator>();

        // Ice shot variables
        shootTimer = 0;
        StartLocation = gameObject.transform.position;
        Destination = new Vector2(192.61f, 37.91f);
        shooting = false;
        movingToDestination = true;

        rocketShotTimer = 0.5f;
        shootingRocket = false;

        attackTurnTimer = new float[2];
        for (int i = 0; i <= 1; i++)
        {
            attackTurnTimer[i] = UnityEngine.Random.Range(5f, 10f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        bossHealth.text = Convert.ToString(AiHealth);
        if (Player.transform.position.x > gameObject.transform.position.x && !isFacingRight)
        {
            Flip();
        }
        else if (Player.transform.position.x < gameObject.transform.position.x && isFacingRight)
        {
            Flip();
        }

        if (attackTurnTimer[0] > 0)
        {
            if (rocketShotTimer < 0 && !shootingRocket)
            {
                rocketBarrageOn = true;
                Instantiate(Rocket, flyingBoardTip.position, Quaternion.identity);
                StartCoroutine(QueueAnimation(animations[3], animations[2], "rocket"));
                rocketAmountShot += 1;

                if (rocketAmountShot == 3)
                {
                    rocketBarrageOn = false;
                    rocketAmountShot = 0;
                    rocketShotTimer = 3f;
                }
            }

            if (!rocketBarrageOn)
            {
                if (movingToDestination)
                {
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, Destination, 4f * Time.deltaTime);
                    if (Vector3.Distance(gameObject.transform.position, Destination) < 0.2f)
                    {
                        movingToDestination = false;
                    }
                }
                else
                {
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, StartLocation, 4f * Time.deltaTime);
                    if (Vector3.Distance(gameObject.transform.position, StartLocation) < 0.2f)
                    {
                        movingToDestination = true;
                    }
                }
            }
            attackTurnTimer[0] -= Time.deltaTime;
        }
        else if (attackTurnTimer[1] > 0)
        {
            if (shootTimer < 0)
            {
                if (shotsTaken <= 5)
                {
                    shotsTaken += 1;
                    shootTimer = 0.5f;
                    StartCoroutine(QueueAnimation(animations[1], animations[2], "shoot"));
                    Instantiate(IceBlast, weaponTip.position, Quaternion.identity);
                }
                else
                {
                    shotsTaken = 0;
                    shootTimer = 2f;
                }

            }
            if (!shooting)
            {
                if (movingToDestination)
                {
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, Destination, 4f * Time.deltaTime);
                    if (Vector3.Distance(gameObject.transform.position, Destination) < 0.2f)
                    {
                        movingToDestination = false;
                    }
                }
                else
                {
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, StartLocation, 4f * Time.deltaTime);
                    if (Vector3.Distance(gameObject.transform.position, StartLocation) < 0.2f)
                    {
                        movingToDestination = true;
                    }
                }
            }
            attackTurnTimer[1] -= Time.deltaTime;
        }
        if (attackTurnTimer[1] < 0)
        {
            for (int i = 0; i <= 1; i++)
            {
                attackTurnTimer[i] = UnityEngine.Random.Range(5f, 10f);
            }
        }

        shootTimer -= Time.deltaTime;
        rocketShotTimer -= Time.deltaTime;
    }

    public bool AiHealthDamage(float damage)
    {
        Debug.Log("called");
        StartCoroutine(QueueAnimation(animations[4], animations[0], "hurt"));
        AiHealth -= damage;
        if (AiHealth <= 0)
        {
            SceneManager.LoadScene("chineseCityLevel");
        }
        return AiHealth <= 0;
    }

    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        isFacingRight = !isFacingRight;
    }

    IEnumerator QueueAnimation(RuntimeAnimatorController firstClip, RuntimeAnimatorController secondClip, string anim)
    {

        if (anim == "shoot")
        {
            shooting = true;
            animationPlayer.runtimeAnimatorController = firstClip;
            yield return new WaitForSeconds(0.5f);
            if (shotsTaken > 5)
            {
                shooting = false;
                animationPlayer.runtimeAnimatorController = secondClip;
            }

        }
        else if (anim == "rocket")
        {
            shootingRocket = true;
            animationPlayer.runtimeAnimatorController = firstClip;
            yield return new WaitForSeconds(0.5f);
            shootingRocket = false;
            animationPlayer.runtimeAnimatorController = secondClip;
        }

        else if (anim == "hurt")
        {
            Debug.Log("HIT");
            secondClip = animationPlayer.runtimeAnimatorController;
            animationPlayer.runtimeAnimatorController = firstClip;
            yield return new WaitForSeconds(0.167f);
            animationPlayer.runtimeAnimatorController = secondClip;
        }
    }
}