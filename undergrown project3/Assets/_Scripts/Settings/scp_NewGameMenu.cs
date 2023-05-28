using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class scp_NewGameMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject newGameMenu;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Campaign()
    {
        audioSource.Play();
        Cursor.lockState = true ? CursorLockMode.Locked : CursorLockMode.None;
        SceneManager.LoadScene(2);
    }

    public void Arena()
    {
        audioSource.Play();
        Cursor.lockState = true ? CursorLockMode.Locked : CursorLockMode.None;
        SceneManager.LoadScene(1);
    }

    public void Back()
    {
        audioSource.Play();
        if (mainMenu != null) mainMenu.SetActive(true);
        newGameMenu.SetActive(false);
    }
}
