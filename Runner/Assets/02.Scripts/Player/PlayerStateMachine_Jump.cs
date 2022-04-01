using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine_Jump : PlayerStateMachine
{
    public float jumpForce;
    private GroundDetector groundDetector;
    private Rigidbody rb;
    
    public override void Awake()
    {
        base.Awake();
        groundDetector = GetComponent<GroundDetector>();
        rb = GetComponent<Rigidbody>();
        keyCode = KeyCode.LeftAlt;
    }

    public override bool IsExecuteOK()
    {
        bool isOK = false;
        if (groundDetector.isDetected &&
            (manager.state == PlayerState.Idle || 
             manager.state == PlayerState.Run))
        {
            isOK = true;
        }
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
                rb.velocity = new Vector3(rb.velocity.x,
                                          0,
                                          rb.velocity.z);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                state++;
                break;
            case State.Casting:
                if (groundDetector.isDetected == false)
                    state++;
                break;
            case State.OnAction:
                if (rb.velocity.y < 0)
                    state++;
                break;
            case State.Finish:
                nextPlayerState = PlayerState.Fall;
                break;
            default:
                break;
        }
        return nextPlayerState;
    }

}
