using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemsView : MonoBehaviour
{
    public CMDState CMDState;
    public ItemType itemType;
    public Transform content;
    public int totalSlotNumber = 12;
    public GameObject slotPrefab;
    private List<InventorySlot> slots = new List<InventorySlot>();

    private void Awake()
    {
        SetUp();
    }

    private void SetUp()
    {
        InventorySlot tmpSlot = null;
        for (int i = 0; i < totalSlotNumber; i++)
        {
            tmpSlot = Instantiate(slotPrefab, content).GetComponent<InventorySlot>();
            tmpSlot.id = i;
            tmpSlot.itemType = itemType;
            slots.Add(tmpSlot);
        }
        CMDState = CMDState.Ready;
    }

    public int AddItem(Item item, int itemNum, InventorySlot.OnUse useEvent)
    {
        if (itemNum <= 0) 
            return 0;

        int remain = itemNum;

        InventorySlot tmpSlot = slots.Find(x => x.isItemExist &&
                                                x.item.name == item.name &&
                                                x.num < item.numMax); ;
        // µø¿œ«— æ∆¿Ã≈€¿Ã ¡∏¿Á«œ∏È
        if (tmpSlot != null)
        {
            //Ω¿µÊ«œ∑¡¥¬ ∞πºˆ + ±‚¡∏ æ∆¿Ã≈€ ∞πºˆ∞° √÷¥Î ∞πºˆ ¿Ã«œ¿Ã∏È «ÿ¥ÁΩΩ∑‘ ∞πºˆ √ﬂ∞°
            if (itemNum + tmpSlot.num <= item.numMax)
            {
                tmpSlot.num += itemNum;
                remain = 0;
            }
            else
            {
                remain = tmpSlot.num + itemNum - item.numMax; // ¥Ÿ¿Ω ΩΩ∑‘ø° √ﬂ∞°«“ ∞πºˆ
                int tmp = itemNum - remain; // «ˆ¿Á ΩΩ∑‘ø° √ﬂ∞°«“ ∞πºˆ

                tmpSlot.num += tmp;

                // ∫ÛΩΩ∑‘ ∞Àªˆ
                tmpSlot = slots.Find(x => 
                    (x.isItemExist == false) || 
                    ((x.item.name == item.name) && (x.num < item.numMax))
                );

                // ∫Û ΩΩ∑‘ ¿÷¿∏∏È
                if (tmpSlot != null)
                     return AddItem(item, remain, useEvent);
                else
                    return remain;
            }
        }
        // µø¿œ«— æ∆¿Ã≈€¿Ã æ¯¿∏∏È
        else
        {
            // ∫ÛΩΩ∑‘ ∞Àªˆ
            tmpSlot = slots.Find(x => x.isItemExist == false);
            // ∫Û ΩΩ∑‘ ¿÷¿∏∏È
            if (tmpSlot != null)
            {
                tmpSlot.SetUp(item, itemNum, useEvent);
                remain = 0;
            }
        }
        return remain;
    }

    public bool Remove(Item item, int itemNum)
    {
        if (itemNum <= 0)
            return false;

        InventorySlot tmpSlot = slots.Find(x => x.isItemExist &&
                                                x.item.name == item.name &&
                                                x.num >= itemNum);

        if (tmpSlot != null)
        {
            tmpSlot.num -= itemNum;
            return true;
        }
        return false;
    }

    public bool Remove(string key, Item item, int itemNum)
    {
        if (itemNum <= 0)
            return false;

        InventorySlot tmpSlot = slots.Find(x => x.isItemExist &&
                                                x.item.name == item.name &&
                                                x.num >= itemNum);

        if (tmpSlot != null)
        {
            tmpSlot.num -= itemNum;
            return true;
        }
        return false;
    }
}
