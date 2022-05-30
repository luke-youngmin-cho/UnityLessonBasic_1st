using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private string[] talks;
    [SerializeField] private string talkBoxButtonEventName;
    public void StartTalk()
    {
        TalkBox.instance.RegisterTalker(this, TalkBoxButtonEvent, talkBoxButtonEventName);
        TalkBox.instance.ShowRandomTalk();
        TalkBox.instance.gameObject.SetActive(true);
    }

    public string GetRandomTalk()
    {
        return talks[Random.Range(0, talks.Length)];
    }

    protected virtual void TalkBoxButtonEvent()
    {
        // �� NPC ���� �ϰ���� ������ �̺�Ʈ ��� (ex, ���ȭ â ���� / ����Ʈ â ���� ��)
    }
}
