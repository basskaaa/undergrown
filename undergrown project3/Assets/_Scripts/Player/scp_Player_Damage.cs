using UnityEngine;

public class scp_Player_Damage : MonoBehaviour
{
    private scp_Player_Manager playerManager;
    private scp_Enemy_AI enemyAI;

    private void Start()
    {
        playerManager = FindObjectOfType<scp_Player_Manager>();
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
