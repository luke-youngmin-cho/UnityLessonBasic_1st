using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine_Run : PlayerStateMachine
{
    public override bool IsExecuteOK()
    {
        bool isOK = false;
        if (manager.state == PlayerState.Idle)
            isOK = true;
        return isOK;
    }

    public override PlayerState UpdateState()
    {
        PlayerState nextPlayerState = playerState;
        switch (state)
        {
            case State.Idle:
                break;
            case State.Prepare:
                animator.Play("Run");
                state = State.OnAction;
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
}
