using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_Enemy_Hit : MonoBehaviour
{
    [SerializeField] private float iFrames = 0.1f;

    private scp_Enemy_AI ai;
    private bool playerAttacking;

    private void Start()
    {
        ai = GetComponentInParent<scp_Enemy_AI>();
    }

    private void Update()
    {
        playerAttacking = ai._EnemyManager._PlayerAttacking;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("pWeapon") && playerAttacking) StartCoroutine(Hit());
    }

    private IEnumerator Hit()
    {
        Debug.Log("hit by sword");
        ai._Dying = true;
        yield return new WaitForSeconds(iFrames);
    }
}
