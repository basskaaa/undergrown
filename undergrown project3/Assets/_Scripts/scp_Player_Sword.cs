using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_Player_Sword : MonoBehaviour
{
    [SerializeField] private float Multiplier;
    [SerializeField] private ParticleSystem SwordParticles;
    private scp_Player_Manager_Holder h;
    private scp_Player_Manager manager;

    public bool collisionDetect = false;


    private void Start()
    {
        h = GetComponentInParent<scp_Player_Manager_Holder>();
        manager = h._Manager;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();
        collisionDetect = true;

        if (rb != null )
        {
            Debug.Log(rb.gameObject.name);

            Vector3 direction = collision.transform.position - transform.position;
            direction.y = 0;

            rb.AddForce(direction.normalized * Multiplier, ForceMode.Impulse);
        }


        if (manager._Attacking)
        {
            Debug.Log("sword hit something");
            SwordParticles.Play();
        }
    }
}
