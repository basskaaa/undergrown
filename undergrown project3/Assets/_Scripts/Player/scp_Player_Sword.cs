using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class scp_Player_Sword : MonoBehaviour
{
    [SerializeField] private float Multiplier;
    [SerializeField] private GameObject SwordParticles;
    [SerializeField] private GameObject SkeleHitSound;
    private scp_Player_Manager_Holder h;
    private scp_Player_Manager manager;

    private Vector3 particleSpawnTf;



    private void Start()
    {
        h = GetComponentInParent<scp_Player_Manager_Holder>();
        manager = h._Manager;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (manager._Attacking && collision.gameObject.tag == "Enemy")
        {
            //Debug.Log("sword hit " + collision.gameObject.name);

            particleSpawnTf = collision.transform.position;
            GameObject clone = (GameObject)Instantiate(SwordParticles, particleSpawnTf, Quaternion.identity);
            GameObject clone1 = (GameObject)Instantiate(SkeleHitSound, particleSpawnTf, Quaternion.identity);
            Destroy(clone, 1.0f);
            Destroy(clone1, 1.0f);
        }
    }
}
