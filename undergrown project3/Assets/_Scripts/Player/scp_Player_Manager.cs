using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class scp_Player_Manager : MonoBehaviour
{
    [HideInInspector] public bool _Dead = false;
    [HideInInspector] public bool _Attacking = false;
    [SerializeField] private bool canMoveCheck = true;

    public GameObject _Player;

    [HideInInspector] public ThirdPersonController _ThirdPersonController;
    [HideInInspector] public scp_Player_Animation _AnimManager;

    private void Start()
    {
        _ThirdPersonController = _Player.GetComponent<ThirdPersonController>();
        _AnimManager = _Player.GetComponent<scp_Player_Animation>();
    }

    private void Update()
    {
        if (_Dead) canMoveCheck = false;

        if (!canMoveCheck) _CantMove();
        if (!_Attacking) canMove();
    }

    public void _CantMove()
    {     
        _ThirdPersonController.MoveSpeed = 0;
        _ThirdPersonController.SprintSpeed = 0;
        _ThirdPersonController.JumpHeight = 0;
    }

    private void canMove()
    {
        _ThirdPersonController.MoveSpeed = 2;
        _ThirdPersonController.SprintSpeed = 6;
        _ThirdPersonController.JumpHeight = 1.2f;
    }
}
