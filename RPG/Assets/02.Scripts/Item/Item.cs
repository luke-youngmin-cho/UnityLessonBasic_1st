
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public string key;
    public ItemType type;
    new public string name;
    public string description;
    public int price;
    public int numMax;
    public Sprite icon;

    public Item(Item copy, string copyKey)
    {
        key = copyKey;
        type = copy.type;
        name = copy.name;
        description = copy.description;
        price = copy.price;
        numMax = copy.numMax;
        icon = copy.icon;
    }
}

public enum ItemType
{
    None, 
    Equip,
    Spend,
    ETC,
}