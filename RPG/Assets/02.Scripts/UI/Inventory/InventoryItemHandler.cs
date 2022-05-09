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

    private GraphicRaycaster _graphicRaycaster; // UI ����ĳ���� �ϴ� ������Ʈ
    private PointerEventData _pointerEventData; // ���콺 �̺�Ʈ ������ 
    private EventSystem _eventSystem; // �̺�Ʈ�� ó���ϴ� ��ü


    private void Update()
    {
        // ���콺 ���ʹ�ư
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // �߻��� �̺�Ʈ�� ���� ���콺 �̺�Ʈ ������
            _pointerEventData = new PointerEventData(_eventSystem); // ���� �̺�Ʈ�鿡�� ���콺 �̺�Ʈ �����͸� ���� ����
            _pointerEventData.position = Input.mousePosition; // ���콺 �Է� �������� , ���� ���콺 ��ġ�� ���콺 �̺�Ʈ ������ ��ġ��

            List<RaycastResult> results = new List<RaycastResult>(); // ����ĳ��Ʈ ����
            _graphicRaycaster.Raycast(_pointerEventData, results); // UI ����ĳ��Ʈ

            // UI ĳ��Ʈ ��
            if (results.Count > 0)
            {
                bool isSlotExist = false;
                // item slot �ִ��� 
                foreach (var result in results)
                {
                    if (result.gameObject.TryGetComponent(out InventorySlot slot))
                    {
                        // ���Թ�ȣ�� ���Կ� �ִ� �������̸� ������ �ϰ͵� ��������
                        if (_slot.id == slot.id &&
                            _slot.item.name == slot.item.name )
                        {
                            gameObject.SetActive(false);
                        }
                        // ĳ���õ� ���԰� ���� ������ ������.
                        else
                        {
                            Item tmpItem = slot.item;
                            int tmpNum = slot.num;
                            InventorySlot.OnUse tmpOnUse = slot._OnUse;
                            slot.SetUp(_slot.item, _slot.num, _slot._OnUse);
                            _slot.SetUp(tmpItem, tmpNum, tmpOnUse);

                            Clear();
                        }
                        isSlotExist = true;
                        break;
                    }
                }
                //���� ������
                if (isSlotExist == false)
                    Clear();

            }
            // �ʵ忡 ���콺 ���� Ŭ�� �����Ƿ� ������ ���
            else
            {
                // ����� ������ ���
                GameObject tmpPrefab = ItemAssets.GetItemPrefab(_slot.item.name);
                if (tmpPrefab != null)
                {
                    _slot.Clear();
                    Instantiate(tmpPrefab, Player.instance.transform.position, Quaternion.identity);
                }   
                Clear();
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Clear();
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

    public void Clear()
    {
        SetUp(null, null);
        gameObject.SetActive(false);
    }
}
