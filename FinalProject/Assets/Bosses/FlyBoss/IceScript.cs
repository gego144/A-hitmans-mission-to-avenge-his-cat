using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceScript : MonoBehaviour
{
    private GameObject Player;
    private GameObject FlyBoss;
    private Vector2 PlayerLocation;
    [SerializeField] private Rigidbody2D rb2d;
    Vector3 direction;
    private bool canTouchPlayer;
    [SerializeField] private float distance;
    [SerializeField] private AudioSource iceSE;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        FlyBoss = GameObject.FindGameObjectWithTag("FlyBoss");
        PlayerLocation = new Vector2(Player.transform.position.x, Player.transform.position.y);

        Vector3 direction = Player.transform.position - transform.position;
        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);

        canTouchPlayer = true;
        iceSE.Play();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, new Vector2(PlayerLocation.x, PlayerLocation.y), 5f * Time.deltaTime);

        if(Vector2.Distance(gameObject.transform.position, PlayerLocation) < 0.1f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(canTouchPlayer && collision.transform.name == "Player")
        {
            
            Player.GetComponent<PlayerHealth>().TakeDamage(20f);
            canTouchPlayer = false;
        }
    }

}
