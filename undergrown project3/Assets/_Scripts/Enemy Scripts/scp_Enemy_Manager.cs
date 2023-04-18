using UnityEngine;

public class scp_Enemy_Manager : MonoBehaviour
{
    public GameObject _Player;
    public scp_Player_Manager _PlayerManager;
    public bool _PlayerDead;

    public Transform[] _Waypoints;

    private void Update()
    {
        _PlayerDead = _PlayerManager._Dead;
    }
}
