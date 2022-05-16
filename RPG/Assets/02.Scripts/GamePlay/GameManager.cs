using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static GameState state;

    public static void Next()
    {
        state++;
    }


    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Next();
    }

    private void Update()
    {
        switch (state)
        {
            case GameState.Idle:
                break;
            case GameState.StartLoadAssets:
                if( PlayerDataManager.instance != null &&
                    InventoryDataManager.instance != null)
                {
                    Next();
                }
                break;
            case GameState.WaitForAssetsLoaded:
                Next();
                break;
            case GameState.SelectCharacter:
                SceneMover.MoveScene("CharacterSelection");
                Next();
                break;
            case GameState.WaitForCharacterSelected:
                break;
            case GameState.StartStage:
                break;
            case GameState.OnStage:
                break;
            default:
                break;
        }
    }
}


public enum GameState
{
    Idle,
    StartLoadAssets,
    WaitForAssetsLoaded,
    SelectCharacter,
    WaitForCharacterSelected,
    StartStage,
    OnStage,           
}