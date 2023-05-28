using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_worldFall : MonoBehaviour
{
    private scp_Respawn respawnM;

    private void Start()
    {
        respawnM = FindObjectOfType<scp_Respawn>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            respawnM.Respawn();
        }

        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
    }
}
