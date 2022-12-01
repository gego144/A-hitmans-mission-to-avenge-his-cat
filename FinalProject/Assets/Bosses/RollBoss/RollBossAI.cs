using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollBossAI : MonoBehaviour
{
    private GameObject Player;
    private Animator animationPlayer;
    [SerializeField] private RuntimeAnimatorController[] animations;

    [SerializeField] private GameObject bomb;
    private float bombCoolDownTimer;
    private int bombsShot;

    private Vector2 startLocation;
    [SerializeField] private Vector2 Destination;
    private bool movingToDestination;
    private bool isFacingRight;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        animationPlayer = gameObject.GetComponent<Animator>();
        bombCoolDownTimer = 1f;
        bombsShot = 0;
        startLocation = gameObject.transform.position;
        isFacingRight = true;
        movingToDestination = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*if(bombCoolDownTimer <= 0 && bombsShot <= 7)
        {
            bombCoolDownTimer = 1.7f;
            StartCoroutine(QueueAnimation(animations[0], animations[1], "bomb"));
            bombsShot += 1;
        }*/


        if (movingToDestination)
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, Destination, 12f * Time.deltaTime);
            if (Vector2.Distance(gameObject.transform.position, Destination) < 0.2f)
            {
                movingToDestination = false;
                Flip();
            }
        }
        else
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, startLocation, 12f * Time.deltaTime);
            if (Vector3.Distance(gameObject.transform.position, startLocation) < 0.2f)
            {
                movingToDestination = true;
                Flip();
            }
        }


        bombCoolDownTimer -= Time.deltaTime;
    }

    IEnumerator QueueAnimation(RuntimeAnimatorController firstClip, RuntimeAnimatorController secondClip, string anim)
    {

        if (anim == "bomb")
        {
            animationPlayer.runtimeAnimatorController = firstClip;
            yield return new WaitForSeconds(0.4f);
            Instantiate(bomb, new Vector2(Player.transform.position.x, Player.transform.position.y + 5f), Quaternion.identity);
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
}
