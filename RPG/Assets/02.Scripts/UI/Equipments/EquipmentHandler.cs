using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipmentHandler : MonoBehaviour
{
    public static EquipmentHandler instance;
    public Image _image;
    private EquipmentSlot _slot;
    private GraphicRaycaster _graphicRaycaster; // UI 레이캐스팅 하는 컴포넌트
    private PointerEventData _pointerEventData; // 마우스 이벤트 데이터 
    private EventSystem _eventSystem; // 이벤트를 처리하는 객체

    private Coroutine _coroutine;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        instance = this;
        gameObject.SetActive(false);
        _graphicRaycaster = transform.parent.GetComponent<GraphicRaycaster>();
        _eventSystem = transform.parent.GetComponent<EventSystem>();
    }

    private void OnEnable()
    {
        CursorHandler.controllable = false;
    }

    private void OnDisable()
    {
        CursorHandler.controllable = true;
    }

    private void Update()
    {
        // 마우스 왼쪽버튼
        if (_slot != null &&
            Input.GetKeyDown(KeyCode.Mouse0) &&
            InventoryItemHandler.instance.gameObject.activeSelf == false)
        {
            // 발생할 이벤트에 대한 마우스 이벤트 데이터
            _pointerEventData = new PointerEventData(_eventSystem); // 현재 이벤트들에서 마우스 이벤트 데이터만 따로 생성
            _pointerEventData.position = Input.mousePosition; // 마우스 입력 들어왔으니 , 현재 마우스 위치를 마우스 이벤트 데이터 위치로

            List<RaycastResult> results = new List<RaycastResult>(); // 레이캐스트 대상들
            _graphicRaycaster.Raycast(_pointerEventData, results); // UI 레이캐스트

            // UI 캐스트 됨
            if (results.Count > 0)
            {
                foreach (var result in results)
                {
                    // item slot 있는지 
                    if (result.gameObject.TryGetComponent(out InventorySlot inventorySlot))
                    {
                        // 장비 슬롯인지 체크
                        if (inventorySlot.itemType == ItemType.Equip)
                        {
                            // 빈 슬롯 이면 선택된 장비 장착해제 
                            if (inventorySlot.isItemExist == false)
                            {
                                if (Player.instance.Unequip(_slot.equipmentType))
                                {
                                    _slot.Clear();
                                    Clear();
                                    return;
                                }
                            }
                            else
                            {
                                // 빈슬롯 아니고, 슬롯에 있는 아이템의 장비타입과 선택된 장비타입이 같다면 교체
                                if (ItemAssets.GetItemPrefab(inventorySlot.item.name)
                                    .TryGetComponent(out ItemController_Equipment controller))
                                {
                                    if (_slot.equipmentType == controller.equipmentType)
                                    {
                                        inventorySlot._OnUse();
                                        Clear();
                                        return;
                                    }
                                }
                            }                            
                        }

                        break;
                    }
                }
            }
            Clear();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Clear();
        }

    }

    private void FixedUpdate()
    {
        transform.position = Input.mousePosition;
    }

    public void SetUp(EquipmentSlot slot, Sprite icon)
    {
        _slot = slot;
        _image.sprite = icon;
    }

    public void Clear()
    {
        SetUp(null, null);
        if (_coroutine != null)
            StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(E_DeactiveAfterEndOfFrame());
    }

    private IEnumerator E_DeactiveAfterEndOfFrame()
    {
        yield return new WaitForEndOfFrame();
        gameObject.SetActive(false);
    }
}
