using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_setPlants : MonoBehaviour
{
    [SerializeField] private GameObject[] plants;
    [SerializeField] private GameObject plantUi;
    [SerializeField] private GameObject deathUi;
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject endGameUi;
    [SerializeField] private GameObject endGameParticles;
    [SerializeField] private GameObject endGameSound;
    [SerializeField] private GameObject music;
    [SerializeField] private scp_Player_Manager manager;

    private bool inRange;

    public bool endGame;

    void Start()
    {
        manager = FindObjectOfType<scp_Player_Manager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            plantUi.SetActive(true);
            inRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            plantUi.SetActive(false);
            inRange = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && inRange) 
        {
            endGame = true;
            setPlants();
            setPlayerDeath();
        }
    }

    private void setPlants()
    {
        foreach (var plant in plants)
        {
            plant.SetActive(true);
        }
    }

    private void setPlayerDeath()
    {
        manager._Dead = true;
        plantUi.SetActive(false);
        hud.SetActive(false);
        Destroy(deathUi);
        Destroy(music);
        endGameUi.SetActive(true);
        GameObject clone = (GameObject)Instantiate(endGameParticles, transform.position, Quaternion.identity);
        GameObject clone1 = (GameObject)Instantiate(endGameSound, transform.position, Quaternion.identity);
        Destroy(clone, 5.0f);
        Destroy(clone1, 5.0f);
    }
}
