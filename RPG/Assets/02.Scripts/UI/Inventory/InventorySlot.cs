using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class InventorySlot : MonoBehaviour , IPointerClickHandler
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
    private string _itemName;
    public string itemName
    {
        get
        {
            return _itemName;
        }
    }
    private string _description;
    private int _num;
    public int num
    {
        set
        {
            _num = value;

            if (_num > 1)
                _numText.text = _num.ToString();
            else
                _numText.text = "";
        }

        get
        {
            return _num;
        }
    }
    private Sprite _icon;
    public Sprite icon
    {
        set
        {
            _icon = value;
            _image.sprite = _icon;
        }

        get
        {
            return _icon;
        }
    }
   

    [SerializeField] private Image _image;
    [SerializeField] private Text _numText;

    public void SetUp(Item item, int itemNum)
    {
        //Debug.Log($"Setup Slot {item.name}, {itemNum}");

        if (item != null)
        {
            _itemName = item.name;
            _description = item.description;
            num = itemNum;
            icon = item.icon;
        }
        else
            Clear();

    }

    public void Clear()
    {
        _itemName = "";
        _description = "";
        num = 0;
        icon = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isItemExist)
        {
            InventoryItemHandler.instance.SetUp(this, _icon);
            InventoryItemHandler.instance.gameObject.SetActive(true);
        }
    }
}
