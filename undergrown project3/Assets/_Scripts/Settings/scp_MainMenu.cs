using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scp_MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject newGameMenu;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void NewGame()
    {
        audioSource.Play();
        newGameMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void Settings()
    {
        audioSource.Play();
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void Quit()
    {
        audioSource.Play();
        Debug.Log("Quit");
        Application.Quit();
    }
}
