using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class scp_Player_Manager : MonoBehaviour
{
    public bool _Dead = false;
    private bool deadCheck = false;
    public bool _Attacking = false;
    public bool _Hit = false;
    public bool _Jump = false;
    [SerializeField] private bool canMoveCheck = true;
    [SerializeField] private GameObject uiDeath;
    [SerializeField] private Light healthLight;

    [SerializeField] private float move = 20f;
    [SerializeField] private float run = 40f;
    [SerializeField] private float speedChangeRate = 10f;
    [SerializeField] private float jumpHeight = 3f;
    private float healthFloat;
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    public GameObject _Player;

    [HideInInspector] public ThirdPersonController _ThirdPersonController;
    [HideInInspector] public scp_Player_Animation _AnimManager;
    [HideInInspector] public scp_Player_Hit _HealthManager;

    private void Start()
    {
        _ThirdPersonController = _Player.GetComponent<ThirdPersonController>();
        _AnimManager = _Player.GetComponent<scp_Player_Animation>();
        _HealthManager = _Player.GetComponentInChildren<scp_Player_Hit>();
        uiDeath.SetActive(false);
        maxHealth = _HealthManager._MaxHealth;
    }

    private void Update()
    {
        currentHealth = _HealthManager._CurrentHealth;
        healthFloat = currentHealth / maxHealth;
        //healthLight.intensity = healthFloat;

        if (_Dead && !deadCheck) canMoveCheck = false;

        if (!canMoveCheck) death();
        if (_Attacking) _AttackMove();
        if (!_Attacking && !_Dead) canMove();
    }

    private void death()
    {     
        _ThirdPersonController.enabled = false;
        uiDeath.SetActive(true);
        deadCheck = true;
    }

    private void canMove()
    {
        _ThirdPersonController.MoveSpeed = move;
        _ThirdPersonController.SprintSpeed = run;
        _ThirdPersonController.SpeedChangeRate = speedChangeRate;
        _ThirdPersonController.JumpHeight = jumpHeight;
    }

    public void _AttackMove()
    {
        _ThirdPersonController.MoveSpeed = 0f;
        _ThirdPersonController.SprintSpeed = 0f;
        _ThirdPersonController.SpeedChangeRate = 20f;
        _ThirdPersonController.JumpHeight = 3f;
    }
}
