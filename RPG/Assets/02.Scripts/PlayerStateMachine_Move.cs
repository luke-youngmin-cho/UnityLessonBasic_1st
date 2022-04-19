using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine_Move : PlayerStateMachine
{

    public override bool IsExecuteOK()
    {
        if (controller.isGrounded)
            return true;
        return false;
    }

    public override PlayerState Workflow()
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
                nextState = PlayerState.Move;
                break;
            default:
                break;
        }

        return nextState;
    }

}
