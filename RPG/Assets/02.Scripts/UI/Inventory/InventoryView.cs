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
    /// 현재 플레이어의 인벤토리 데이터로 인벤토리를 세팅함 
    /// </summary>
    /// <param name="data"></param>
    public void SetUp(List<InventoryItemData> itemsData)
    {
        for (int i = 0; i < itemsData.Count; i++)
        {
            GameObject prefab = ItemAssets.GetItemPrefab(itemsData[i].itemName); // 아이템 에셋 가져옴
            ItemController controller = prefab.GetComponent<ItemController>(); // 아이템 에셋의 Controller 컴포넌트
            IUseable useable = controller as IUseable; // 아이템 에셋에 사용가능한 인터페이스 있으면 가져옴
            InventorySlot.OnUse onUse = null;
            // 사용가능한 아이템이면 사용하는 이벤트함수 대리자로 전달 
            if (useable != null)
                onUse = useable.Use;

            Item item = new Item(controller.item, itemsData[i].key);

            // 해당 아이템타입에 맞는 인벤토리에 아이템을 추가함.
            GetItemsView(itemsData[i].type).AddItem(item,
                                                    itemsData[i].num,
                                                    onUse);
        }

        //foreach (InventoryItemData item in itemsData)
        //{
        //    GameObject prefab = ItemAssets.GetItemPrefab(item.itemName); // 아이템 에셋 가져옴
        //    ItemController controller = prefab.GetComponent<ItemController>(); // 아이템 에셋의 Controller 컴포넌트
        //    IUseable useable = controller as IUseable; // 아이템 에셋에 사용가능한 인터페이스 있으면 가져옴
        //    InventorySlot.OnUse onUse = null;
        //    // 사용가능한 아이템이면 사용하는 이벤트함수 대리자로 전달 
        //    if (useable != null)
        //        onUse = useable.Use;
        //
        //    // 해당 아이템타입에 맞는 인벤토리에 아이템을 추가함.
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
