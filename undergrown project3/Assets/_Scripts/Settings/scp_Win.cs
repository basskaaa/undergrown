using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scp_Win : MonoBehaviour
{
    [SerializeField] GameObject winUI;
    [SerializeField] TextMeshProUGUI deathsText;
    [SerializeField] TextMeshProUGUI killedText;
    private scp_Enemy_Manager enemy;

    private void Start()
    {
        enemy = FindObjectOfType<scp_Enemy_Manager>();
    }

    public void setWinUi()
    {
        winUI.SetActive(true);
        killedText.text = ("Kills: " + enemy.killed.ToString());
        deathsText.text = ("Deaths: " + scp_Player_Manager.deathCount.ToString());
        scp_PauseMenu.GameIsPaused = true;
    }
}
