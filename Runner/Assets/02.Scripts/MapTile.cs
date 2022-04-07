using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour
{
    Item[] items;

    private void Awake()
    {
        items = GetComponentsInChildren<Item>();
    }
    private void OnEnable()
    {
        foreach (Item item in items)
            item.gameObject.SetActive(true);
    }
}
