using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    State state;

    public virtual bool IsExecuteOK()
    {
        return true;
    }
    public virtual void Execute()
    {
        state = State.Prepare;
    }

    public virtual void UpdateState()
    {

    }

    public virtual void OnReset()
    {

    }

    private void ForceStop()
    {
        OnReset();
        state = State.Idle;
    }

    enum State
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish
    }
}
