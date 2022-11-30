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
    private GameObject isInvisible;
    [SerializeField]
    private float AiHealth;
    [SerializeField]
    private ParticleSystem bloodSplat;
    [SerializeField]
    private RuntimeAnimatorController[] clips;
    private Animator theAnimator;


    void Start()
    {
        isInvisible = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        StartLocation = gameObject.transform.position;
        movingToDestination = true;
        rocketsCreated = new List<GameObject>();
        AiHealth = 70f;
        theAnimator = gameObject.GetComponent<Animator>();
        theAnimator.runtimeAnimatorController = clips[0];
    }

    // Update is called once per frame
    void Update()
    {
        coolDownShotTimer -= Time.deltaTime;

        if (movingToDestination)
        {
            
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, Destination, 5f * Time.deltaTime);
            if (Vector3.Distance(gameObject.transform.position, Destination) < 0.5f)
            {
                movingToDestination = !movingToDestination;
            }
        }
        else if (isInvisible.activeSelf)
        {
            foreach (GameObject flyingRocket in rocketsCreated)
            {
                Destroy(flyingRocket);
            }
        }
        else if(coolDownShotTimer < 0)
        {
            theAnimator.runtimeAnimatorController = clips[1];
            coolDownShotTimer = 10f;
            rocketsCreated.Add(Instantiate(rocket, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.2f, gameObject.transform.position.z), gameObject.transform.rotation));
        }
        else if(coolDownShotTimer < 9.3f && coolDownShotTimer > 0)
        {
            theAnimator.runtimeAnimatorController = clips[2];
        }
    }
    void OnDestroy()
    {
        foreach(GameObject flyingRocket in rocketsCreated)
        {
            Destroy(flyingRocket);
        }
    }
    public bool AiHealthDamage(float damage)
    {
        bloodSplat.Play();
        AiHealth -= damage;
        return AiHealth <= 0;
    }
}
