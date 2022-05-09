using UnityEngine;
public class ItemController_Spend : ItemController , IUseable
{
    public virtual void Use()
    {
        InventoryView.instance.GetItemsView(ItemType.Spend).Remove(item, 1);
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