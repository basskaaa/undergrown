using UnityEngine;

public class scp_Player_Damage : MonoBehaviour
{
    private scp_Player_Manager_Holder h;
    private scp_Player_Manager playerManager;
    private scp_Enemy_AI enemyAI;

    private void Start()
    {
        h = GetComponentInParent<scp_Player_Manager_Holder>();
        playerManager = h._Manager;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "eWeapon")
        {
            enemyAI = collision.gameObject.GetComponentInParent<scp_Enemy_AI>();

            if (enemyAI._Attacking)
            {
                playerManager._Dead = true;
                Debug.Log("You died");
            }
        }
    }    
}
