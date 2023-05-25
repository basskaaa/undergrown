using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class scp_PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public bool InSettings = false;

    public GameObject pauseMenuUI;
    [SerializeField] private GameObject settingsMenu;
    private scp_Player_Manager player;


    private void Start()
    {
        player = FindObjectOfType<scp_Player_Manager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !player._Dead && !InSettings)
        {
            if (GameIsPaused) Resume();
            else Pause();
        }

        if (GameIsPaused) Cursor.lockState = false ? CursorLockMode.None : CursorLockMode.None;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        settingsMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = true ? CursorLockMode.Locked : CursorLockMode.None;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = false ? CursorLockMode.None : CursorLockMode.None;
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Settings()
    {
        InSettings = true;
        settingsMenu.SetActive(true);
        pauseMenuUI.SetActive(false);
    }
}
