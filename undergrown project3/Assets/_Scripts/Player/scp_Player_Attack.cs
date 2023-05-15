using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class scp_Player_Attack : MonoBehaviour
{
    [SerializeField] private GameObject Sword;
    [SerializeField] private ParticleSystem SwordParticles;
    private Collider swordCollider;

    private scp_Player_Manager_Holder h;
    private scp_Player_Manager playerManager;
    [SerializeField] private float attackCooldown = 0.5f;
    private bool attackReady = true;
    private bool isDead;

    private void Start()
    {
        h = GetComponent<scp_Player_Manager_Holder>();
        playerManager = h._Manager;
        swordCollider = Sword.GetComponent<Collider>();
        //swordCollider.enabled = false;
        isDead = playerManager._Dead;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && attackReady && !isDead)
        {
            playerManager._Attacking = true;
        }

        if (playerManager._Attacking)
        {
            playerManager._AttackMove();
            StartCoroutine(Attack());
        }

        //else swordCollider.enabled = false;
    }

    private IEnumerator Attack()
    {
        attackReady = false;
        swordCollider.enabled = true;

        yield return new WaitForSeconds(attackCooldown);
        SwordParticles.Stop();
        playerManager._Attacking = false;
        attackReady = true;
    }
}
