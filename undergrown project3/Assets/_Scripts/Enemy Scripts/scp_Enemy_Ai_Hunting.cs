using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class scp_Enemy_Ai_Hunting : MonoBehaviour
{
    private scp_Enemy_Ai enemyAI;

    [SerializeField] private float waitToEndHuntTime = 4f;

    void Start()
    {
        enemyAI = GetComponentInParent<scp_Enemy_Ai>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemyAI._Hunting = true;
            //Debug.Log("Hunt range entered");
        }
    }
    
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemyAI._Hunting = true;
            //Debug.Log("Hunt range entered");
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(WaitToEndHunt());
            //Debug.Log("Hunt range exited");
        }
    }

    private IEnumerator WaitToEndHunt()
    {
        yield return new WaitForSeconds(waitToEndHuntTime);
        enemyAI._Hunting = false;
        enemyAI._Patrolling = true;
        //Debug.Log("Stopped hunting");
    }
}
