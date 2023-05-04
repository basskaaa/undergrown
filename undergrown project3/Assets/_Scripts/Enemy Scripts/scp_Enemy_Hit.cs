using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_Enemy_Hit : MonoBehaviour
{
    [SerializeField] private float iFrames = 1f;

    private scp_Enemy_AI ai;
    private bool playerAttacking;
    [SerializeField] private float KnockbackMultiplier = 2f;


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
        if (other.gameObject.tag == ("pWeapon") && playerAttacking)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Debug.Log(rb.gameObject.name);

                Vector3 direction = other.transform.position - transform.position;
                direction.y = 0;

                rb.AddForce(direction.normalized * KnockbackMultiplier, ForceMode.Impulse);
            }

            StartCoroutine(Hit());
        }
    }

    private IEnumerator Hit()
    {
        Debug.Log("hit by sword");
        yield return new WaitForSeconds(iFrames);
        ai._Dying = true;
    }
}
