using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class EquipmentSlot : MonoBehaviour, IPointerDownHandler
{
    public EquipmentType equipmentType;
    public bool isItemExist
    {
        get
        {
            return _item != null ? true : false;
        }
    }

    private Item _item;
    public Item item
    {
        set
        {
            _item = value;
            if (_item != null)
                _image.sprite = _item.icon;
            else
                _image.sprite = null;
        }

        get
        {
            return _item;
        }
    }

    [SerializeField] private Image _image;

    public void SetUp(Item _item)
    {
        //Debug.Log($"Setup Slot {item.name}, {itemNum}");

        if (_item != null)
        {
            item = _item;
            InventoryDataManager.data.SetEquipmentItemData(equipmentType, _item.name);
            InventoryDataManager.SaveData();
        }
        else
            Clear();

    }

    public void Clear()
    {
        _item = null;
        _image.sprite = null;
        InventoryDataManager.data.RemoveEquipmentItemData(equipmentType);
        InventoryDataManager.SaveData();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isItemExist && 
            InventoryItemHandler.instance.gameObject.activeSelf == false)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (EquipmentHandler.instance.gameObject.activeSelf == false)
                {
                    EquipmentHandler.instance.SetUp(this, _item.icon);
                    EquipmentHandler.instance.gameObject.SetActive(true);
                }
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (Player.instance.Unequip(equipmentType))
                {
                    Clear();
                }
            }
        }
    }
}
