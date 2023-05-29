using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_controlsMenu : MonoBehaviour
{
    [SerializeField] private GameObject controlsMenu;
    [SerializeField] private GameObject settingsMenu;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Back()
    {
        audioSource.Play();
        controlsMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
}
