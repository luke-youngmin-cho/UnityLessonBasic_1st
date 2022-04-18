using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState playerState;
    public State state;
    [HideInInspector] public PlayerStateMachineManager manager;
    [HideInInspector] public PlayerAnimator playerAnimator;

    public virtual void Awake()
    {
        manager = GetComponent<PlayerStateMachineManager>();
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    public virtual bool IsExecuteOK()
    {
        return true;
    }

    public virtual void Execute()
    {
        state = State.Prepare;
    }

    public virtual PlayerState Workflow()
    {
        PlayerState nextState = playerState;

        switch (state)
        {
            case State.Idle:
                break;
            case State.Prepare:
                state++;
                break;
            case State.Casting:
                state++;
                break;
            case State.OnAction:
                state++;
                break;
            case State.Finish:
                break;
            default:
                break;
        }

        return nextState;
    }

    public virtual void ForceStop()
    {

    }

    public enum State
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish,
    }
}
