using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public CMDState CMDState;
    [SerializeField] private string[] talks;
    [SerializeField] private string talkBoxButtonEventName;
    public void StartTalk()
    {
        if (CMDState == CMDState.Busy) return;

        TalkBox.instance.RegisterTalker(this, TalkBoxButtonEvent, talkBoxButtonEventName);
        TalkBox.instance.ShowRandomTalk();
        TalkBox.instance.gameObject.SetActive(true);
        CMDState = CMDState.Busy;
    }

    public string GetRandomTalk()
    {
        return talks[Random.Range(0, talks.Length)];
    }

    protected virtual void TalkBoxButtonEvent()
    {
        // �� NPC ���� �ϰ���� ������ �̺�Ʈ ��� (ex, ���ȭ â ���� / ����Ʈ â ���� ��)
    }

    private void Awake()
    {
        CMDState = CMDState.Ready;
    }
}
