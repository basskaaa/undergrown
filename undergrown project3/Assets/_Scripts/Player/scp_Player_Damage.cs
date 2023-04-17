using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_Player_Damage : MonoBehaviour
{
    private scp_Player_Manager manager;
    private scp_Enemy_Ai enemyAI;

    private void Start()
    {
        manager = GetComponentInParent<scp_Player_Manager>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "eWeapon")
        {
            enemyAI = collision.gameObject.GetComponentInParent<scp_Enemy_Ai>();

            if (enemyAI._Attacking)
            {
                manager._Dying = true;
                Debug.Log("You died");
            }
        }
    }    
}
