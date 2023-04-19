using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class scp_Player_Attack : MonoBehaviour
{
    [SerializeField] private GameObject Sword;
    private Collider swordCollider;

    private scp_Player_Manager_Holder h;
    private scp_Player_Manager playerManager;
    private float attackTime;
    [SerializeField] private float attackCooldown = 0.2f;
    private bool attackReady = true;

    private ThirdPersonController thirdPersonController;

    private void Start()
    {
        h = GetComponent<scp_Player_Manager_Holder>();
        playerManager = h._Manager;
        thirdPersonController = GetComponent<ThirdPersonController>();
        swordCollider = Sword.GetComponent<Collider>();
        swordCollider.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && attackReady)
        {
            playerManager._Attacking = true;
        }

        if (playerManager._Attacking)
        {
            playerManager._CantMove();
            StartCoroutine(Attack());
        }

        else swordCollider.enabled = false;
    }

    private IEnumerator Attack()
    {
        attackTime = playerManager._AnimManager._PlayerAnim.GetCurrentAnimatorStateInfo(0).length;

        attackReady = false;
        swordCollider.enabled = true;

        yield return new WaitForSeconds(attackTime * 0.1f);

        swordCollider.enabled = false;
        playerManager._Attacking = false;

        yield return new WaitForSeconds(attackCooldown);

        attackReady = true;
    }
}
