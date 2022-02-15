using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo_Dice : TileInfo
{
    public override void TileEvent()
    {
        base.TileEvent();
        DicePlayManager.instance.diceNum += 1;
    }
}
