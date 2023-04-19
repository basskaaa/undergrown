using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class scp_Player_Manager : MonoBehaviour
{
    public bool _Dead = false;
    public bool _Attacking = false;
    public bool _Jump = false;
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

        if (!canMoveCheck) cantMove();
        if (_Attacking) _AttackMove();
        if (!_Attacking) canMove();
    }

    private void cantMove()
    {     
        _ThirdPersonController.MoveSpeed = 0f;
        _ThirdPersonController.SprintSpeed = 0f;
        _ThirdPersonController.SpeedChangeRate = 0f;
        _ThirdPersonController.JumpHeight = 0f;
    }

    private void canMove()
    {
        _ThirdPersonController.MoveSpeed = 2f;
        _ThirdPersonController.SprintSpeed = 10f;
        _ThirdPersonController.SpeedChangeRate = 10f;
        _ThirdPersonController.JumpHeight = 1.2f;
    }

    public void _AttackMove()
    {
        _ThirdPersonController.MoveSpeed = 0f;
        _ThirdPersonController.SprintSpeed = 0f;
        _ThirdPersonController.SpeedChangeRate = 20f;
        _ThirdPersonController.JumpHeight = 1.2f;
    }
}
