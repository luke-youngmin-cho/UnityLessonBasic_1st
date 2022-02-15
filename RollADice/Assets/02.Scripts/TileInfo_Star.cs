using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TileInfo_Star : TileInfo
{
    private int _starValue;
    public int starValue
    {
        set
        {
            _starValue = value;
            starValueText.text = _starValue.ToString();
        }
        get
        {
            return _starValue;
        }
    }
    [SerializeField] Text starValueText;
    private void Awake()
    {
        starValue = 3;
    }
    public override void TileEvent()
    {
        base.TileEvent();
        starValue += 1;
    }
}
