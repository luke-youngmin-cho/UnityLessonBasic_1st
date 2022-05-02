using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour
{
    public bool isItemExist
    {
        get
        {
            return num > 0 ? true : false;
        }
    }
    public int id;
    public string itemName;
    public string description;
    public int num;
    public Sprite icon;

    [SerializeField] private Image _image;

    public void SetUp(Item item, int itemNum)
    {
        Debug.Log($"Setup Slot {item.name}, {itemNum}");
        itemName = item.name;
        description = item.description;
        num = itemNum;
        icon = item.icon;

        _image.sprite = icon;
    }
}
