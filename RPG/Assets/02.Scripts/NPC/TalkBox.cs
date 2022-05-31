using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TalkBox : MonoBehaviour
{
    public static TalkBox instance;
    public static NPC currentNPC;
    [SerializeField] private Text talkText;
    [SerializeField] private Button interactButton;

    public delegate void InteractEvent();
    InteractEvent IE;

    public void RegisterTalker(NPC npc, InteractEvent interactEvent, string interactEventName)
    {
        currentNPC = npc;
        IE = interactEvent;
 
        if (IE != null)
        {
            interactButton.onClick.AddListener(() => IE());
            interactButton.onClick.AddListener(() => FinishTalk());
            interactButton.transform.GetChild(0).GetComponent<Text>().text = interactEventName;
        }            
    }


    public void FinishTalk()
    {
        Clear();
    }

    public void Clear()
    {
        if (currentNPC != null)
        {
            currentNPC.CMDState = CMDState.Ready;
        }
        currentNPC = null;

        if (IE != null)
        {
            interactButton.onClick.RemoveAllListeners();
            interactButton.transform.GetChild(0).GetComponent<Text>().text = string.Empty;
        }
        IE = null;

        gameObject.SetActive(false);
    }

    public void ShowRandomTalk()
    {
        if (currentNPC != null)
        {
            talkText.text = currentNPC.GetRandomTalk();

            if (IE != null)
                interactButton.gameObject.SetActive(true);
            else
                interactButton.gameObject.SetActive(false);
        }
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        CursorHandler.ShowCursor();
    }

    private void OnDisable()
    {
        if (InGameUIManager.instance != null &&
            InGameUIManager.instance.GetActiveUICount() <= 0)
            CursorHandler.HideCursor();
    }
}
