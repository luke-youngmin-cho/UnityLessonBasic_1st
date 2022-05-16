using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string nickName;
    public Stats stats;
    

    public PlayerData(string newNickName)
    {
        nickName = newNickName;
        stats = new Stats()
        {
            LV = 0,
            EXP = 0,

            STR = 10,
            DEX = 10,
            INT = 10,
            LUK = 10,

            HP = 100,
            HPMax = 100,
            MP = 50,
            MPMax = 50,

            ATK = 10,
            DEF = 1,

            statPoint = 0
        };
        
    }

    
}

[System.Serializable]
public class Stats
{
    public int LV;
    public int EXP;

    public int STR;
    public int DEX;
    public int INT;
    public int LUK;

    public int HP;
    public int HPMax;
    public int MP;
    public int MPMax;

    public int ATK;
    public int DEF;

    public int statPoint;
}



//[System.Serializable]
//public struct SavePoint
//{
//    public int mapIdx;
//    public float coordx;
//    public float coordy;
//}