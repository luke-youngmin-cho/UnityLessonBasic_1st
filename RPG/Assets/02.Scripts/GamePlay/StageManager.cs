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
            // �� ����. �ʻ��� & ���� ���� ����
            case StageState.SetUpMap:
                // todo -> Spawn monsters 
                Next();
                break;
            // �� ���� ������ ���� ��ٸ�
            case StageState.WaitForMapSetUpFinished:
                Next();
                break;
            // �÷��̾� ���� �� �÷��̾� ������ & �κ��丮 ������ ���� ����
            case StageState.SetUpPlayer:
                player = Instantiate(playerPrefab, 
                                     playerSpawnPoint.position, 
                                     Quaternion.identity).GetComponent<Player>();
                player.SetUp(PlayerDataManager.data);
                InventoryView.instance.SetUp(InventoryDataManager.data);
                Next();
                break;
            // �÷��̾� �� �κ��丮 ���� ������ ���� ��ٸ�
            case StageState.WaitForPlayerSetUp:
                if (Player.isReady &&
                    InventoryView.isReady)
                {
                    Next();
                }    
                break;
            // �������� ����
            case StageState.StartStage:
                Next();
                break;
            // �������� ������
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