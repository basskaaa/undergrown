using System.Collections;
using UnityEngine;

public class scp_Enemy_AI_Hunting : MonoBehaviour
{
    private scp_Enemy_AI ai;

    private Collider huntRangeCollider;

    [SerializeField] private float WaitToEndHuntTime = 4f;
    private bool playerDeadCheckFailsafe = false;

    void Start()
    {
        ai = GetComponentInParent<scp_Enemy_AI>();
        huntRangeCollider = GetComponent<Collider>();
    }

    void Update()
    {
        if (ai._Attacking || ai._EnemyManager._PlayerManager._Dead)
        {
            huntRangeCollider.enabled = false;
        }
        else huntRangeCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && !ai._EnemyManager._PlayerManager._Dead)
        {
            ai._Hunting = true;
        }
    }
    
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && !ai._EnemyManager._PlayerManager._Dead)
        {
            ai._Hunting = true;
        }

        if (collision.gameObject.tag == "Player" && ai._EnemyManager._PlayerManager._Dead && !playerDeadCheckFailsafe)
        {
            playerDeadCheckFailsafe = true;

            StartCoroutine(bug1());
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && !ai._EnemyManager._PlayerManager._Dead)
        {
            StartCoroutine(WaitToEndHunt());
        }
    }

    private IEnumerator WaitToEndHunt()
    {
        yield return new WaitForSeconds(WaitToEndHuntTime);
        ai._Hunting = false;
        ai._Patrolling = true;
    }

    private IEnumerator bug1()
    {
        yield return new WaitForSeconds(0.1f);
        ai._PlayerDeadCheck = false;
    }
}
