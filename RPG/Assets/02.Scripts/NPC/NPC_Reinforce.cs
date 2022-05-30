using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Reinforce : NPC
{
    [SerializeField] private GameObject reinforceUI;
    protected override void TalkBoxButtonEvent()
    {
        base.TalkBoxButtonEvent();

        reinforceUI.SetActive(true);
    }
}
