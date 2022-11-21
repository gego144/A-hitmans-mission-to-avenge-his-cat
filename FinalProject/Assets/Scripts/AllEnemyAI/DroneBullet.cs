using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBullet : MonoBehaviour
{
    private GameObject Player;
    private Vector3 PlayerLocation;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerLocation = Player.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, PlayerLocation, 2f * Time.deltaTime);
        if(Vector3.Distance(gameObject.transform.position, PlayerLocation) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            Destroy(gameObject);
        }
        Debug.Log(collision.gameObject.layer);
    }*/

}
