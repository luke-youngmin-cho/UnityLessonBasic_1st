using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RacingPlay : MonoBehaviour
{
    #region 싱글톤패턴
    static public RacingPlay instance;

    private void Awake()
    {
        if(instance == null) instance = this;   
    }
    #endregion

    private List<PlayerMove> list_PlayerMove = new List<PlayerMove>();
    private List<Transform> list_FinishedPlayer = new List<Transform>();
    [SerializeField] private List<Transform> list_WinPlatform = new List<Transform>();
    private int totalPlayerNum;
    private int grade;
    [SerializeField] Transform goal;
    [SerializeField] Text grade1PlayerNameText;
    private void Update()
    {
        CheckPlayerReachedToGoalAndStopMove();
    }
    public void Register(PlayerMove playerMove)
    {
        list_PlayerMove.Add(playerMove);
        totalPlayerNum++;
        Debug.Log($"{playerMove.gameObject.name} (이)가 등록 완료 되었습니다, 현재 총 등록수 : {list_PlayerMove.Count}");
    }
    public void StartRacing()
    {
        foreach (PlayerMove playerMove in list_PlayerMove)
        {
            playerMove.doMove = true;
        }
    }
    private void CheckPlayerReachedToGoalAndStopMove()
    {
        PlayerMove tmpFinishedPlayerMove = null;
        //플레이어가 목표지점에 도착했는지 체크
        foreach (PlayerMove playerMove in list_PlayerMove)
        {
            if(playerMove.transform.position.z > goal.position.z)
            {
                tmpFinishedPlayerMove = playerMove;
                break;
            }
        }
        // 플레이어가 목표지점에 도달했을때
        if(tmpFinishedPlayerMove != null)
        {
            tmpFinishedPlayerMove.doMove = false;
            list_FinishedPlayer.Add(tmpFinishedPlayerMove.transform);
            list_PlayerMove.Remove(tmpFinishedPlayerMove);
        }
        // 경주가 끝났다면 ( 모든 플레이어가 목표를 통과했을 때)
        if(list_FinishedPlayer.Count == totalPlayerNum)
        {
            // 1,2,3 등은 단상에 위치시키고, 카메라는 단상을 찍도록 한다.
            for (int i = 0; i < list_WinPlatform.Count; i++)
            {
                list_FinishedPlayer[i].position = list_WinPlatform[i].position;
            }
            CameraHandler.instance.MoveToPlatform();
            // 1등 친구 이름 텍스트 업데이트 
            grade1PlayerNameText.text = list_FinishedPlayer[0].name;
            grade1PlayerNameText.gameObject.SetActive(true);
        }
    }
    public Transform GetPlayer(int index)
    {
        // 함수의 반환용 지역변수의 선언과 초기화는 함수의 가장 상단에 한다.
        Transform tmpPlayerTransform = null; 

        // 함수내용 : 연산에 따라 반환용 지역변수에 값을 대입한다.
        if (index < list_PlayerMove.Count)
        {
             tmpPlayerTransform = list_PlayerMove[index].transform;
        }
        // 함수의 가장 하단에는 반환용 지역변수를 반환한다.
        return tmpPlayerTransform;
    }
    public Transform Get1GradePlayer()
    {
        Transform leader = list_PlayerMove[0].gameObject.GetComponent<Transform>();
        float prevDistance = list_PlayerMove[0].distance;
        foreach (PlayerMove playerMove in list_PlayerMove)
        {
            if(playerMove.distance > prevDistance)
            {
                prevDistance = playerMove.distance;
                leader = playerMove.gameObject.GetComponent<Transform>();
            }
        }
        return leader;
    }

    public int GetTotalPlayerNumber()
    {
        return list_PlayerMove.Count;
    }
}