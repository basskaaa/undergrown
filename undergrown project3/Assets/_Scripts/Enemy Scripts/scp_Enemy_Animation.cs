using System.Collections;
using UnityEngine;

public class scp_Enemy_Animation : MonoBehaviour
{
    private scp_Enemy_AI ai;

    private Animator enemyAnim;

    void Start()
    {
        enemyAnim = GetComponent<Animator>();
        ai = GetComponent<scp_Enemy_AI>();
    }

    void Update()
    {
        updateAnim();
    }

    private void updateAnim()
    {
        if (ai._Patrolling) enemyAnim.Play("anim_skeleton_walk");

        if (ai._Resting) enemyAnim.Play("anim_skeleton_idle");

        if (ai._Hunting) enemyAnim.Play("anim_skeleton_run");

        if (ai._Attacking) enemyAnim.Play("anim_skeleton_attack");

        if (ai._Dying) enemyAnim.Play("anim_skeleton_death");
    }
}
