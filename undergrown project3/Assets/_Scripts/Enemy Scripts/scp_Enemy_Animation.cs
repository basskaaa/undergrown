using System.Collections;
using UnityEngine;

public class scp_Enemy_Animation : MonoBehaviour
{
    private scp_Enemy_AI ai;
    private scp_Enemy_Manager enemyManager;

    private Animator enemyAnimRef;

    void Start()
    {
        enemyAnimRef = GetComponent<Animator>();
        ai = GetComponent<scp_Enemy_AI>();
    }

    void Update()
    {
        updateAnim();
    }

    private void updateAnim()
    {
        if (ai._Patrolling)
        {
            enemyAnimRef.Play("anim_skeleton_walk");
        }

        if (ai._Resting) 
        {
            enemyAnimRef.Play("anim_skeleton_idle");
        }

        if (ai._Hunting) 
        {
            enemyAnimRef.Play("anim_skeleton_run");
        }

        if (ai._Attacking)
        {
            enemyAnimRef.Play("anim_skeleton_attack");
        }
    }
}
