using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_Knockback : MonoBehaviour
{
    [SerializeField] private float Multiplier;
    public bool collisionDetect = false;

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
    }
}
