using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Vector3 StartLocation;
    public Vector3 Destination;
    private bool movingToDestination;

    public GameObject Player;
    private bool movingToPlayer;
    // Start is called before the first frame update
    void Start()
    {
        StartLocation = gameObject.transform.position;
        movingToDestination = true;
        movingToPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(gameObject.transform.position, Player.transform.position) < 5f)
        {
            movingToPlayer = true;
        }

        if (movingToPlayer)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, Player.transform.position, 3f * Time.deltaTime);
            if(Vector3.Distance(gameObject.transform.position, Player.transform.position) < 1f)
            {
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
}
