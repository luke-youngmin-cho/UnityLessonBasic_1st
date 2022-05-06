using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public float hpMax;
    private float _hp;
    public float hp
    {
        set
        {
            if (value < 0)
            {
                value = 0;
                // do die
            }
            
            _hp = value;

            if (PlayerUI.instance != null)
            {
                PlayerUI.instance.SetHPBar(_hp / hpMax);
            }
        }

        get
        {
            return _hp;
        }

    }

    public float mpMax;
    private float _mp;
    public float mp
    {
        set
        {
            if (value < 0)
            {
                value = 0;
                // do die
            }

            _mp = value;

            if (PlayerUI.instance != null)
            {
                PlayerUI.instance.SetMPBar(_mp / mpMax);
            }
        }

        get
        {
            return _mp;
        }

    }

    public Transform weapon1Point;

    public bool EquipWeapon1(GameObject weaponPrefab)
    {
        UnequipWeapon1();
        Instantiate(weaponPrefab, weapon1Point);
        
        return true;
    }

    public bool UnequipWeapon1()
    {
        if (weapon1Point.childCount > 0)
        {
            GameObject weapon1 = weapon1Point.GetChild(0).gameObject;
            Item item = weapon1.GetComponent<Equipment>().item;
            InventoryView.instance.GetItemsView(ItemType.Equip).AddItem(item, 1);
            Destroy(weapon1);
            return true;
        }
        return false;
    }

    private void Awake()
    {
        instance = this;
        _hp = hpMax;
    }

    

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            if (Input.GetKey(KeyCode.Z))
                other.gameObject.GetComponent<ItemController>().PickUp(this);
        }
    }
}
