using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DicePlayManager : MonoBehaviour
{
    private int currentTileIndex;
    private int diceNum;
    private int goldenDiceNum;
    public int diceNumInit;
    public int goldenDiceNumInit;
    public List<Transform> list_MapTile;

    private void Awake()
    {
        diceNum = diceNumInit;
        goldenDiceNum = goldenDiceNumInit;
    }
    public void RollADice()
    {
        if (diceNum < 1) return;
        int diceValue = Random.Range(1, 7);
        MovePlayer(diceValue);
    }
    // 해당 눈금만큼 플레이어를 이동
    private void MovePlayer(int diceValue)
    {
        currentTileIndex += diceValue;
        Vector3 target = GetTilePosition(currentTileIndex);
        Player.instance.Move(target);
    }
    private Vector3 GetTilePosition(int tileIndex)
    {   
        int tmpIndex = tileIndex;
        if (tileIndex >= list_MapTile.Count)
        {
            tmpIndex -= (list_MapTile.Count);
            currentTileIndex = tmpIndex;
        }            
        Debug.Log(tmpIndex);

        Vector3 pos = list_MapTile[tmpIndex].position;
        return pos;
    }
}
