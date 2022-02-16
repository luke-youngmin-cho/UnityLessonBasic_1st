using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo_Inverse : TileInfo
{
    public override void TileEvent()
    {
        base.TileEvent();
        DicePlayUI.instance.SwitchDicePanel();
    }
}
