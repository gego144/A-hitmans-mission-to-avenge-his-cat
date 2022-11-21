using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingRocket : MonoBehaviour
{
    private GameObject player;
    private float aliveTimer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        aliveTimer = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, 2f * Time.deltaTime);
        aliveTimer -= Time.deltaTime;
        if(aliveTimer < 0)
        {
            Destroy(gameObject);
        }
    }
}
