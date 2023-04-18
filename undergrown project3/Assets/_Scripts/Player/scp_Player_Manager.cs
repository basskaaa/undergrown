using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class scp_Player_Manager : MonoBehaviour
{
    [HideInInspector] public bool _Dead = false;
    [SerializeField] private bool canMoveCheck = true;

    public GameObject _Player;

    private ThirdPersonController thirdPersonController;
    private scp_Player_Animation animManager;



    private void Start()
    {
        thirdPersonController = _Player.GetComponent<ThirdPersonController>();
        animManager = _Player.GetComponent<scp_Player_Animation>();
    }

    private void Update()
    {
        if (_Dead)
        {
            animManager._PlayDeathAnim = true;
            canMoveCheck = false;
        }

        if (!canMoveCheck)
        {
            cantMove();
        }
        else canMove();
    }

    private void cantMove()
    {     
        thirdPersonController.MoveSpeed = 0;
        thirdPersonController.SprintSpeed = 0;
        thirdPersonController.JumpHeight = 0;
    }

    private void canMove()
    {
        thirdPersonController.MoveSpeed = 2;
        thirdPersonController.SprintSpeed = 6;
        thirdPersonController.JumpHeight = 1.2f;
    }
}
