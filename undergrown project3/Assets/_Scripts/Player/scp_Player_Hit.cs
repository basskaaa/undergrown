using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_Player_Hit : MonoBehaviour
{
    private scp_Player_Manager_Holder h;
    private scp_Player_Manager playerManager;
    private scp_Enemy_AI enemyAI;
    [SerializeField] GameObject hitPlayerParticles;
    private Vector3 particleSpawnTf;

    [SerializeField] private int MaxHealth = 10;
    [SerializeField] private int CurrentHealth;
    [SerializeField] private float IFrames = 0.3f;

    private bool invincible;

    private void Start()
    {
        h = GetComponentInParent<scp_Player_Manager_Holder>();
        playerManager = h._Manager;

        CurrentHealth = MaxHealth;
    }

    private void Update()
    {
        if (CurrentHealth == 0 && !playerManager._Dead) 
        {
            playerManager._Dead = true;
            Debug.Log("You died");
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "eWeapon")
        {
            enemyAI = collision.gameObject.GetComponentInParent<scp_Enemy_AI>();

            if (enemyAI._Attacking && !invincible)
            {
                StartCoroutine (waitForIFrames());
            }

            particleSpawnTf = collision.transform.position;
            GameObject clone = (GameObject)Instantiate(hitPlayerParticles, particleSpawnTf, Quaternion.identity);
            Destroy(clone, 1.0f);
        }
    }    

    private IEnumerator waitForIFrames()
    {
        CurrentHealth--;
        Debug.Log("Health: " + CurrentHealth);
        invincible = true;
        playerManager._Hit = true;

        yield return new WaitForSeconds(IFrames);
        playerManager._Hit = false;
        invincible = false;
    }
}
