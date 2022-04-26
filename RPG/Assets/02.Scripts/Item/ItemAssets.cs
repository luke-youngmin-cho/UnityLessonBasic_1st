using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets _instance;
    public static ItemAssets instance
    {
        get
        {
            if (_instance == null)
                _instance = Instantiate(Resources.Load<ItemAssets>("Assets/ItemAssets"));
            return _instance;
        }
    }

    public List<Item> items = new List<Item>();
    public List<GameObject> itemPrefabs = new List<GameObject>();

    public static Item GetItem(string itemName) =>
        instance.items.Find(x => x.name == itemName);

    public static GameObject GetItemPrefab(string itemName) =>
        instance.itemPrefabs.Find(x => x.GetComponent<ItemController>().item.name == itemName);
}
