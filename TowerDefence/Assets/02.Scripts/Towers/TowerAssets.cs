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
        towerName = string.Empty;
        if(level < 4)
        {
            towerName = towers.Find(x => x.name == type.ToString() + level.ToString()).name;
        }
        return towerName != string.Empty ? true : false;
    }

    public bool TryGetTowerByName(string towerName, out Tower tower)
    {
        tower = towers.Find(x => x.name == towerName).GetComponent<Tower>();
        return tower != null ? true : false;
    }
}
