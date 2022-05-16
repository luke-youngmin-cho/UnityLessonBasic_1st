using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveCharacterButton : MonoBehaviour
{
    public void OnClick()
    {
        string tmpName = CharacterSelectionView.characterSelected;
        if (tmpName != "")
        {
            PlayerDataManager.RemoveData(tmpName);
            InventoryDataManager.RemoveData(tmpName);
            CharacterSelectionView.instance.Refresh();
        }
    }
}
