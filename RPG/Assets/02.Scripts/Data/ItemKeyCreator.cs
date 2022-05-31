
using System;
using UnityEngine;

public class ItemKeyCreator : MonoBehaviour
{
    public static int keyIndex;
    private float resetTime = 2f;
    private float resetTimer = 2f;

    public static string CreateKey(Item item, int num)
    {
        if (PlayerDataManager.data == null) 
            return string.Empty;

        string key = PlayerDataManager.data.nickName +
                     DateTime.Now.ToString() +
                     item.type.ToString() +
                     item.name + 
                     num.ToString() +
                     keyIndex.ToString();

        keyIndex++;
        return key;
    }

    private void Update()
    {
        if (keyIndex != 0)
        {
            if (resetTimer < 0)
                ResetKeyIndex();
            else
                resetTimer -= Time.deltaTime;
        }
    }

    private void ResetKeyIndex()
    {
        keyIndex = 0;
        resetTimer = resetTime;
    }
}
