using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class scp_Player_Sword : MonoBehaviour
{
    [SerializeField] private float Multiplier;
    [SerializeField] private GameObject SwordParticles;
    private scp_Player_Manager_Holder h;
    private scp_Player_Manager manager;

    public bool collisionDetect = false;
    private Vector3 particleSpawnTf;



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


        if (manager._Attacking && collision.gameObject.tag == "Enemy")
        {
            //Debug.Log("sword hit " + collision.gameObject.name);

            particleSpawnTf = collision.transform.position;
            GameObject clone = (GameObject)Instantiate(SwordParticles, particleSpawnTf, Quaternion.identity);
            Destroy(clone, 1.0f);
        }
    }
}
