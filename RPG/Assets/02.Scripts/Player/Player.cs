using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public Stats stats;
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
            stats.HP = (int)_hp;

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
            stats.MP = (int)_mp;

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

    private float _exp;
    public float exp
    {
        set
        {
            if (value < 0)
            {
                value = 0;
            }

            _exp = value;
            stats.EXP = (int)_exp;

            if (PlayerUI.instance != null)
            {
                PlayerUI.instance.SetEXPBar(_exp / GetEXPRequired(stats.LV));
            }
        }
        get
        {
            return _exp;
        }
    }

    private int _lv;
    public int lv
    {
        set
        {
            _lv = value;
            stats.LV = _lv;

            if (PlayerUI.instance != null)
            {
                PlayerUI.instance.SetLVText(_lv.ToString());
            }
        }
        get
        {
            return _lv;
        }
    }    

    public Transform weapon1Point;

    [Header("Àåºñ")]
    public Weapon1 weapon1;


    public void SetUp(PlayerData data)
    {
        stats = data.stats;
        hpMax = stats.HP;
        mpMax = stats.MPMax;
        hp = stats.HP;
        mp = stats.MP;
        lv = stats.LV;
        exp = stats.EXP;
    }

    public bool EquipWeapon1(GameObject weaponPrefab)
    {
        UnequipWeapon1();
        weapon1 = Instantiate(weaponPrefab, weapon1Point).GetComponent<Weapon1>();
        
        return true;
    }

    public bool UnequipWeapon1()
    {
        if (weapon1Point.childCount > 0)
        {
            GameObject weapon1 = weapon1Point.GetChild(0).gameObject;
            ItemController_Equipment controller = weapon1.GetComponent<Equipment>().controller;
            InventoryView.instance.GetItemsView(ItemType.Equip).AddItem(controller.item, 1, controller.Use);
            Destroy(weapon1);
            return true;
        }
        return false;
    }

    private void Awake()
    {
        instance = this;


        // get equipments
        weapon1 = GetComponentInChildren<Weapon1>();
    }

    private float GetEXPRequired(int level)
    {
        return 1000 + (Mathf.Pow(Mathf.Exp(1), level)) * 10;
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
