using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gadgets : MonoBehaviour
{
    public string gadgetType;
    private float gadgetCooldown;

    // Teleport parts
    public GameObject player;
    public ParticleSystem teleportParticles;

    // Invisibility parts
    public Material invisibilityMaterial;
    
    //Rage parts
    private SpriteRenderer playerDisplay;
    private Color playersColor;
    private PlayerMovement movement;

    // Start is called before the first frame update
    void Start()
    {
        gadgetCooldown = 0f;

        //Rage parts
        playerDisplay = gameObject.GetComponent<SpriteRenderer>();
        playersColor = playerDisplay.color;
        movement = gameObject.GetComponent<PlayerMovement>();

    }
    void Update()
    {
        //Check for mouse click 
        if (Input.GetButtonDown("Fire2") && gadgetCooldown < 0f)
        {
            switch (gadgetType) {
                case "Teleport":
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
                case "Invisibility":
                    gadgetCooldown = 10f;
                    Color color = invisibilityMaterial.color;
                    color.a = 0.5f;
                    invisibilityMaterial.color = color;
                    StartCoroutine(StartTimer(5f));
                    break;
                case "Rage":
                    gadgetCooldown = 10f;
                    Color customColor = new Color(0.4f, 0.9f, 0.7f, 1.0f);
                    playerDisplay.color = customColor;
                    movement.rageModeOn();
                    StartCoroutine(StartTimer(5f));
                    break;
            }
        }
        gadgetCooldown -= Time.deltaTime;
    }

    IEnumerator StartTimer(float length)
    {
        switch (gadgetType)
        {
            case "Invisibility":
                Color color = invisibilityMaterial.color;
                yield return new WaitForSeconds(length);
                color.a = 1f;
                invisibilityMaterial.color = color;
                break;
            case "Rage":
                yield return new WaitForSeconds(length);
                movement.rageModeOff();
                playerDisplay.color = playersColor;
                break;
        }
    }
}
