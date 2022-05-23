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
    private GraphicRaycaster _graphicRaycaster; // UI ����ĳ���� �ϴ� ������Ʈ
    private PointerEventData _pointerEventData; // ���콺 �̺�Ʈ ������ 
    private EventSystem _eventSystem; // �̺�Ʈ�� ó���ϴ� ��ü

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
        // ���콺 ���ʹ�ư
        if (_slot != null &&
            Input.GetKeyDown(KeyCode.Mouse0) &&
            InventoryItemHandler.instance.gameObject.activeSelf == false)
        {
            // �߻��� �̺�Ʈ�� ���� ���콺 �̺�Ʈ ������
            _pointerEventData = new PointerEventData(_eventSystem); // ���� �̺�Ʈ�鿡�� ���콺 �̺�Ʈ �����͸� ���� ����
            _pointerEventData.position = Input.mousePosition; // ���콺 �Է� �������� , ���� ���콺 ��ġ�� ���콺 �̺�Ʈ ������ ��ġ��

            List<RaycastResult> results = new List<RaycastResult>(); // ����ĳ��Ʈ ����
            _graphicRaycaster.Raycast(_pointerEventData, results); // UI ����ĳ��Ʈ

            // UI ĳ��Ʈ ��
            if (results.Count > 0)
            {
                foreach (var result in results)
                {
                    // item slot �ִ��� 
                    if (result.gameObject.TryGetComponent(out InventorySlot inventorySlot))
                    {
                        // ��� �������� üũ
                        if (inventorySlot.itemType == ItemType.Equip)
                        {
                            // �� ���� �̸� ���õ� ��� �������� 
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
                                // �󽽷� �ƴϰ�, ���Կ� �ִ� �������� ���Ÿ�԰� ���õ� ���Ÿ���� ���ٸ� ��ü
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
