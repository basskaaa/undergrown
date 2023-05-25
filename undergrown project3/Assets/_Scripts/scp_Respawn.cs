using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_Respawn : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject RespawnPos;
    [SerializeField] private GameObject[] Enemy;
    [SerializeField] private GameObject[] SwordFlower;

    private Transform playerSpawnTf;
    private scp_Player_Manager playerManager;
    private scp_Player_Attack playerAttack;
    private scp_Player_Hit playerHealth;
    private scp_Enemy_AI[] enemyAi;
    private scp_SwordFlower[] flowerScript;

    public bool respawnCheck;

    private void Start()
    {
        playerSpawnTf = Player.transform;
        playerManager = FindObjectOfType<scp_Player_Manager>();
        playerHealth = FindObjectOfType<scp_Player_Hit>();
        playerAttack = FindObjectOfType<scp_Player_Attack>();
        Enemy = GameObject.FindGameObjectsWithTag("Enemy");
    }

    private void Update()
    {
        SwordFlower = GameObject.FindGameObjectsWithTag("swordFlower");
    }

    public void Respawn()
    {
        RespawnPlayer();
        RespawnEnemies();
        SetFlowers();
    }

    public void RespawnEnemies()
    {
        var enemyAis = FindObjectsOfType<scp_Enemy_AI>();

        foreach (var enemyAi in enemyAis)
        {
            enemyAi.EnemyRespawn();
        }
    }

    public void RespawnPlayer()
    {
        playerAttack.ResetPos();
        playerHealth._CurrentHealth = playerHealth._MaxHealth;
        playerHealth.SetHealthBar();
        playerManager.PlayerRespawn();
    }

    public void SetFlowers()
    {
        // FindObjectsOfType gets you the components directly so you don't need to lookup by tag
        // It is too slow to use every frame but doing a 1 off for a respawn or something is perfectly valid
        var swordFlowers = FindObjectsOfType<scp_SwordFlower>();

        foreach (var swordFlower in swordFlowers)
        {
            swordFlower.SetFlower();
        }
    }

}
