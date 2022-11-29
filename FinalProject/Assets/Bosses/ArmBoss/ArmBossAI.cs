using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmBossAI : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;
    private PlayerHealth playersHealth;

    [SerializeField]
    private RuntimeAnimatorController[] animations;
    private Animator animationPlayer;

    private bool MovingToPlayer;
    private float timer;
    [SerializeField]
    private bool isFacingRight;
    private float AirTime;
   
    // Start is called before the first frame update
    void Start()
    {
        playersHealth = Player.GetComponent<PlayerHealth>();

        animationPlayer = gameObject.GetComponent<Animator>();
        timer = 0;
        AirTime = 0;

        MovingToPlayer = true;
        isFacingRight = true;
        //transform.Translate(new Vector3(0, 5f, 0) * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {

        // code where boss moves to player and if they in certain distance, swings (attack 2)
        if (MovingToPlayer && timer < 1f)
        {
            if (Vector2.Distance(transform.position, Player.transform.position) > 5f && AirTime < 5f)
            {
                AirTime = 10f;
                gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 20f);
            }
            
            if(Player.transform.position.x > gameObject.transform.position.x && !isFacingRight)
            {
                Flip();
            }
            else if (Player.transform.position.x < gameObject.transform.position.x && isFacingRight)
            {
                Flip();
            }
            animationPlayer.runtimeAnimatorController = animations[1];
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, 2f * Time.deltaTime);
            //Debug.Log(Vector2.Distance(transform.position, Player.transform.position));
            if (Vector2.Distance(transform.position, Player.transform.position) < 1.25f)
            {
                
                playersHealth.TakeDamage(15f);
                animationPlayer.runtimeAnimatorController = animations[2];
                timer = 2f;
            }
        }
        // code where boss moves to player and if they in certain distance, swings (attack 1)

        timer -= Time.deltaTime;
        AirTime -= Time.deltaTime;
    }

    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        isFacingRight = !isFacingRight;
    }
}
