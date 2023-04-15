using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_Player_Damage : MonoBehaviour
{
    [SerializeField] private scp_Enemy_Ai enemyAI;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("You died");
        }
    }    
    
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("You died");
        }
    }
}
