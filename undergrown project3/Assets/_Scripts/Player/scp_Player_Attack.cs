using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
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

    public int attackAmmo;
    [SerializeField] private TextMeshProUGUI attackAmmoCount;
    [SerializeField] private Image swordIcon;

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
        if (Input.GetKeyDown(KeyCode.Mouse0) && attackReady && !isDead && attackAmmo>0 && !(scp_PauseMenu.GameIsPaused))
        {
            playerManager._Attacking = true;
            attackAmmo--;
            attackAmmoCount.text = attackAmmo.ToString();
        }

        if (playerManager._Attacking)
        {
            playerManager._AttackMove();
            StartCoroutine(Attack());
        }

        if (attackAmmo==0)
        {
            swordIcon.color = new Color(swordIcon.color.r, swordIcon.color.g, swordIcon.color.b, 0.3f);
            attackAmmoCount.color = new Color(attackAmmoCount.color.r, attackAmmoCount.color.g, attackAmmoCount.color.b, 0.3f);
        }

        if (attackAmmo>0)
        {
            swordIcon.color = new Color(swordIcon.color.r, swordIcon.color.g, swordIcon.color.b, 1f);
            attackAmmoCount.color = new Color(attackAmmoCount.color.r, attackAmmoCount.color.g, attackAmmoCount.color.b, 1f);
        }
    }

    private IEnumerator Attack()
    {
        attackReady = false;
        swordCollider.enabled = true;

        yield return new WaitForSeconds(attackCooldown);
        playerManager._Attacking = false;
        attackReady = true;
    }

    public void CollectFlower()
    {
        attackAmmo = attackAmmo + 10;
        attackAmmoCount.text = attackAmmo.ToString();
    }
}
