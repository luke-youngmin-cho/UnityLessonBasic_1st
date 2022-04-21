using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachineManager : MonoBehaviour
{
    public PlayerState playerState;

    [SerializeField] private Transform cam;
    private Transform tr;
    private PlayerMove playerMove;
    private PlayerAnimator playerAnimator;
    private CharacterController characterController;

    private PlayerStateMachine[] machines;
    private PlayerStateMachine currentMachine;


    private void Awake()
    {
        tr = GetComponent<Transform>();
        playerMove = GetComponent<PlayerMove>();
        playerAnimator = GetComponent<PlayerAnimator>();
        characterController = GetComponent<CharacterController>();
        machines = GetComponents<PlayerStateMachine>();
        currentMachine = machines[0];        
    }


    private void Update()
    {
        // movement
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        playerAnimator.SetFloat("h", h);
        playerAnimator.SetFloat("v", v);

        tr.rotation = Quaternion.Euler(0, cam.eulerAngles.y, 0);
        Vector3 move = cam.rotation * new Vector3(h, 0, v);
        playerMove.SetMove(move.x, move.z);

        // Jump
        if (Input.GetKey(KeyCode.Space))
            ChangePlayerState(PlayerState.Jump);

        // Attack
        if (Input.GetMouseButton(0))
            ChangePlayerState(PlayerState.Attack);            

        UpdatePlayerState();
    }

    private void UpdatePlayerState()
    {
        if (currentMachine != null)
            ChangePlayerState(currentMachine.Workflow()); 
    }

    public void ChangePlayerState(PlayerState newState)
    {
        if (playerState == newState) return;

        // 바꾸려는 머신 검색
        foreach (var sub in machines)
        {
            if (sub.playerState == newState &&
                sub.IsExecuteOK()) // 변경하려는 머신 실행가능하면
            {
                currentMachine.ForceStop(); // 현재 돌아가는 머신 중단
                currentMachine = sub; // 현재 머신 갱신
                currentMachine.Execute(); // 현재 머신 가동
                playerState = newState; // 상태 변경
                return;
            }
        }
    }

}

public enum PlayerState
{
    Idle,
    Move,
    Jump,
    Attack,
}