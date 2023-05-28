using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scp_Win : MonoBehaviour
{
    [SerializeField] GameObject winUI;
    [SerializeField] TextMeshProUGUI deathsText;

    public void setWinUi()
    {
        winUI.SetActive(true);
        deathsText.text = ("Deaths: " + scp_Player_Manager.deathCount.ToString());
        scp_PauseMenu.GameIsPaused = true;
    }
}
