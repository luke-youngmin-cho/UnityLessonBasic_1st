using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private static StageManager _instance;
    public static StageManager instance;
    public static StageState state;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform playerSpawnPoint;

    private Player player;

    public void Next() => state++;

    private void Awake()
    {
        if (_instance != null)
            Destroy(_instance);
        instance = this;
    }

    private void Start()
    {
        Next();
    }

    private void Update()
    {
        switch (state)
        {
            // nothing to do 
            case StageState.Idle:
                break;
            // 맵 세팅. 맵생성 & 몬스터 생성 시작
            case StageState.SetUpMap:
                // todo -> Spawn monsters 
                Next();
                break;
            // 맵 세팅 끝날때 까지 기다림
            case StageState.WaitForMapSetUpFinished:
                Next();
                break;
            // 플레이어 생성 및 플레이어 데이터 & 인벤토리 데이터 세팅 시작
            case StageState.SetUpPlayer:
                player = Instantiate(playerPrefab, 
                                     playerSpawnPoint.position, 
                                     Quaternion.identity).GetComponent<Player>();
                player.SetUp(PlayerDataManager.data);
                InventoryView.instance.SetUp(InventoryDataManager.data);
                Next();
                break;
            // 플레이어 및 인벤토리 세팅 끝날때 까지 기다림
            case StageState.WaitForPlayerSetUp:
                if (Player.isReady &&
                    InventoryView.isReady)
                {
                    Next();
                }    
                break;
            // 스테이지 시작
            case StageState.StartStage:
                Next();
                break;
            // 스테이지 진행중
            case StageState.OnStage:
                break;
            default:
                break;
        }
    }
}

public enum StageState
{
    Idle,
    SetUpMap,
    WaitForMapSetUpFinished,
    SetUpPlayer,
    WaitForPlayerSetUp,
    StartStage,
    OnStage,
}