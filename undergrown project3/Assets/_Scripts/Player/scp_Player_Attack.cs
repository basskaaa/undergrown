using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class scp_Player_Attack : MonoBehaviour
{
    [SerializeField] private GameObject Sword;
    private Collider swordCollider;

    private scp_Player_Manager_Holder h;
    private scp_Player_Manager playerManager;
    [SerializeField] private float attackCooldown = 0.4f;
    private bool attackReady = true;
    private bool isDead;

    [SerializeField] private int attackAmmo;
    [SerializeField] private TextMeshPro attackAmmoCount;
    [SerializeField] private GameObject swordIcon;

    private void Start()
    {
        h = GetComponent<scp_Player_Manager_Holder>();
        playerManager = h._Manager;
        swordCollider = Sword.GetComponent<Collider>();
        isDead = playerManager._Dead;

        //test
        attackAmmo = 10;
        attackAmmoCount.text = attackAmmo.ToString();

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
    }

    private IEnumerator Attack()
    {
        attackReady = false;
        swordCollider.enabled = true;
        attackAmmo--;
        attackAmmoCount.text = attackAmmo.ToString();

        yield return new WaitForSeconds(attackCooldown);
        playerManager._Attacking = false;
        attackReady = true;
    }
}
