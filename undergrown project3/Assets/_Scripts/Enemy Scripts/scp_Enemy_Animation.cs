using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_Enemy_Animation : MonoBehaviour
{
    private Animator enemyAnimRef;
    private scp_Enemy_Ai enemyAI;

    void Start()
    {
        enemyAnimRef = GetComponent<Animator>();
        enemyAI = GetComponent<scp_Enemy_Ai>();
    }

    void Update()
    {
        updateAnim();
    }

    private void updateAnim()
    {
        if (enemyAI._Patrolling)
        {
            enemyAnimRef.Play("anim_skeleton_walk");
        }

        if (enemyAI._Resting) 
        {
            enemyAnimRef.Play("anim_skeleton_idle");
        }

        if (enemyAI._Hunting) 
        {
            enemyAnimRef.Play("anim_skeleton_run");
        }

        if (enemyAI._Attacking)
        {
            enemyAnimRef.Play("anim_skeleton_attack");
        }
    }
}
