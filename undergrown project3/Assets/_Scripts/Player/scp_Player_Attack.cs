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
    [SerializeField] private GameObject RespawnPos;
    [SerializeField] private GameObject AttackSound;
    [SerializeField] private GameObject SeedSpawnSound;
    private bool seedSpawnSoundPlayed;
    [SerializeField] private GameObject FlowerCollectSound;
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
        attackAmmo = 11;
        attackAmmoCount.text = (attackAmmo - 1).ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && attackReady && !isDead && attackAmmo>0 && !(scp_PauseMenu.GameIsPaused))
        {
            playerManager._Attacking = true;
            attackAmmo--;
            attackAmmoCount.text = attackAmmo.ToString();
            GameObject clone = (GameObject)Instantiate(AttackSound, transform.position, Quaternion.identity);
            Destroy(clone, 1.0f);
            playerManager._AttackMove();
            StartCoroutine(Attack());
        }

        if ((attackAmmo)==0 && !seedSpawnSoundPlayed)
        {
            GameObject clone = (GameObject)Instantiate(SeedSpawnSound, transform.position, Quaternion.identity);
            Destroy(clone, 1.0f);
            swordIcon.color = new Color(swordIcon.color.r, swordIcon.color.g, swordIcon.color.b, 0.3f);
            attackAmmoCount.color = new Color(attackAmmoCount.color.r, attackAmmoCount.color.g, attackAmmoCount.color.b, 0.3f);
            Sword.SetActive(false);
            seedSpawnSoundPlayed = true;
        }

        if ((attackAmmo)>0)
        {
            Sword.SetActive(true);
            swordIcon.color = new Color(swordIcon.color.r, swordIcon.color.g, swordIcon.color.b, 1f);
            attackAmmoCount.color = new Color(attackAmmoCount.color.r, attackAmmoCount.color.g, attackAmmoCount.color.b, 1f);
            seedSpawnSoundPlayed=false;
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
        GameObject clone = (GameObject)Instantiate(FlowerCollectSound, transform.position, Quaternion.identity);
        Destroy(clone, 1.0f);
    }

    public void ResetPos()
    {
        transform.SetPositionAndRotation(RespawnPos.transform.position, RespawnPos.transform.rotation); 
    }
}
