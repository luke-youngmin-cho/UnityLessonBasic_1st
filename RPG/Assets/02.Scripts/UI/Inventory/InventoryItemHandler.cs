using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class InventoryItemHandler : MonoBehaviour
{
    public static InventoryItemHandler instance;

    public Image _image;

    private InventorySlot _slot;

    private GraphicRaycaster _graphicRaycaster; // UI 레이캐스팅 하는 컴포넌트
    private PointerEventData _pointerEventData; // 마우스 이벤트 데이터 
    private EventSystem _eventSystem; // 이벤트를 처리하는 객체


    private void Update()
    {
        // 마우스 왼쪽버튼
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // 발생할 이벤트에 대한 마우스 이벤트 데이터
            _pointerEventData = new PointerEventData(_eventSystem); // 현재 이벤트들에서 마우스 이벤트 데이터만 따로 생성
            _pointerEventData.position = Input.mousePosition; // 마우스 입력 들어왔으니 , 현재 마우스 위치를 마우스 이벤트 데이터 위치로

            List<RaycastResult> results = new List<RaycastResult>(); // 레이캐스트 대상들
            _graphicRaycaster.Raycast(_pointerEventData, results); // UI 레이캐스트

            // UI 캐스트 됨
            if (results.Count > 0)
            {
                // item slot 있는지 
                foreach (var result in results)
                {
                    if (result.gameObject.TryGetComponent(out InventorySlot slot))
                    {
                        // 슬롯번호와 슬롯에 있는 아이템이름 같으면 암것도 하지않음
                        if (_slot.id == slot.id &&
                            _slot.itemName == slot.itemName )
                        {
                            gameObject.SetActive(false);
                        }
                        // 캐스팅된 슬롯과 원래 슬롯을 스왑함.
                        else
                        {
                            Item oldItem = ItemAssets.GetItem(_slot.itemName);
                            Item newItem = ItemAssets.GetItem(slot.itemName);
                            _slot.SetUp(newItem, slot.num);
                            slot.SetUp(oldItem, _slot.num);
                            gameObject.SetActive(false);
                        }

                        break;
                    }
                }
            
            
            }
        }

    }
    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        instance = this;
        gameObject.SetActive(false);
        _graphicRaycaster = transform.parent.GetComponent<GraphicRaycaster>();
        _eventSystem = transform.parent.GetComponent<EventSystem>();
    }

    private void FixedUpdate()
    {
        transform.position = Input.mousePosition;
    }

    public void SetUp(InventorySlot slot, Sprite icon)
    {
        _slot = slot;
        _image.sprite = icon;
    }
}
