using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gadgets : MonoBehaviour
{
    private string gadgetType;
    public GameObject player;
    public ParticleSystem teleportParticles;
    public Material invisibilityMaterial;
    private float gadgetCooldown;
    private SpriteRenderer playerDisplay;
    // Start is called before the first frame update
    void Start()
    {
        gadgetType = "teleport";
        gadgetCooldown = 0f;
        playerDisplay = gameObject.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        //Check for mouse click 
        if (Input.GetButtonDown("Fire2") && gadgetCooldown < 0f)
        {
            switch (gadgetType) {
                case "teleport":
                    gadgetCooldown = 5f;
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                    if (hit.collider == null)
                    {
                        teleportParticles.Play();
                        Vector3 clickLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        clickLocation.z += 10;
                        player.transform.position = clickLocation;
                    }
                    break;
                case "invisibility":
                    gadgetCooldown = 20f;
                    Color color = invisibilityMaterial.color;
                    color.a = 0.5f;
                    invisibilityMaterial.color = color;
                    StartCoroutine(StartTimer(5f));
                    break;
                case "rage":
                    gadgetCooldown = 20f;

                    break;
            }
        }
        gadgetCooldown -= Time.deltaTime;
    }

    IEnumerator StartTimer(float length)
    {
        switch (gadgetType)
        {
            case "teleport":
                break;
            case "invisibility":
                Color color = invisibilityMaterial.color;
                yield return new WaitForSeconds(length);
                color.a = 1f;
                invisibilityMaterial.color = color;
                break;
            case "rage":
                break;
        }
    }
}
