using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_Enemy_Sword : MonoBehaviour
{
    private scp_Enemy_AI ai;
    [SerializeField] GameObject hitPlayerParticles;
    private Vector3 particleSpawnTf;
    public bool _HitPlayer;

    private void Start()
    {
        ai = GetComponentInParent<scp_Enemy_AI>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (ai._Attacking && collision.gameObject.tag == "Player")
        {
            _HitPlayer = true;
            particleSpawnTf = collision.transform.position;
            GameObject clone = (GameObject)Instantiate(hitPlayerParticles, particleSpawnTf, Quaternion.identity);
            Destroy(clone, 1.0f);
        }
        
        else _HitPlayer = false;
    }
}
