using UnityEngine;

public class ItemController_Equipment : ItemController
{
    public GameObject equipmentPrefab;

    public override void Use()
    {
        base.Use();
        InventoryView.instance.GetItemsView(ItemType.Equip).Remove(item, 1);
    }
}