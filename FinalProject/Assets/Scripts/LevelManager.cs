using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static bool isPaused;
    [SerializeField] private GameObject gadgetMenu;
    [SerializeField] private GameObject weaponMenu;
    [SerializeField] private GameObject player;
    private float playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        isPaused = true;
        StartCoroutine(gameStart());
           
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth = player.GetComponent<PlayerHealth>().health;
        if (playerHealth <= 0) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    IEnumerator gameStart() {

        while (gadgetMenu.activeSelf || weaponMenu.activeSelf) {
            yield return null;
        }
        Time.timeScale = 1;
        isPaused = false;
        yield return null;
    }
}
