using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject StartMenu;
    [SerializeField] private GameObject HowToPlayMenu;
    [SerializeField] private GameObject PlayerDeadMenu;

    public void StartButtonClicked()
    {
        SceneManager.LoadScene("cityLevel");
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

    public void RestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
