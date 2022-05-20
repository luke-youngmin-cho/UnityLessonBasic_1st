using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public static bool isReady;

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

    public Transform headPoint;
    public Transform bodyPoint;
    public Transform footPoint;
    public Transform weapon1Point;
    public Transform weapon2Point;
    public Transform ringPoint;
    public Transform necklacePoint;

    [Header("장비")]
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

        isReady = true;
    }

    public bool Equip(EquipmentType equipmentType, GameObject equipmentPrefab)
    {
        Unequip(equipmentType);
        GameObject equipment = Instantiate(equipmentPrefab,
                                           GetEquipPoint(equipmentType));

        SetEquipmentInstance(equipmentType, equipment);
        EquipmentsView.instance.SetSlot(equipmentType,
                                        equipmentPrefab.GetComponent<Equipment>().controller.item);
        return true;
    }

    public bool Unequip(EquipmentType equipmentType)
    {
        if (CheckIsEquipmentExist(equipmentType))
        {
            GameObject equipment = GetEquipPoint(equipmentType).GetChild(0).gameObject;
            ItemController_Equipment controller = equipment.GetComponent<Equipment>().controller;
            int remain = InventoryView.instance.GetItemsView(ItemType.Equip).AddItem(controller.item, 1, controller.Use);
            // 장비 해제 성공
            if (remain <= 0)
            {
                Destroy(equipment);
                return true;
            }
        }
        return false;
    }

    public bool Unequip(EquipmentType equipmentType, InventorySlot slot)
    {
        if (CheckIsEquipmentExist(equipmentType))
        {
            GameObject equipment = GetEquipPoint(equipmentType).GetChild(0).gameObject;
            ItemController_Equipment controller = equipment.GetComponent<Equipment>().controller;
            if (slot.isItemExist == false)
            {
                slot.SetUp(controller.item, 1, controller.Use);
                return true;
            }
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

    private Transform GetEquipPoint(EquipmentType equipmentType)
    {
        switch (equipmentType)
        {
            case EquipmentType.Head:
                return headPoint;
            case EquipmentType.Body:
                return bodyPoint;
            case EquipmentType.Foot:
                return footPoint;
            case EquipmentType.Weapon1:
                return weapon1Point;
            case EquipmentType.Weapon2:
                return weapon2Point;
            case EquipmentType.Ring:
                return ringPoint;
            case EquipmentType.Necklace:
                return necklacePoint;
            default:
                throw new System.Exception("치명적인문제! : 잘못된 장비 타입을 가져오려고 시도했습니다");
        }
    }

    private void SetEquipmentInstance(EquipmentType equipmentType, GameObject instance)
    {
        switch (equipmentType)
        {
            case EquipmentType.Head:
                break;
            case EquipmentType.Body:
                break;
            case EquipmentType.Foot:
                break;
            case EquipmentType.Weapon1:
                weapon1 = instance.GetComponent<Weapon1>();
                break;
            case EquipmentType.Weapon2:
                break;
            case EquipmentType.Ring:
                break;
            case EquipmentType.Necklace:
                break;
            default:
                break;
        }
    }

    private bool CheckIsEquipmentExist(EquipmentType equipmentType)
    {
        switch (equipmentType)
        {
            case EquipmentType.Head:
                return headPoint.childCount > 0 ? true : false;
            case EquipmentType.Body:
                return bodyPoint.childCount > 0 ? true : false;
            case EquipmentType.Foot:
                return footPoint.childCount > 0 ? true : false;
            case EquipmentType.Weapon1:
                return weapon1Point.childCount > 0 ? true : false;
            case EquipmentType.Weapon2:
                return weapon2Point.childCount > 0 ? true : false;
            case EquipmentType.Ring:
                return ringPoint.childCount > 0 ? true : false;
            case EquipmentType.Necklace:
                return necklacePoint.childCount > 0 ? true : false;
            default:
                throw new System.Exception("치명적인오류! : 잘못된 장비 정보를 가져오려고 시도함");
        }
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
