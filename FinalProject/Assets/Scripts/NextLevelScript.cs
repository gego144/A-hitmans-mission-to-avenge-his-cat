using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelScript : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnDestroy()
    {
        switch (gameObject.tag)
        {
            case "ArmBoss":
                SceneManager.LoadScene("parkLevel");
                break;
            case "FlyBoss":
                SceneManager.LoadScene("chineseCityLevel");
                break;
            case "RollBoss":
                SceneManager.LoadScene("StartScene");
                break;
        }
    }
}
