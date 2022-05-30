using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public static bool isReady;

    private static CMDState _CMDState;
    public static CMDState CMDState
    {
        set
        {
            _CMDState = value;
            switch (_CMDState)
            {
                case CMDState.Idle:
                    break;
                case CMDState.Ready:
                    instance._machineManager.controllable = true;
                    break;
                case CMDState.Busy:
                    instance._machineManager.controllable = false;
                    break;
                case CMDState.Error:
                    instance._machineManager.controllable = false;
                    break;
                default:
                    break;
            }
        }

        get
        {
            return _CMDState;
        }
    }


    private Stats _stats;
    public Stats stats
    {
        set
        {
            _stats = value;

            if (StatsView.instance.gameObject.activeSelf)
                StatsView.instance.Refresh();
        }
        get
        {
            return _stats;
        }
    }

    private Stats _additionalStats;
    public Stats additionalStats
    {
        set
        {
            _additionalStats = value;

            if (StatsView.instance.gameObject.activeSelf)
                StatsView.instance.Refresh();
        }
        get
        {
            return _additionalStats;
        }
    }


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
    public Transform leftFootPoint;
    public Transform rightFootPoint;
    public Transform weapon1Point;
    public Transform weapon2Point;
    public Transform ringPoint;
    public Transform necklacePoint;

    [Header("장비")]
    public Weapon1 weapon1;
    public Weapon2 weapon2;
    public Head head;
    public Body body;
    public Foot leftFoot;
    public Foot rightFoot;
    public Ring ring;
    public Necklace necklace;

    private PlayerStateMachineManager _machineManager;

    public void SetUp(PlayerData data)
    {
        stats = data.stats;
        hpMax = stats.HP;
        mpMax = stats.MPMax;
        hp = stats.HP;
        mp = stats.MP;
        lv = stats.LV;
        exp = stats.EXP;

        additionalStats = new Stats();
        Debug.Log("Set player up");
        isReady = true;
    }

    public bool Equip(EquipmentType equipmentType, GameObject equipmentPrefab)
    {
        Unequip(equipmentType);
       
        switch (equipmentType)
        {
            case EquipmentType.Head:
                head = Instantiate(equipmentPrefab, headPoint).GetComponent<Head>();
                break;
            case EquipmentType.Body:
                body = Instantiate(equipmentPrefab, bodyPoint).GetComponent<Body>();
                break;
            case EquipmentType.Foot:
                leftFoot = Instantiate(equipmentPrefab, leftFootPoint).GetComponent<Foot>();
                rightFoot = Instantiate(equipmentPrefab, rightFootPoint).GetComponent<Foot>();
                break;
            case EquipmentType.Weapon1:
                weapon1 = Instantiate(equipmentPrefab, weapon1Point).GetComponent<Weapon1>();
                break;
            case EquipmentType.Weapon2:
                weapon2 = Instantiate(equipmentPrefab, weapon2Point).GetComponent<Weapon2>();
                break;
            case EquipmentType.Ring:
                ring = Instantiate(equipmentPrefab, ringPoint).GetComponent<Ring>();
                break;
            case EquipmentType.Necklace:
                necklace = Instantiate(equipmentPrefab, necklacePoint).GetComponent<Necklace>();
                break;
            default:
                break;
        }

        Equipment component = equipmentPrefab.GetComponent<Equipment>();
        additionalStats += component.additionalStats;
        EquipmentsView.instance.SetSlot(equipmentType,
                                        component.controller.item);
        return true;
    }

    public bool Unequip(EquipmentType equipmentType)
    {
        if (CheckIsEquipmentExist(equipmentType))
        {
            GameObject equipment = null;
            switch (equipmentType)
            {
                case EquipmentType.Head:
                    equipment = headPoint.GetChild(0).gameObject;
                    break;
                case EquipmentType.Body:
                    equipment = bodyPoint.GetChild(0).gameObject;
                    break;
                case EquipmentType.Foot:
                    equipment = leftFootPoint.GetChild(0).gameObject; 
                    break;
                case EquipmentType.Weapon1:
                    equipment = weapon1Point.GetChild(0).gameObject;
                    break;
                case EquipmentType.Weapon2:
                    equipment = weapon2Point.GetChild(0).gameObject;
                    break;
                case EquipmentType.Ring:
                    equipment = ringPoint.GetChild(0).gameObject;
                    break;
                case EquipmentType.Necklace:
                    equipment = necklacePoint.GetChild(0).gameObject;
                    break;
                default:
                    break;
            }

            Equipment component = equipment.GetComponent<Equipment>();
            additionalStats -= component.additionalStats;
            ItemController_Equipment controller = component.controller;
            int remain = InventoryView.instance.GetItemsView(ItemType.Equip).AddItem(controller.item, 1, controller.Use);
            // 장비 해제 성공
            if (remain <= 0)
            {
                Destroy(equipment);
                if (equipmentType == EquipmentType.Foot)
                    Destroy(rightFootPoint.GetChild(0).gameObject);

                return true;
            }
        }
        return false;
    }

    public bool Unequip(EquipmentType equipmentType, InventorySlot slot)
    {
        if (CheckIsEquipmentExist(equipmentType))
        {
            GameObject equipment = null;
            switch (equipmentType)
            {
                case EquipmentType.Head:
                    equipment = headPoint.GetChild(0).gameObject;
                    break;
                case EquipmentType.Body:
                    equipment = bodyPoint.GetChild(0).gameObject;
                    break;
                case EquipmentType.Foot:
                    equipment = leftFootPoint.GetChild(0).gameObject;
                    break;
                case EquipmentType.Weapon1:
                    equipment = weapon1Point.GetChild(0).gameObject;
                    break;
                case EquipmentType.Weapon2:
                    equipment = weapon2Point.GetChild(0).gameObject;
                    break;
                case EquipmentType.Ring:
                    equipment = ringPoint.GetChild(0).gameObject;
                    break;
                case EquipmentType.Necklace:
                    equipment = necklacePoint.GetChild(0).gameObject;
                    break;
                default:
                    break;
            }
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
        _machineManager = GetComponent<PlayerStateMachineManager>();
    }

    private float GetEXPRequired(int level)
    {
        return 1000 + (Mathf.Pow(Mathf.Exp(1), level)) * 10;
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
                return leftFootPoint.childCount > 0 ? true : false;
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
