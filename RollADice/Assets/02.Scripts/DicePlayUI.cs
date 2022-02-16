using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DicePlayUI : MonoBehaviour
{
    static public DicePlayUI instance;
    private void Awake()
    {
        instance = this;
    }
    public GameObject normalDicePanel;
    public GameObject inverseDicePanel;

    public void SwitchDicePanel()
    {
        if (normalDicePanel.activeSelf)
        {
            normalDicePanel.SetActive(false);
            inverseDicePanel.SetActive(true);
        }
        else
        {
            normalDicePanel.SetActive(true);
            inverseDicePanel.SetActive(false);
        }
            
    }
}
