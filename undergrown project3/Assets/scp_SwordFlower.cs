using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_SwordFlower : MonoBehaviour
{
    [SerializeField] private GameObject flowerPromptUI;
    [SerializeField] private scp_Player_Attack attackScript;
    private bool inRange;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inRange)
        {
            attackScript.CollectFlower();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            flowerPromptUI.SetActive(true);
            inRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            flowerPromptUI.SetActive(false);
            inRange = false;
        }
    }
}
