using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemsView : MonoBehaviour
{
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
            slots.Add(tmpSlot);
        }
    }

    public int AddItem(Item item, int itemNum, InventorySlot.OnUse useEvent)
    {
        if (itemNum <= 0) 
            return 0;

        int remain = itemNum;

        InventorySlot tmpSlot = slots.Find(x => x.isItemExist &&
                                                x.item.name == item.name &&
                                                x.num < item.numMax); ;
        // ������ �������� �����ϸ�
        if (tmpSlot != null)
        {
            //�����Ϸ��� ���� + ���� ������ ������ �ִ� ���� �����̸� �ش罽�� ���� �߰�
            if (itemNum + tmpSlot.num <= item.numMax)
            {
                tmpSlot.num += itemNum;
                remain = 0;
            }
            else
            {
                remain = tmpSlot.num + itemNum - item.numMax; // ���� ���Կ� �߰��� ����
                int tmp = itemNum - remain; // ���� ���Կ� �߰��� ����

                tmpSlot.num += tmp;

                // �󽽷� �˻�
                tmpSlot = slots.Find(x => 
                    (x.isItemExist == false) || 
                    ((x.item.name == item.name) && (x.num < item.numMax))
                );

                // �� ���� ������
                if (tmpSlot != null)
                     return AddItem(item, remain, useEvent);
                else
                    return remain;
            }
        }
        // ������ �������� ������
        else
        {
            // �󽽷� �˻�
            tmpSlot = slots.Find(x => x.isItemExist == false);
            // �� ���� ������
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
}
