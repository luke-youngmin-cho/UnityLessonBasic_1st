using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine_Attack : PlayerStateMachine
{
    private float comboTerm;

    public override bool IsExecuteOK()
    {
        return true;
    }

    public override PlayerState Workflow()
    {
        PlayerState nextState = playerState;

        switch (state)
        {
            case State.Idle:
                break;
            case State.Prepare:
                playerAnimator.SetTrigger("doAttack");
                state++;
                break;
            case State.Casting:
                state++;
                break;
            case State.OnAction:
                if (playerAnimator.IsClipPlaying("Attack") &&
                    Input.GetMouseButton(0))
                {
                    playerAnimator.SetBool("attackComboOn", true);
                }

                break;
            case State.Finish:
                if (playerAnimator.GetBool("attackComboOn"))
                    nextState = PlayerState.Attack;
                else
                    nextState = PlayerState.Move;
                break;
            default:
                break;
        }

        return nextState;
    }

}
