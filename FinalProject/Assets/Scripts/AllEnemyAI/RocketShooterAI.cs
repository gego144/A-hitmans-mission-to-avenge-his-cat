using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShooterAI : MonoBehaviour
{
    private Vector3 StartLocation;
    public Vector3 Destination;
    private bool movingToDestination;

    [SerializeField]
    private float coolDownShotTimer;
    [SerializeField]
    private GameObject rocket;
    private List<GameObject> rocketsCreated;

    void Start()
    {
        StartLocation = gameObject.transform.position;
        movingToDestination = true;
        rocketsCreated = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        coolDownShotTimer -= Time.deltaTime;

        if (movingToDestination)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, Destination, 5f * Time.deltaTime);
            if(Vector3.Distance(gameObject.transform.position, Destination) < 0.5f)
            {
                movingToDestination = !movingToDestination;
            }
        }
        else if(coolDownShotTimer < 0)
        {
            coolDownShotTimer = 10f;
            rocketsCreated.Add(Instantiate(rocket, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.2f, gameObject.transform.position.z), gameObject.transform.rotation));
        }
    }
    void OnDestroy()
    {
        foreach(GameObject flyingRocket in rocketsCreated)
        {
            Destroy(flyingRocket);
        }
    }
}
