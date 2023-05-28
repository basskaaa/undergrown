using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scp_KeyInteract : MonoBehaviour
{
    [SerializeField] private GameObject keyPromptUI;
    private bool inRange;
    public bool doorOpen = false;
    [SerializeField] private GameObject doorOpenObj;
    [SerializeField] private GameObject doorClosedObj;
    [SerializeField] private GameObject doorOpenSound;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inRange)
        {
            SetDoorOpen();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !doorOpen)
        {
            keyPromptUI.SetActive(true);
            inRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !doorOpen)
        {
            keyPromptUI.SetActive(false);
            inRange = false;
        }
    }

    public void SetDoorOpen()
    {
        GameObject clone = (GameObject)Instantiate(doorOpenSound, doorOpenObj.transform.position, Quaternion.identity);
        Destroy(clone, 1.0f);
        doorOpenObj.SetActive(true);
        doorClosedObj.SetActive(false);
        keyPromptUI.SetActive(false);
        doorOpen = true;
    }
}
