using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �κ��丮�� �����۵� ������
/// </summary>
public class InventoryData
{
    public List<InventoryItemData> items; // �κ��丮 ���� �����۵�
    public List<EquipmentItemData> equipItems; // �������� �����۵�

    /// <summary>
    /// ����Ʈ �ʱ�ȭ
    /// </summary>
    public InventoryData()
    {
        items = new List<InventoryItemData>();
        equipItems = new List<EquipmentItemData>();
    }

    /// <summary>
    /// ������ ������ ����
    /// </summary>
    /// <param name="type"> Ÿ�� ( ���, �Һ�, ��Ÿ ) </param>
    /// <param name="itemName"> ������ �̸� </param>
    /// <param name="num"> ���� ���� </param>
    /// <param name="slotID"> �ش� �������� ������ �κ��丮 ���� </param>
    public void SetItemData(ItemType type, string itemName, int num, int slotID)
    {
        InventoryItemData oldData = items.Find(x => x.type == type && x.slotID == slotID); // �̹� �ش� ���Կ� �������� �����ϴ��� 
        
        // �̹� �ش� ���Կ� ������ �����Ͱ� �����ϸ� ����
        if (oldData != null)
            items.Remove(oldData);

        // ���ڵ�� ������ ������ �߰�
        items.Add(new InventoryItemData()
        {
            type = type,
            itemName = itemName,
            num = num,
            slotID = slotID
        });
    }

    /// <summary>
    /// ������ ������ ����
    /// </summary>
    /// <param name="type"> Ÿ�� ( ���, �Һ�, ��Ÿ ) </param>
    /// <param name="itemName"> ������ �̸� </param>
    /// <param name="slotID"> �ش� �������� �����ϴ� ���� </param>
    public void RemoveItemData(ItemType type, string itemName, int slotID)
    {
        // �ش� ���Կ� �������� �����ϸ� ����
        InventoryItemData oldData = items.Find(x => x.type == type && x.itemName == itemName && x.slotID == slotID);
        if (oldData != null)
        {
            items.Remove(oldData);
        }
    }

    /// <summary>
    /// ����ϰ��ִ� ������ ������ ���� �Լ�
    /// </summary>
    /// <param name="type"> ��� ���� </param>
    /// <param name="itemName"> ��� ������ �̸� </param>
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
    /// ���������� ���� ���������� ȣ���ؼ� ������ ���� �����͸� ������
    /// </summary>
    /// <param name="type"> ��� ���� </param>
    public void RemoveEquipmentItemData(EquipmentType type)
    {
        EquipmentItemData tmpData = equipItems.Find(x => x.type == type);

        if (tmpData != null)
            equipItems.Remove(tmpData);
    }
}

