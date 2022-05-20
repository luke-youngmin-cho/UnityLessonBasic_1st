using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CursorHandler : MonoBehaviour
{
    public static bool controllable = true;
    private GraphicRaycaster _graphicRaycaster; // UI ����ĳ���� �ϴ� ������Ʈ
    private PointerEventData _pointerEventData; // ���콺 �̺�Ʈ ������ 
    private EventSystem _eventSystem; // �̺�Ʈ�� ó���ϴ� ��ü

    private void Awake()
    {
        _graphicRaycaster = GetComponent<GraphicRaycaster>();
        _eventSystem = GetComponent<EventSystem>();
    }

    private void Update()
    {
        if (controllable)
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
                    ShowCursor(); // ���콺 �����ֶ�
                                  // �ȵ�
                else
                    HideCursor(); // ���콺 �����ֶ�
            }
        }
        

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowCursor();
        }
    }


    private void HideCursor()
    {
        if (Cursor.visible)
            Cursor.visible = false;

        if (Cursor.lockState != CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.Locked;
    }

    private void ShowCursor()
    {
        if (Cursor.visible == false)
            Cursor.visible = true;

        if (Cursor.lockState != CursorLockMode.Confined)
            Cursor.lockState = CursorLockMode.Confined;
    }

}
