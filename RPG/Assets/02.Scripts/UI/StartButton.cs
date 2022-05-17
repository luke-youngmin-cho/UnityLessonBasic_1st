using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public void OnClick()
    {
        if ( CharacterSelectionView.characterSelected != "")
        {
            PlayerDataManager.LoadData(CharacterSelectionView.characterSelected);
            InventoryDataManager.LoadData(CharacterSelectionView.characterSelected);
            SceneMover.MoveScene("Stage0");
        }
    }
}
