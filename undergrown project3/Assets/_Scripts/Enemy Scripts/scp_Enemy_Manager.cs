using UnityEngine;

public class scp_Enemy_Manager : MonoBehaviour
{
    public GameObject _Player;
    public GameObject _KnockbackTf;
    public scp_Player_Manager _PlayerManager;
    public bool _PlayerDead;
    public bool _PlayerAttacking;
    public float _WaitToDestroy = 30f;

    public Transform[] _Waypoints;

    private void Update()
    {
        _PlayerDead = _PlayerManager._Dead;
        _PlayerAttacking = _PlayerManager._Attacking;

    }
}
