using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject StartMenu;
    [SerializeField] GameObject HowToPlayMenu;

    public void StartButtonClicked()
    {
        SceneManager.LoadScene("parkLevel");
    }

    public void HowToPlayButtonClicked()
    {
        StartMenu.SetActive(false);
        HowToPlayMenu.SetActive(true);
    }

    public void QuitButtonClicked()
    {
        Application.Quit();
    }
}
