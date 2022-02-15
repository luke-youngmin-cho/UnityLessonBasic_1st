using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo_GoldenDice : TileInfo
{
    public override void TileEvent()
    {
        base.TileEvent();
        DicePlayManager.instance.goldenDiceNum += 1;
    }
}
