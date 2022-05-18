using UnityEngine;

public class ItemController_Equipment : ItemController, IUseable
{
    public EquipmentType equipmentType;
    public GameObject equipmentPrefab;

    public virtual void Use()
    {
        InventoryView.instance.GetItemsView(ItemType.Equip).Remove(item, 1);
        Player.instance.Equip(equipmentType, equipmentPrefab);
    }

    public override void PickUp(Player player)
    {
        if (coroutine == null)
        {
            int remain = InventoryView.instance.GetItemsView(item.type).AddItem(item, num, Use);
            Debug.Log($"플레이어가 아이템 {item.name} {num - remain} 개 획득 했습니다");

            if (remain <= 0)
                coroutine = StartCoroutine(E_PickUpEffect(player));
        }
    }
}

public enum EquipmentType
{
    Head,
    Body,
    Foot,
    Weapon1,
    Weapon2,
    Ring,
    Necklace
}