using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class TypingEffect : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Text typingText;
    [SerializeField] private float typingDelay = 0.1f;
    [SerializeField] private List<GameObject> gameObjectsForToggle; // Ÿ���� ����Ʈ ������ Ȱ��/ ��Ȱ��ȭ ��ų ���� ������Ʈ ����Ʈ
    private string originText;

    private Coroutine coroutine = null;
    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
