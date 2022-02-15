using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DicePlayManager : MonoBehaviour
{
    static public DicePlayManager instance;
    private int currentTileIndex;
    private int _diceNum;
    public int diceNum
    {
        set
        {
            _diceNum = value;
            diceNumText.text = _diceNum.ToString();
        }
        get
        {
            return _diceNum;
        }
    }
    private int _goldenDiceNum;
    public int goldenDiceNum
    {
        set
        {
             _goldenDiceNum = value;
            goldenDiceNumText.text = _goldenDiceNum.ToString();
        }
        get
        {
            return _goldenDiceNum;
        }
    }
    public int diceNumInit;
    public int goldenDiceNumInit;
    public List<Transform> list_MapTile;

    public Text diceValueText;
    public Text diceNumText;
    public Text goldenDiceNumText;
    private void Awake()
    {
        instance = this;
        diceNum = diceNumInit;
        goldenDiceNum = goldenDiceNumInit;
    }
    public void RollADice()
    {
        if (diceNum < 1) return;

        diceNum--;
        int diceValue = Random.Range(1, 7);
        diceValueText.text = diceValue.ToString();
        MovePlayer(diceValue);
    }
    public void RollAGoldenDice(int diceValue)
    {
        if (goldenDiceNum < 1) return;

        goldenDiceNum--;
        MovePlayer(diceValue);
    }
    // 해당 눈금만큼 플레이어를 이동
    private void MovePlayer(int diceValue)
    {
        currentTileIndex += diceValue;
        if (currentTileIndex >= list_MapTile.Count)
            currentTileIndex -= (list_MapTile.Count);

        Vector3 target = GetTilePosition(currentTileIndex);
        
        Player.instance.Move(target);
        list_MapTile[currentTileIndex].GetComponent<TileInfo>().TileEvent();
    }
    private Vector3 GetTilePosition(int tileIndex)
    {   
        Vector3 pos = list_MapTile[tileIndex].position;
        return pos;
    }
}
