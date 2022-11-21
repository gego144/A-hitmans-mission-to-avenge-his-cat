using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeSinceLastShot;
    public float fireRate;
    public float maxDistance;
    public GameObject projectile;

    private bool CanShoot()
    {
        if (Time.time >= timeSinceLastShot)
        {
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (CanShoot())
            {
                Instantiate(projectile, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.2f, gameObject.transform.position.z), gameObject.transform.rotation);
                timeSinceLastShot = Time.time + fireRate;
            }
        }
    }
}