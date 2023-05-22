using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource music;

    private bool huntCheck;
    public bool hunted;
    private bool idleCheck;
    private scp_PauseMenu pauseScript;
    private bool pauseCheck;

    private void Start()
    {
        pauseScript = FindObjectOfType<scp_PauseMenu>();
    }

    private void Update()
    {
        if (scp_PauseMenu.GameIsPaused && !pauseCheck) music.pitch = 0.75f; pauseCheck = true;
        if (!scp_PauseMenu.GameIsPaused && !pauseCheck) music.pitch = 0.8f; pauseCheck = false;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && !huntCheck)
        {
            Debug.Log("Entered hunt");
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
        music.pitch = 1.2f;
    }

    public IEnumerator SetIdleMusic()
    {
        idleCheck = true;
        huntCheck = false;

        yield return new WaitForSeconds(20f);
        music.pitch = 0.8f;
    }
}
