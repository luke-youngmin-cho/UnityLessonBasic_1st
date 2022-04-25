
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public ItemType type;
    new public string name;
    public string description;
    public int price;
    public int numMax;
    public Sprite icon;
}

public enum ItemType
{
    None, 
    Equip,
    Spend,
    ETC,
}