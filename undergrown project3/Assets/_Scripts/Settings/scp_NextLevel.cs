using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scp_NextLevel : MonoBehaviour
{
    private int currentLevel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        { 
            currentLevel = SceneManager.GetActiveScene().buildIndex;
             SceneManager.LoadScene(currentLevel + 1);
        }
    }
}
