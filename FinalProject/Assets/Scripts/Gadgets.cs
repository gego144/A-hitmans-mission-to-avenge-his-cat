using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gadgets : MonoBehaviour
{
    public string gadgetType;
    private float gadgetCooldown;
    [SerializeField]
    private TextMeshProUGUI gadgetReady;

    // Teleport parts
    public GameObject player;
    public ParticleSystem teleportParticles;

    // Invisibility parts
    [SerializeField]
    private SpriteRenderer invisibilityMaterial;
    private GameObject currentlyInvisible;
    private Color lowerAlpha;
    
    //Rage parts
    private SpriteRenderer playerDisplay;
    private Color playersColor;
    private PlayerMovement movement;
    [SerializeField]
    private GameObject currentlyRaged;

    // Start is called before the first frame update
    void Start()
    {
        gadgetCooldown = 0f;
        //invisible parts
        currentlyInvisible = player.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;

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
            Debug.Log(gadgetType);
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
                    currentlyInvisible.SetActive(true);
                    gadgetCooldown = 10f;
                    lowerAlpha = invisibilityMaterial.color;
                    lowerAlpha.a = 0.5f;
                    invisibilityMaterial.color = lowerAlpha;
                    StartCoroutine(StartTimer(5f));
                    break;
                case "Rage":
                    currentlyRaged.SetActive(true);
                    gadgetCooldown = 10f;
                    Color customColor = new Color(0.4f, 0.9f, 0.7f, 1.0f);
                    playerDisplay.color = customColor;
                    movement.rageModeOn();
                    StartCoroutine(StartTimer(5f));
                    break;
            }
        }
        if(gadgetCooldown < 0f)
        {
            gadgetReady.text = "Gadget: Ready";
        }
        else
        {
            gadgetReady.text = "Gadget: Not Ready";
        }
        gadgetCooldown -= Time.deltaTime;
    }

    IEnumerator StartTimer(float length)
    {
        switch (gadgetType)
        {
            case "Invisibility":
                
                yield return new WaitForSeconds(length);
                lowerAlpha.a = 1f;
                invisibilityMaterial.color = lowerAlpha;
                currentlyInvisible.SetActive(false);
                break;
            case "Rage":
                yield return new WaitForSeconds(length);
                movement.rageModeOff();
                playerDisplay.color = playersColor;
                currentlyRaged.SetActive(false);
                break;
        }
    }
}
