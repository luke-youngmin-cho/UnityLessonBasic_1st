using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CreateCharacterButton : MonoBehaviour
{
    [SerializeField] private InputField nickNameField;
    public void OnClick()
    {
        if (nickNameField.text.Length > 1)
        {
            PlayerDataManager.CreateData(nickNameField.text);
            InventoryDataManager.CreateData(nickNameField.text);
            CharacterSelectionView.instance.Refresh();
        }
    }
}
