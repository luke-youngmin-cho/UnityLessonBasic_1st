using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemsView : MonoBehaviour
{
    public Transform content;
    public int totalSlotNumber = 12;
    public GameObject slotPrefab;
    private List<InventorySlot> slots = new List<InventorySlot>();

    private void Start()
    {
        SetUp();
    }

    private void SetUp()
    {
        for (int i = 0; i < totalSlotNumber; i++)
        {
            slots.Add(Instantiate(slotPrefab, content).GetComponent<InventorySlot>());
        }
    }

    public int AddItem(Item item, int itemNum)
    {
        int remain = itemNum;
        InventorySlot tmpSlot = slots.Find(x => x.itemName == item.name);
        // 동일한 아이템이 존재하면
        if (tmpSlot != null)
        {
            //습득하려는 갯수 + 기존 아이템 갯수가 최대 갯수 이하이면 해당슬롯 갯수 추가
            if (itemNum + tmpSlot.num <= item.numMax)
            {
                tmpSlot.num += itemNum;
                remain = 0;
            }
            else
            {
                remain = tmpSlot.num + itemNum - item.numMax; // 다음 슬롯에 추가할 갯수
                int tmp = itemNum - remain; // 현재 슬롯에 추가할 갯수

                tmpSlot.num += tmp;

                // 빈슬롯 검색
                tmpSlot = slots.Find(x => 
                    (x.isItemExist == false) || 
                    ((x.itemName == item.name) && (x.num < item.numMax))
                );

                // 빈 슬롯 있으면
                if (tmpSlot != null)
                     return AddItem(item, remain);
                else
                    return remain;
            }
        }
        // 동일한 아이템이 없으면
        else
        {
            // 빈슬롯 검색
            tmpSlot = slots.Find(x => x.isItemExist == false);
            // 빈 슬롯 있으면
            if (tmpSlot != null)
            {
                tmpSlot.SetUp(item, itemNum);
                remain = 0;
            }
        }
        return remain;
    }
}
