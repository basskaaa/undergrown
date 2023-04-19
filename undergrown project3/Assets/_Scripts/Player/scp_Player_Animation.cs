using UnityEngine;

public class scp_Player_Animation : MonoBehaviour
{
    private scp_Player_Manager_Holder h;
    private scp_Player_Manager playerManager;

    [HideInInspector] public Animator _PlayerAnim;
    [HideInInspector] public bool _PlayDeathAnim;

    private void Start()
    {
        h = GetComponent<scp_Player_Manager_Holder>();
        playerManager = h._Manager;
        _PlayerAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerManager._Dead) _PlayerAnim.Play("anim_player_death");

        if (playerManager._Attacking) _PlayerAnim.SetBool("Attack", true);
        else _PlayerAnim.SetBool("Attack", false);
    }
}
