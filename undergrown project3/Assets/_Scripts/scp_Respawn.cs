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
        int enemyCount = Enemy.Length;
        enemyAi = new scp_Enemy_AI[enemyCount];

        for (int i = 0; i < enemyCount; i++) 
        {
            enemyAi[i] = Enemy[i].GetComponent<scp_Enemy_AI> ();
            if (enemyAi[i] == null) Debug.Log("no enemy script" + enemyAi[i]);
            enemyAi[i].EnemyRespawn();
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
        SwordFlower = GameObject.FindGameObjectsWithTag("swordFlower");
        int flowerCount = SwordFlower.Length;
        flowerScript = new scp_SwordFlower[flowerCount];
        Debug.Log(flowerCount);

        for (int i = 0;i < flowerCount; i++)
        {
            flowerScript[i] = SwordFlower[i].GetComponent<scp_SwordFlower>();
            if (flowerScript[i] == null) Debug.Log("no flower script" + flowerScript[i]);
            flowerScript[i].SetFlower();
            flowerScript[i].flower = true;
            SwordFlower[i].transform.rotation = new Quaternion (0,0,0,0);
        }
    }

}
