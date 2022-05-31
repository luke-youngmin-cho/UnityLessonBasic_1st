using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class TypingEffect : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Text typingText;
    [SerializeField] private float typingDelay = 0.1f;
    [SerializeField] private List<GameObject> gameObjectsForToggle; // 타이핑 이펙트 끝나면 활성/ 비활성화 시킬 게임 오브젝트 리스트
    private string originText;

    private Coroutine coroutine = null;
    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
