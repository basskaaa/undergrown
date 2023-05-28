using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_DropKey : MonoBehaviour
{
    private scp_Enemy_AI ai;
    [SerializeField] private GameObject key;

    private void Start()
    {
        ai = GetComponent<scp_Enemy_AI>();
    }

    private void Update()
    {
        if (ai._Dead && key != null)
        {
            key.SetActive(true);
            Vector3 spawnKey = new Vector3 (transform.position.x, transform.position.y + 1, transform.position.z);
            key.transform.position = spawnKey;
        }
    }
}
