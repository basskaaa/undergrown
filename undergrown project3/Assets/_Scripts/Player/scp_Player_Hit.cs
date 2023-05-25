using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_Player_Hit : MonoBehaviour
{
    private scp_Player_Manager_Holder h;
    private scp_Player_Manager playerManager;
    private scp_Enemy_AI enemyAI;
    [SerializeField] public GameObject hitPlayerParticles;
    private Vector3 particleSpawnTf;

    public HealthBar _HealthBar;
    public int _MaxHealth = 10;
    public int _CurrentHealth;
    [SerializeField] private float IFrames = 0.3f;

    private bool invincible;

    private void Start()
    {
        h = GetComponentInParent<scp_Player_Manager_Holder>();
        playerManager = h._Manager;

        _CurrentHealth = _MaxHealth;
        _HealthBar.SetMaxHealth(_MaxHealth);
    }

    private void Update()
    {
        if (_CurrentHealth == 0 && !playerManager._Dead) 
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
        _CurrentHealth--;
        _HealthBar.SetHealth(_CurrentHealth);
        invincible = true;
        playerManager._Hit = true;

        yield return new WaitForSeconds(IFrames);
        playerManager._Hit = false;
        invincible = false;
    }

    public void SetHealthBar()
    {
        _HealthBar.SetHealth(_CurrentHealth);

    }
}
