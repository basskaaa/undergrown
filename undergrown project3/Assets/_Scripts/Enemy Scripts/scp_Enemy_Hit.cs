using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_Enemy_Hit : MonoBehaviour
{
    [SerializeField] private float iFrames = 0.1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("pWeapon")) StartCoroutine(Hit());
    }

    private IEnumerator Hit()
    {
        Debug.Log("hit by sword");
        yield return new WaitForSeconds(iFrames);
    }
}
