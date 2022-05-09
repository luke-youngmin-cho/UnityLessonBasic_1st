using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine_Attack : PlayerStateMachine
{
    public float comboTerm = 1f;
    private float combo1Time;
    private float combo2Time;
    private float combo3Time;
    private float comboTime;
    private float comboTimer;
    private int comboCount;
    private Coroutine comboCoroutine = null;
    public float damage = 10f;

    private void Start()
    {
        combo1Time = playerAnimator.GetClipTime("Attack_Combo1");
        combo2Time = playerAnimator.GetClipTime("Attack_Combo2");
        combo3Time = playerAnimator.GetClipTime("Attack_Combo3");
    }

    public override bool IsExecuteOK()
    {
        if (comboCount == 0 && 
            (manager.playerState == PlayerState.Move && 
             playerAnimator.IsClipPlaying("Movement")))
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
                comboTimer = comboTime = GetComboTime();
                playerAnimator.SetTrigger("doAttack");
                Player.instance.weapon1.doCasting = true;
                state++;
                break;
            case State.Casting:
                
                if (playerAnimator.IsClipPlaying(GetClipName()))
                {
                    comboCount++;
                    playerAnimator.SetInt("attackComboCount", comboCount);

                    Debug.Log($"Attack : detected target count: {Player.instance.weapon1.GetTargets().Count}");
                    // ĳ���� ���� ���⿡ ���� ��� Ÿ�� ������
                    foreach (var target in Player.instance.weapon1.GetTargets())
                    {
                        // Ÿ���� ���ʹ� �̸� ��ġ����
                        if (target.TryGetComponent(out Enemy enemy))
                            enemy.Hurt(damage);
                    }
                    Player.instance.weapon1.doCasting = false;
                    state++;
                }
                break;
            case State.OnAction:

                // ���콺�Է� ������ �״��� �޺� ���� 
                // �ȵ����� �����Ʈ�� ���ư�

                if (Input.GetMouseButton(0))
                {
                    if (comboTimer < 0.6f && 
                        comboCount < 3)
                    {
                        state = State.Prepare;
                    }
                }

                if (comboTimer < 0.5f)
                {
                    if (state != State.Prepare)
                    {
                        state++;
                    }   
                }
                break;
            case State.Finish:
                nextState = PlayerState.Move;
                break;
            default:
                break;
        }

        //Debug.Log(comboTimer);
        comboTimer -= Time.deltaTime;
        return nextState;
    }

    public override void ForceStop()
    {
        base.ForceStop();
        comboCount = 0;
        playerAnimator.SetInt("attackComboCount", comboCount);
    }

    private float GetComboTime()
    {
        if (comboCount == 0)
            return combo1Time;
        else if (comboCount == 1)
            return combo2Time;
        else if (comboCount == 2)
            return combo3Time;
        else
        {
            Debug.LogError("Attack machine : �޺� �ִϸ��̼� �ð��� �������µ� ������ �ֽ��ϴ�.");
            return -1f;
        }
    }

    private string GetClipName()
    {
        if (comboCount == 0)
            return "Attack_Combo1";
        else if (comboCount == 1)
            return "Attack_Combo2";
        else if (comboCount == 2)
            return "Attack_Combo3";
        else
        {
            Debug.LogError("Attack machine : �޺� �ִϸ��̼� �̸��� �������µ� ������ �ֽ��ϴ�.");
            return "";
        }
    }


}
