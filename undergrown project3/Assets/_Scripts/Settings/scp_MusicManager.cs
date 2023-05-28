using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource music;

    private bool huntCheck;
    public bool hunted;
    private bool idleCheck;
    private bool pauseCheck;

    private void Update()
    {
        if (scp_PauseMenu.GameIsPaused && !pauseCheck) music.pitch = 0.5f; pauseCheck = true;
        if (!scp_PauseMenu.GameIsPaused && pauseCheck) music.pitch = 0.7f; pauseCheck = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && !huntCheck)
        {
            StartCoroutine(SetHuntMusic());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && !huntCheck)
        {
            StartCoroutine(SetHuntMusic());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && !idleCheck)
        {
            StartCoroutine(SetIdleMusic());
        }
    }

    public IEnumerator SetHuntMusic()
    {
        idleCheck = false;
        huntCheck = true;

        yield return new WaitForSeconds(0.1f);
        music.pitch = Mathf.MoveTowards(music.pitch,0.9f, (1/2) * Time.deltaTime); 
    }

    public IEnumerator SetIdleMusic()
    {
        idleCheck = true;
        huntCheck = false;

        yield return new WaitForSeconds(20f);
        music.pitch = Mathf.MoveTowards(music.pitch, 0.6f, (1 / 6) * Time.deltaTime);
    }
}
