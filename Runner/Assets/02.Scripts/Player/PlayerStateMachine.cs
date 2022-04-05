using System;
using System.Collections.Generic;
using UnityEngine;
public class PlayerStateMachine : MonoBehaviour
{
    public State state;
    public PlayerState playerState;
    public KeyCode keyCode;
    [HideInInspector] public Animator animator;
    [HideInInspector] public PlayerStateMachineManager manager;


    public virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        manager = GetComponent<PlayerStateMachineManager>();
    }

    // 머신 동작 끝났는지 체크
    public bool isFinihsed
    {
        get
        {
            return state == State.Finish;
        }
    }

    /// <summary>
    /// 머신 동작 가능 여부 체크
    /// </summary>
    public virtual bool IsExecuteOK()
    {
        return true;
    }

    /// <summary>
    /// 머신 동작 시작
    /// </summary>
    public virtual void Execute()
    {
        state = State.Prepare;
    }

    /// <summary>
    /// 머신 동작 업데이트
    /// </summary>
    public virtual PlayerState UpdateState()
    {
        PlayerState nextPlayerState = playerState;
        switch (state)
        {
            case State.Idle:
                break;
            case State.Prepare:
                break;
            case State.Casting:
                break;
            case State.OnAction:
                break;
            case State.Finish:
                break;
            default:
                break;
        }
        return nextPlayerState;
    }

    /// <summary>
    /// 머신 강제종료
    /// </summary>
    public virtual void ForceStop()
    {
        state = State.Idle;
    }


    public enum State
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish
    }
}
