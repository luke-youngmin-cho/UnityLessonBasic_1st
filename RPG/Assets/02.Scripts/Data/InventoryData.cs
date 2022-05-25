using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 인벤토리의 아이템들 데이터
/// </summary>
public class InventoryData
{
    public List<InventoryItemData> items; // 인벤토리 내의 아이템들
    public List<EquipmentItemData> equipItems; // 장착중인 아이템들

    /// <summary>
    /// 리스트 초기화
    /// </summary>
    public InventoryData()
    {
        items = new List<InventoryItemData>();
        equipItems = new List<EquipmentItemData>();
    }

    /// <summary>
    /// 아이템 데이터 세팅
    /// </summary>
    /// <param name="type"> 타입 ( 장비, 소비, 기타 ) </param>
    /// <param name="itemName"> 아이템 이름 </param>
    /// <param name="num"> 보유 갯수 </param>
    /// <param name="slotID"> 해당 아이템이 존재한 인벤토리 슬롯 </param>
    public void SetItemData(ItemType type, string itemName, int num, int slotID)
    {
        InventoryItemData oldData = items.Find(x => x.type == type && x.slotID == slotID); // 이미 해당 슬롯에 아이템이 존재하는지 
        
        // 이미 해당 슬롯에 아이템 데이터가 존재하면 삭제
        if (oldData != null)
            items.Remove(oldData);

        // 인자들로 아이템 데이터 추가
        items.Add(new InventoryItemData()
        {
            type = type,
            itemName = itemName,
            num = num,
            slotID = slotID
        });
    }

    /// <summary>
    /// 아이템 데이터 삭제
    /// </summary>
    /// <param name="type"> 타입 ( 장비, 소비, 기타 ) </param>
    /// <param name="itemName"> 아이템 이름 </param>
    /// <param name="slotID"> 해당 아이템이 존재하는 슬롯 </param>
    public void RemoveItemData(ItemType type, string itemName, int slotID)
    {
        // 해당 슬롯에 아이템이 존재하면 삭제
        InventoryItemData oldData = items.Find(x => x.type == type && x.itemName == itemName && x.slotID == slotID);
        if (oldData != null)
        {
            items.Remove(oldData);
        }
    }

    /// <summary>
    /// 장비하고있는 아이템 데이터 세팅 함수
    /// </summary>
    /// <param name="type"> 장비 종류 </param>
    /// <param name="itemName"> 장비 아이템 이름 </param>
    public void SetEquipmentItemData(EquipmentType type, string itemName)
    {
        EquipmentItemData tmpData = equipItems.Find(x => x.type == type);
        if (tmpData != null)
        {
            tmpData.type = type;
            tmpData.itemName = itemName;
        }
        else
        {
            tmpData = new EquipmentItemData()
            {
                type = type,
                itemName = itemName
            };
        }
    }

    /// <summary>
    /// 장비아이템을 장착 해제했을때 호출해서 기존에 장착 데이터를 삭제함
    /// </summary>
    /// <param name="type"> 장비 종류 </param>
    public void RemoveEquipmentItemData(EquipmentType type)
    {
        EquipmentItemData tmpData = equipItems.Find(x => x.type == type);

        if (tmpData != null)
            equipItems.Remove(tmpData);
    }
}

