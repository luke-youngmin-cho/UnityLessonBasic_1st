using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    public static InventoryView instance;
    public static bool isReady;

    [SerializeField] private InventoryItemsView itemsView_Equip;
    [SerializeField] private InventoryItemsView itemsView_Spend;
    [SerializeField] private InventoryItemsView itemsView_ETC;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        itemsView_Equip.gameObject.SetActive(true);
        itemsView_Spend.gameObject.SetActive(false);
        itemsView_ETC.gameObject.SetActive(false);
    }

    /// <summary>
    /// ���� �÷��̾��� �κ��丮 �����ͷ� �κ��丮�� ������ 
    /// </summary>
    /// <param name="data"></param>
    public void SetUp(InventoryData data)
    {
        foreach (InventoryItemData item in data.items)
        {
            GameObject prefab = ItemAssets.GetItemPrefab(item.itemName); // ������ ���� ������
            ItemController controller = prefab.GetComponent<ItemController>(); // ������ ������ Controller ������Ʈ
            IUseable useable = controller as IUseable; // ������ ���¿� ��밡���� �������̽� ������ ������
            InventorySlot.OnUse onUse = null;
            // ��밡���� �������̸� ����ϴ� �̺�Ʈ�Լ� �븮�ڷ� ���� 
            if (useable != null)
                onUse = useable.Use;

            // �ش� ������Ÿ�Կ� �´� �κ��丮�� �������� �߰���.
            GetItemsView(item.type).AddItem(controller.item,
                                            item.num,
                                            onUse);
        }

        isReady = true;
    }

    public InventoryItemsView GetItemsView(ItemType itemType)
    {
        InventoryItemsView tmpView = null;
        switch (itemType)
        {
            case ItemType.None:
                break;
            case ItemType.Equip:
                tmpView = itemsView_Equip;
                break;
            case ItemType.Spend:
                tmpView = itemsView_Spend;
                break;
            case ItemType.ETC:
                tmpView = itemsView_ETC;
                break;
            default:
                break;
        }
        return tmpView;
    }
}
