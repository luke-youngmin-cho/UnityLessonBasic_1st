using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentsView : MonoBehaviour
{
    public static EquipmentsView instance;
    public CMDState CMDState;

    [SerializeField] private EquipmentSlot headSlot;
    [SerializeField] private EquipmentSlot bodySlot;
    [SerializeField] private EquipmentSlot footSlot;
    [SerializeField] private EquipmentSlot weapon1Slot;
    [SerializeField] private EquipmentSlot weapon2Slot;
    [SerializeField] private EquipmentSlot ringSlot;
    [SerializeField] private EquipmentSlot necklaceSlot;

    public void SetUp(List<EquipmentItemData> itemsData)
    {
        foreach (var item in itemsData)
        {
            GameObject equipment = ItemAssets.GetItemPrefab(item.itemName).GetComponent<ItemController_Equipment>().equipmentPrefab;
            Player.instance.Equip(item.type, equipment);
        }
    }

    public void SetSlot(EquipmentType equipmentType, Item item)
    {
        switch (equipmentType)
        {
            case EquipmentType.Head:
                headSlot.SetUp(item);
                break;
            case EquipmentType.Body:
                bodySlot.SetUp(item);
                break;
            case EquipmentType.Foot:
                footSlot.SetUp(item);
                break;
            case EquipmentType.Weapon1:
                weapon1Slot.SetUp(item);
                break;
            case EquipmentType.Weapon2:
                weapon2Slot.SetUp(item);
                break;
            case EquipmentType.Ring:
                ringSlot.SetUp(item);
                break;
            case EquipmentType.Necklace:
                necklaceSlot.SetUp(item);
                break;
            default:
                throw new System.Exception("치명적인 에러! 잘못된 장비 타입의 슬롯에 접근하였습니다.");
        }
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
        gameObject.SetActive(false);
        CMDState = CMDState.Ready;
    }
}
