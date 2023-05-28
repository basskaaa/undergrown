using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class scp_Timer : MonoBehaviour
{
    public float timeStart;
    public TextMeshProUGUI timerText;
    bool timerActive = true;

    void Start()
    {
        if (timerText != null) timerText.text = timeStart.ToString("F2");
    }

    void Update()
    {
        if (scp_PauseMenu.GameIsPaused && timerText != null) timerActive = false; timerText.text = ("Time: " + timeStart.ToString("F2") + "s");
        if (!scp_PauseMenu.GameIsPaused && timerText != null) timerActive = true;

        if (timerActive && timerText != null)
        {
            timeStart += Time.deltaTime;
        }
    }
}