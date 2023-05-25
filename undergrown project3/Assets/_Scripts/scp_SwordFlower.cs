using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scp_SwordFlower : MonoBehaviour
{
    [SerializeField] private GameObject flowerPromptUI;
    [SerializeField] private scp_Player_Attack attackScript;
    private bool inRange;
    public bool flower = false;
    [SerializeField] private GameObject flowerObj;
    [SerializeField] private GameObject seedObj;


    private void Start()
    {
        attackScript = FindObjectOfType<scp_Player_Attack>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inRange)
        {
            attackScript.CollectFlower();
            Destroy(gameObject);
        }

        if (flower)
        {
            SetFlower();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && flower)
        {
            flowerPromptUI.SetActive(true);
            inRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && flower)
        {
            flowerPromptUI.SetActive(false);
            inRange = false;
        }
    }

    public void SetFlower()
    {
        flowerObj.SetActive(true);
        seedObj.SetActive(false );
    }
}
