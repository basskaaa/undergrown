using System.Collections;
using UnityEngine;

public class scp_Enemy_AI_Attacking : MonoBehaviour
{
    private scp_Enemy_AI ai;

    [SerializeField] private float attackTime = .3f;

    void Start()
    {
        ai = GetComponentInParent<scp_Enemy_AI>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && ai._Hunting && !ai._EnemyManager._PlayerManager._Dead)
        {
            ai._Attacking = true;
            //Debug.Log("Hunt range entered");
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && !ai._EnemyManager._PlayerManager._Dead)
        {
            ai._Attacking = true;
            //Debug.Log("Hunt range entered");
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && !ai._EnemyManager._PlayerManager._Dead)
        {
            StartCoroutine(finishAttackAnim());
        }
    }

    IEnumerator finishAttackAnim()
    {
        yield return new WaitForSeconds(attackTime);
        ai._Attacking = false;
        ai._Hunting = true;
    }
}
