using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionView : MonoBehaviour
{
    public static CharacterSelectionView instance;
    public static string characterSelected = "";

    [SerializeField] private Transform content;
    [SerializeField] private GameObject slotOrigin;
    [SerializeField] private GameObject selectedOutLine;

    private List<GameObject> slots = new List<GameObject>();
    
    public void Refresh()
    {
        PlayerData[] datas = PlayerDataManager.GetAllDatas();

        for (int i = slots.Count - 1; i > -1; i--)
        {
            Destroy(slots[i]);
            slots.RemoveAt(i);
        }

        for (int i = 0; i < datas.Length; i++)
        {
            string tmpName = datas[i].nickName;
            GameObject slot = Instantiate(slotOrigin, content);
            slot.transform.GetChild(0).GetComponent<Text>().text = "πÃ¡§";
            slot.transform.GetChild(1).GetComponent<Text>().text = tmpName;
            slot.transform.GetChild(2).GetComponent<Text>().text = datas[i].stats.LV.ToString();
            //slot.transform.GetChild(3).GetComponent<RawImage>().texture = ??
            slot.GetComponent<Button>().onClick.AddListener(() => {

                selectedOutLine.GetComponent<SelectedOutLine>().target = slot.transform;
                selectedOutLine.SetActive(true);
                characterSelected = tmpName;
                
            });

            slot.SetActive(true);
            slots.Add(slot);
        }

        for (int i = 0; i < slots.Count; i++)
        {
            GameObject slot = slots[i];
            slot.GetComponent<Button>().onClick.AddListener(() =>
            {
                slot.GetComponent<Image>().color = new Color(0.9117743f, 0.9716981f, 0.308621f);
                for (int j = 0; j < slots.Count; j++)
                {
                    if (slots[j] != slot)
                    {
                        slots[j].GetComponent<Image>().color = Color.white;
                    }
                }
            });
        }


        selectedOutLine.SetActive(false);
        slotOrigin.SetActive(false);
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
    }

    private void Start()
    {
        if (PlayerDataManager.instance != null)
        {
            Refresh();
        }
    }
}
