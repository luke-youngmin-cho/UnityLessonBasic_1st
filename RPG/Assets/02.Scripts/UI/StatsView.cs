using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatsView : MonoBehaviour
{
    public static StatsView instance;
    public CMDState CMDState;

    [SerializeField] private Text LVText;
    [SerializeField] private Text EXPText;
    [SerializeField] private Text STRText;
    [SerializeField] private Text DEXText;
    [SerializeField] private Text INTText;
    [SerializeField] private Text LUKText;
    [SerializeField] private Text HPText; 
    [SerializeField] private Text MPText; 
    [SerializeField] private Text ATKText;
    [SerializeField] private Text DEFText;


    public void SetUp()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (Player.isReady == false) return;

        Debug.Log("Refresh stats view");
        Stats stats = Player.instance.stats;
        Stats additionalStats = Player.instance.additionalStats;
        LVText.text = stats.LV.ToString();
        EXPText.text = stats.EXP.ToString();
        STRText.text = stats.STR.ToString() + $"(+ {additionalStats.STR})";
        DEXText.text = stats.DEX.ToString() + $"(+ {additionalStats.DEX})";
        INTText.text = stats.INT.ToString() + $"(+ {additionalStats.INT})";
        LUKText.text = stats.LUK.ToString() + $"(+ {additionalStats.LUK})";
        HPText.text = $"{stats.HP} \n    / {stats.HPMax} (+ {additionalStats.HPMax})";
        MPText.text = $"{stats.MP} \n    / {stats.MPMax} (+ {additionalStats.MPMax})";
        ATKText.text = stats.ATK.ToString() + $"(+ {additionalStats.ATK})"; 
        DEFText.text = stats.DEF.ToString() + $"(+ {additionalStats.DEF})"; 
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
        gameObject.SetActive(false);
        CMDState = CMDState.Ready;
    }

    private void OnEnable()
    {
        Refresh();
    }
}
