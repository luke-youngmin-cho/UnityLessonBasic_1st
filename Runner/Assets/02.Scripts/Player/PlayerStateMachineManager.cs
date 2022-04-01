using System;
using System.Collections.Generic;
using UnityEngine;
public class PlayerStateMachineManager : MonoBehaviour
{
    public PlayerState state;
    PlayerStateMachine[] playerStateMachines;
    PlayerStateMachine currentMachine;
    KeyCode keyInput;
    
    private void Awake()
    {
        playerStateMachines = GetComponents<PlayerStateMachine>();
    }

    private void Update()
    {
        CompareKeyInput();
        UpdateMachineState();
    }

    /// <summary>
    /// 유저의 키 입력에 맞는 머신이 있는지 체크하고 
    /// 있으면 머신 실행 가능한지 체크하고
    /// 실행 가능하면 실행한다.
    /// </summary>
    private void CompareKeyInput()
    {
        foreach (var machine in playerStateMachines)
        {
            if (keyInput == machine.keyCode)
            {
                if (machine.IsExecuteOK())
                {
                    machine.Execute();
                    currentMachine = machine;
                    state = machine.playerState;
                }   
                keyInput = KeyCode.None;
                break;
            }
        }
    }

    /// <summary>
    /// 현재 선택된 머신이 있으면 해당 머신을 동작시킨다. 
    /// Update()에서 호출해야함
    /// </summary>
    private void UpdateMachineState()
    {
        if (currentMachine != null)
        {
            PlayerState nextState = currentMachine.UpdateState();
            if(state != nextState)
            {
                TryExecuteMachine(nextState);
            }
        }   
    }

    private void TryExecuteMachine(PlayerState newState)
    {        
        foreach (var machine in playerStateMachines)
        {
            // 해당 상태 머신이 있는지 체크 &&
            // 해당 상태 머신이 실행가능한지 체크
            if (machine.playerState == newState &&
               machine.IsExecuteOK())
            {
                // 실행
                machine.Execute();
                currentMachine = machine;
                state = machine.playerState;
            }
        }
    }


    private void OnGUI()
    {
        Event e = Event.current;
        if(e.isKey && e.keyCode != KeyCode.None)
        {
            keyInput = e.keyCode;
        }
    }

}

public enum PlayerState
{
    Idle,
    Run,
    Jump,
    Fall,
    WallRun,
    Roll,
}
