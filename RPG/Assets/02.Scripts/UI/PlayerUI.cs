using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI instance;

    [SerializeField] private Slider hpBar;
    [SerializeField] private Slider mpBar;
    [SerializeField] private Slider expBar;
    [SerializeField] private Text lvText;
    [SerializeField] private Text nickNameText;

    public void SetHPBar(float value) =>
        hpBar.value = value;

    public void SetMPBar(float value) =>
        mpBar.value = value;

    public void SetEXPBar(float value) => 
        expBar.value = value;

    public void SetLVText(string text) =>
        lvText.text = text;

    public void SetNickNameText(string text) =>
        nickNameText.text = text;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (PlayerDataManager.data != null)
            SetNickNameText(PlayerDataManager.data.nickName);
    }
}
