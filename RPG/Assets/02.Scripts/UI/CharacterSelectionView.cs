using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionView : MonoBehaviour
{
    public static CharacterSelectionView instance;
    [SerializeField] private Transform content;
    [SerializeField] private GameObject slotOrigin;

    private List<GameObject> slots = new List<GameObject>();
    public void Refresh()
    {
        PlayerData[] datas = PlayerDataManager.GetAllDatas();

        for (int i = slots.Count - 1; i > -1; --i)
        {
            Destroy(slots[i]);
            slots.RemoveAt(i);
        }

        for (int i = 0; i < datas.Length; i++)
        {
            GameObject slot = Instantiate(slotOrigin, content);
            slot.transform.GetChild(0).GetComponent<Text>().text = "πÃ¡§";
            slot.transform.GetChild(1).GetComponent<Text>().text = datas[i].nickName;
            slot.transform.GetChild(2).GetComponent<Text>().text = datas[i].stats.LV.ToString();
            //slot.transform.GetChild(3).GetComponent<RawImage>().texture = ??

            slot.SetActive(true);
            slots.Add(slot);
        }
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
