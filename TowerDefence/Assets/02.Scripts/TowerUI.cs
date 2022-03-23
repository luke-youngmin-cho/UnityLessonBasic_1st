using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TowerUI : MonoBehaviour
{
    public static TowerUI instance;
    
    private void Awake()
    {
        instance = this;
    }

    public Node node;

    public Text upgradePriceText;
    public Text sellPriceText;

    private void OnDisable()
    {
        node = null;
        upgradePriceText.text = "";
        sellPriceText.text = "";
    }
    public void OnUpgradeButton()
    {
        int nextLevel = node.towerInfo.level + 1;
        if(TowerAssets.instance.TryGetTowerName(node.towerInfo.type, nextLevel, out string towerName))
        {
            
        }
        
    }

    public void OnSellButton()
    {

    }
}
