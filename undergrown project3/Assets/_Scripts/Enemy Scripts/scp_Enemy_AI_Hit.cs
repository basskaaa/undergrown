using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class scp_Enemy_AI_Hit : MonoBehaviour
{
    [SerializeField] private float iFrames = .3f;
    [SerializeField] private int MaxHealth = 2;
    [SerializeField] private GameObject playerTf;
    public int _CurrentHealth;

    private scp_Enemy_AI ai;
    private bool playerAttacking = false;
    private bool hitCheck = false;
    private Rigidbody rb;

    private void Start()
    {
        ai = GetComponentInParent<scp_Enemy_AI>();
        rb = GetComponent<Rigidbody>();
        playerTf = ai._EnemyManager._KnockbackTf;
        _CurrentHealth = MaxHealth;
    }

    private void Update()
    {
        playerAttacking = ai._EnemyManager._PlayerAttacking;

        if (_CurrentHealth <= 0 && !ai._Dying)
        {
            ai._Dying = true;
        }

        if (!ai._Hit)
        {
            transform.position = ai.transform.position;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == ("pWeapon") && playerAttacking && !hitCheck && !ai._Dying)
        {
            //Vector3 moveDirection = (playerTf.transform.position);
            //moveDirection.y = 0;
            //rb.AddForce(moveDirection.normalized * .6f, ForceMode.Impulse);
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
        rb.velocity = Vector3.zero;
        hitCheck = false;
        _CurrentHealth--;
        ai._Hit = false;
        //if (!ai._Dying) ai._Hunting = true;
    }
}
