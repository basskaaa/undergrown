using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class scp_Enemy_Ai_Attacking : MonoBehaviour
{
    private scp_Enemy_Ai enemyAI;

    void Start()
    {
        enemyAI = GetComponentInParent<scp_Enemy_Ai>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemyAI._Attacking = true;
            //Debug.Log("Hunt range entered");
        }
    }
    
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemyAI._Attacking = true;
            //Debug.Log("Hunt range entered");
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemyAI._Attacking = false;
            enemyAI._Hunting = true;
        }
    }
}
