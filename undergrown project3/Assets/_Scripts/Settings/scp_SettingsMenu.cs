using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class scp_SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private AudioSource music;

    public AudioMixer audioMixer;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void Back()
    {

        if (mainMenu != null) mainMenu.SetActive(true);
        if (pauseMenu != null) pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }
}
