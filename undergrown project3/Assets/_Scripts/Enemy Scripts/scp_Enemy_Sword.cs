using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_Enemy_Sword : MonoBehaviour
{
    private scp_Enemy_AI ai;
    [SerializeField] GameObject hitPlayerParticles;
    private Vector3 particleSpawnTf;

    private void Start()
    {
        ai = GetComponentInParent<scp_Enemy_AI>();
        if (ai == null) Debug.Log("Ai null");
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");
        if (ai._Attacking && collision.gameObject.tag == "Player")
        {
            Debug.Log("player collision");

            particleSpawnTf = collision.transform.position;
            GameObject clone = (GameObject)Instantiate(hitPlayerParticles, particleSpawnTf, Quaternion.identity);
            Destroy(clone, 1.0f);
        }
    }
}
