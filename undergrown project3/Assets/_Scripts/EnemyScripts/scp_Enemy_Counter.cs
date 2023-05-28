using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scp_Enemy_Counter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI enemyCountUi;
    private int enemyCountInt;
    private scp_Enemy_AI[] enemy;
    private scp_Win win;

    void Start()
    {
        ResetCounter();
        win = FindObjectOfType<scp_Win>();
    }

    private void Update()
    {
        if (enemyCountInt == 0) win.setWinUi();
    }

    public void ResetCounter()
    {
        enemy = FindObjectsOfType<scp_Enemy_AI>();
        enemyCountInt = enemy.Length;
        enemyCountUi.text = (enemyCountInt.ToString() + " Foes Remaining");
    }

    public void DecreaseCounter()
    {
        enemyCountInt--;
        enemyCountUi.text = (enemyCountInt.ToString() + " Foes Remaining");
    }
}
