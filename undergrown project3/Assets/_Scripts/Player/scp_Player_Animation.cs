using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_Player_Animation : MonoBehaviour
{
    private scp_Player_Manager manager;
    private Animator playerAnimRef;

    private void Start()
    {
        manager = GetComponent<scp_Player_Manager>();
        playerAnimRef = GetComponent<Animator>();
    }

    private void Update()
    {
        if (manager._Dying)
        {
            playerAnimRef.Play("anim_player_death");
        }
    }
}
