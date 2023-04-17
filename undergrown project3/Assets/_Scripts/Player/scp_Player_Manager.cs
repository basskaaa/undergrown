using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class scp_Player_Manager : MonoBehaviour
{
    [HideInInspector] public bool _Dying = false;
    private CharacterController playerController;
    private PlayerInput playerInput;

    private void Start()
    {
        playerController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (_Dying)
        {
            playerInput.enabled = false;
        }
    }
}
