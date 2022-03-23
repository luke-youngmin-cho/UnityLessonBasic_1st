using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerAssets : MonoBehaviour
{
    private static TowerAssets _instance;
    public static TowerAssets instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Instantiate(Resources.Load<TowerAssets>("TowerAssets"));
                _instance.RegisterAllTowerToObjectPool();
            }   
            return _instance;
        }
    }

    public List<GameObject> towers = new List<GameObject>();

    public void RegisterAllTowerToObjectPool()
    {
        foreach (GameObject tower in towers)
        {
            ObjectPool.instance.AddPoolElement(new PoolElement
            {
                prefab = tower,
                size = 20,
                tag = tower.name
            });
        }
    }

    public bool TryGetTowerName(TowerType type, int level, out string towerName)
    {
        towerName = towers.Find(x => x.GetComponent<Tower>().info.type == type &&
                                x.GetComponent<Tower>().info.level == level).name;
        return towerName.Length > 1 ? true : false;
    }

    public bool TryGetTowerInfoByName(string towerName, out TowerInfo towerInfo)
    {
        towerInfo = towers.Find(x => x.name == towerName).GetComponent<Tower>().info;
        return towerInfo != null ? true : false;
    }
}
