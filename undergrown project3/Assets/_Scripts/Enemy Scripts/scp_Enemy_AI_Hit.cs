using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class scp_Enemy_AI_Hit : MonoBehaviour
{
    [SerializeField] private float iFrames = .3f;
    [SerializeField] private int MaxHealth = 2;
    [SerializeField] private int CurrentHealth;

    private scp_Enemy_AI ai;
    private bool playerAttacking = false;
    private bool hitCheck = false;

    private void Start()
    {
        ai = GetComponentInParent<scp_Enemy_AI>();
        CurrentHealth = MaxHealth;
    }

    private void Update()
    {
        playerAttacking = ai._EnemyManager._PlayerAttacking;

        if (CurrentHealth <= 0 && !ai._Dying)
        {
            ai._Dying = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("pWeapon") && playerAttacking && !hitCheck && !ai._Dying)
        {
            StartCoroutine(Hit());
        }
    }

    private IEnumerator Hit()
    {
        hitCheck = true;
        ai._Attacking = false;
        ai._Hunting = false;
        ai._Hit = true;

        yield return new WaitForSeconds(iFrames);
        hitCheck = false;
        CurrentHealth--;
        ai._Hit = false;
        if (!ai._Dying) ai._Hunting = true;
    }
}
