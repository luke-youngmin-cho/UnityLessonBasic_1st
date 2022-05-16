using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryData
{
    public List<InventoryItemData> items;

    public InventoryData()
    {
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
public class InventoryItemData
{
    public ItemType type;
    public string itemName;
    public int num;
    public int slotID;
}