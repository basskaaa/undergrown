using UnityEngine;

public class scp_Player_Animation : MonoBehaviour
{
    private Animator playerAnimRef;

    public bool _PlayDeathAnim;

    private void Start()
    {
        playerAnimRef = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_PlayDeathAnim)
        {
            playerAnimRef.Play("anim_player_death");
        }
    }
}
