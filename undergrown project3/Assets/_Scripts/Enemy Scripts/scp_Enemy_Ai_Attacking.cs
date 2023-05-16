using System.Collections;
using UnityEngine;

public class scp_Enemy_Ai_Attacking : MonoBehaviour
{
    private scp_Enemy_AI ai;

    [SerializeField] private float attackTime = .5f;
    private bool inAttack;
    [SerializeField] private bool updateAttack;

    void Start()
    {
        ai = GetComponentInParent<scp_Enemy_AI>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && ai._Hunting && !ai._Hit && !ai._EnemyManager._PlayerManager._Dead)
        {
            if (!inAttack) StartCoroutine(Attack());
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && !ai._EnemyManager._PlayerManager._Dead && !ai._Hit)
        {
            if (!inAttack) StartCoroutine(Attack());
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && !ai._EnemyManager._PlayerManager._Dead)
        {
            StartCoroutine(catchUp());
        }
    }

    IEnumerator catchUp()
    {
        yield return new WaitForSeconds(attackTime);
        ai._Attacking = false;
        ai._Hunting = true;
    }
    IEnumerator Attack()
    {
        inAttack = true;
        ai._Attacking = true;

        yield return new WaitForSeconds(attackTime);
        ai._Attacking = false;
        ai._Hunting = true;
        inAttack = false;
        yield return new WaitForSeconds(0.2f);
    }
}
