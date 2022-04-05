using System;
using System.Collections.Generic;
using UnityEngine;
public class PlayerStateMachine_WallRun : PlayerStateMachine
{
    private WallDetector wallDetector;
    private Rigidbody rb;

    public override void Awake()
    {
        base.Awake();
        wallDetector = GetComponentInChildren<WallDetector>();
        rb = GetComponent<Rigidbody>();
    }
    public override bool IsExecuteOK()
    {
        bool isOK = false;
        if(wallDetector.isDetected &&
           manager.state == PlayerState.Run ||
           manager.state == PlayerState.Jump ||
           manager.state == PlayerState.Fall)
            isOK = true;
        return isOK;
    }

    public override void Execute()
    {
        base.Execute();
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
    }
    public override PlayerState UpdateState()
    {
        PlayerState nextState = playerState;
        switch (state)
        {
            case State.Idle:
                break;
            case State.Prepare:
                animator.Play("WallRun");                
                state++;
                break;
            case State.Casting:
                state++;
                break;
            case State.OnAction:
                if (wallDetector.isDetected == false)
                    state++;
                break;
            case State.Finish:
                nextState = PlayerState.Run;
                break;
            default:
                break;
        }
        return nextState;
    }

    public override void ForceStop()
    {
        base.ForceStop();
        rb.isKinematic = false;
    }
}
