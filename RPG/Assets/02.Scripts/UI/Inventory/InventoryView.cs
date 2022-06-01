using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    public static InventoryView instance;
    public CMDState CMDState;

    [SerializeField] private InventoryItemsView itemsView_Equip;
    [SerializeField] private InventoryItemsView itemsView_Spend;
    [SerializeField] private InventoryItemsView itemsView_ETC;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;

        StartCoroutine(E_Init());
    }

    IEnumerator E_Init()
    {
        itemsView_Equip.gameObject.SetActive(true);
        itemsView_Spend.gameObject.SetActive(true);
        itemsView_ETC.gameObject.SetActive(true);

        yield return new WaitUntil(() => itemsView_Equip.CMDState == CMDState.Ready &&
                                         itemsView_Spend.CMDState == CMDState.Ready &&
                                         itemsView_ETC.CMDState == CMDState.Ready);

        itemsView_Spend.gameObject.SetActive(false);
        itemsView_ETC.gameObject.SetActive(false);
        gameObject.SetActive(false);

        CMDState = CMDState.Ready;
    }

    /// <summary>
    /// ���� �÷��̾��� �κ��丮 �����ͷ� �κ��丮�� ������ 
    /// </summary>
    /// <param name="data"></param>
    public void SetUp(List<InventoryItemData> itemsData)
    {
        for (int i = 0; i < itemsData.Count; i++)
        {
            GameObject prefab = ItemAssets.GetItemPrefab(itemsData[i].itemName); // ������ ���� ������
            ItemController controller = prefab.GetComponent<ItemController>(); // ������ ������ Controller ������Ʈ
            IUseable useable = controller as IUseable; // ������ ���¿� ��밡���� �������̽� ������ ������
            InventorySlot.OnUse onUse = null;
            // ��밡���� �������̸� ����ϴ� �̺�Ʈ�Լ� �븮�ڷ� ���� 
            if (useable != null)
                onUse = useable.Use;

            Item item = new Item(controller.item, itemsData[i].key);

            // �ش� ������Ÿ�Կ� �´� �κ��丮�� �������� �߰���.
            GetItemsView(itemsData[i].type).AddItem(item,
                                                    itemsData[i].num,
                                                    onUse);
        }

        //foreach (InventoryItemData item in itemsData)
        //{
        //    GameObject prefab = ItemAssets.GetItemPrefab(item.itemName); // ������ ���� ������
        //    ItemController controller = prefab.GetComponent<ItemController>(); // ������ ������ Controller ������Ʈ
        //    IUseable useable = controller as IUseable; // ������ ���¿� ��밡���� �������̽� ������ ������
        //    InventorySlot.OnUse onUse = null;
        //    // ��밡���� �������̸� ����ϴ� �̺�Ʈ�Լ� �븮�ڷ� ���� 
        //    if (useable != null)
        //        onUse = useable.Use;
        //
        //    // �ش� ������Ÿ�Կ� �´� �κ��丮�� �������� �߰���.
        //    GetItemsView(item.type).AddItem(controller.item,
        //                                    item.num,
        //                                    onUse);
        //}
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
