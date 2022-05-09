using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class InventorySlot : MonoBehaviour , IPointerDownHandler
{
    public bool isItemExist
    {
        get
        {
            return _num > 0 ? true : false;
        }
    }
    private int _id;
    public int id
    {
        set
        {
            _id = value;
        }

        get
        {
            return _id;
        }
    }
    private int _num;
    public int num
    {
        set
        {
            _num = value;

            if (_num > 1)
                _numText.text = _num.ToString();
            else if (_num == 1)
                _numText.text = "";
            else
            {
                Clear();
            }
        }

        get
        {
            return _num;
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
    [SerializeField] private Text _numText;

    public delegate void OnUse();
    public OnUse _OnUse;

    public void SetUp(Item _item, int _num, OnUse useEvent)
    {
        //Debug.Log($"Setup Slot {item.name}, {itemNum}");

        if (_item != null)
        {
            num = _num;
            item = _item;
            _OnUse = useEvent;
        }
        else
            Clear();

    }

    public void Clear()
    {
        _item = null;
        _num = 0;
        _OnUse = null;
        _numText.text = "";
        _image.sprite = null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isItemExist )
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (InventoryItemHandler.instance.gameObject.activeSelf == false)
                {
                    InventoryItemHandler.instance.SetUp(this, _item.icon);
                    InventoryItemHandler.instance.gameObject.SetActive(true);
                }
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (_OnUse != null)
                    _OnUse();
            }
        }
    }
}
