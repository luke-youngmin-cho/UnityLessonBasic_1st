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
        }
        else
            Clear();

    }

    public void Clear()
    {
        _item = null;
        _image.sprite = null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isItemExist)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                //if (InventoryItemHandler.instance.gameObject.activeSelf == false)
                //{
                //    InventoryItemHandler.instance.SetUp(this, _item.icon);
                //    InventoryItemHandler.instance.gameObject.SetActive(true);
                //}
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
