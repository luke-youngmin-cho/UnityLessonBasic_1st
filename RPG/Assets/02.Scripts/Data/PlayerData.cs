using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string nickName;
    public Stats stats;
    public List<InventoryItemData> items;

    public PlayerData(string newNickName)
    {
        nickName = newNickName;
        stats = new Stats()
        {
            LV = 0,
            EXP = 0,

            STR = 10,
            DEX = 10,
            INT = 10,
            LUK = 10,

            HP = 100,
            HPMax = 100,
            MP = 50,
            MPMax = 50,

            ATK = 10,
            DEF = 1,

            statPoint = 0
        };
        items = new List<InventoryItemData>();
    }

    public void SetItemData(ItemType type, string itemName, int num, int slotID)
    {
        InventoryItemData oldData = items.Find(x => x.type == type && x.slotID == slotID);
        if (oldData == null)
            items.Remove(oldData);
        items.Add(new InventoryItemData()
        {
            type = type,
            itemName = itemName,
            num = num,
            slotID = slotID
        });
        // todo -> 데이터 저장하기
    }

    public void RemoveItemData(ItemType type, string itemName, int slotID)
    {
        InventoryItemData oldData = items.Find(x => x.type == type && x.itemName == itemName && x.slotID == slotID);
        if (oldData != null)
        {
            items.Remove(oldData);
            // todo -> 데이터 저장하기
        }   
    }
}

[System.Serializable]
public class Stats
{
    public int LV;
    public int EXP;

    public int STR;
    public int DEX;
    public int INT;
    public int LUK;

    public int HP;
    public int HPMax;
    public int MP;
    public int MPMax;

    public int ATK;
    public int DEF;

    public int statPoint;
}

[System.Serializable]
public class InventoryItemData
{
    public ItemType type;
    public string itemName;
    public int num;
    public int slotID;
}