using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �κ��丮�� �����۵� ������
/// </summary>
public class InventoryData
{
    public List<InventoryItemData> items; // �κ��丮 ���� �����۵�

    /// <summary>
    /// ����Ʈ �ʱ�ȭ
    /// </summary>
    public InventoryData()
    {
        items = new List<InventoryItemData>();
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
}

/// <summary>
/// ������ ��ü ��ü�� �����ϸ� �ʹ� ũ�⶧���� �����͸� �ּ�ȭ�ؼ� �����ϱ����� ���� ���� ������ �����Ͱ����� Ŭ����
/// </summary>
[System.Serializable]
public class InventoryItemData
{
    public ItemType type; // (���, �Һ�, ��Ÿ)
    public string itemName; // �̸�
    public int num; // ���� ����
    public int slotID; // �ش� �������� �����ϴ� ���� ��ȣ
}