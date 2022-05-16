using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    public static InventoryView instance;

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

    public void SetUp(InventoryData data)
    {
        foreach (InventoryItemData item in data.items)
        {
            GameObject prefab = ItemAssets.GetItemPrefab(item.itemName);
            ItemController controller = prefab.GetComponent<ItemController>();
            IUseable useable = controller as IUseable;
            InventorySlot.OnUse onUse = null;
            if (useable != null)
                onUse = useable.Use;

            GetItemsView(item.type).AddItem(controller.item,
                                            item.num,
                                            onUse);
        }
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
