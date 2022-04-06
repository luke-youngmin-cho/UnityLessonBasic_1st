using System;
using System.Collections.Generic;
using UnityEngine;
public class PlayerStateMachine_WallRun : PlayerStateMachine
{
    private WallDetector wallDetector;
    private Rigidbody rb;

    private float animationTime_Start;
    private float animationTime_Finish;
    private float animationTimer;

    public override void Awake()
    {
        base.Awake();
        wallDetector = GetComponentInChildren<WallDetector>();
        rb = GetComponent<Rigidbody>();

        RuntimeAnimatorController ac = animator.runtimeAnimatorController;
        for (int i = 0; i < ac.animationClips.Length; i++)
        {
            if (ac.animationClips[i].name == "WallRunStart")
                animationTime_Start = ac.animationClips[i].length;
            else if (ac.animationClips[i].name == "WallRunFinish")
                animationTime_Finish = ac.animationClips[i].length;
        }
    }
    public override bool IsExecuteOK()
    {
        bool isOK = false;
        if(wallDetector.isDetected &&
           (manager.state == PlayerState.Run ||
            manager.state == PlayerState.Jump||
            manager.state == PlayerState.Fall ))
            isOK = true;
        return isOK;
    }

    public override void Execute()
    {
        base.Execute();
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        animator.Play("WallRunStart");
        animationTimer = animationTime_Start;
    }
    public override PlayerState UpdateState()
    {
        PlayerState nextState = playerState;
        switch (state)
        {
            case State.Idle:
                break;
            case State.Prepare:
                if (animationTimer <= 0)
                {
                    animator.Play("WallRun");
                    state++;
                }
                else
                    animationTimer -= Time.deltaTime;
                break;
            case State.Casting:
                if (wallDetector.isDetected == false)
                {
                    animator.Play("WallRunFinish");
                    animationTimer = animationTime_Finish;
                    state++;
                }
                else
                    animationTimer -= Time.deltaTime;
                break;
            case State.OnAction:
                if (animationTimer <= 0)
                    state++;
                else
                    animationTimer -= Time.deltaTime;
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
