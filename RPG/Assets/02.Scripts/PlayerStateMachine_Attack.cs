using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine_Attack : PlayerStateMachine
{
    public float comboTerm = 1f;
    private float combo1Time;
    private float combo2Time;
    private float combo3Time;
    private float combo4Time;
    private float comboTime;
    private float comboTimer;
    private int comboCount;
    private Coroutine comboCoroutine = null;

    private void Start()
    {
        combo1Time = playerAnimator.GetClipTime("Attack_Combo1");
        combo2Time = playerAnimator.GetClipTime("Attack_Combo2");
        combo3Time = playerAnimator.GetClipTime("Attack_Combo3");
        combo4Time = playerAnimator.GetClipTime("Attack_Combo4");
    }

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
                // 콤보중이면 콤보 카운트 업
                if (playerAnimator.GetBool("attackComboOn") && 
                    comboCount > 3)
                    comboCount++;
                else
                    comboCount = 0;
                playerAnimator.SetInt("attackComboCount", comboCount);

                // 콤보 코루틴 실행중이면 중지
                if (comboCoroutine != null)
                    StopCoroutine(comboCoroutine);

                // 콤보 상태 끔
                playerAnimator.SetBool("attackComboOn", false);

                // 현재 콤보 단계의 애니메이션의 시간 초기화
                comboTimer = comboTime = GetComboTime();                

                // 공격 애니메이션 시작
                playerAnimator.SetTrigger("doAttack");
                state++;
                break;
            case State.Casting:
                if (comboCount < 3)
                    playerAnimator.SetBool("attackComboOn", true);
                state++;
                break;
            case State.OnAction:
                if (comboTimer < 0.1f)
                {
                    if (comboCount < 3)
                        comboCoroutine = StartCoroutine(E_ComboOff());
                    state++;
                }   
                else
                    comboTimer -= Time.deltaTime;
                break;
            case State.Finish:
                nextState = PlayerState.Move;
                break;
            default:
                break;
        }

        return nextState;
    }

    private float GetComboTime()
    {
        if (comboCount == 1)
            return combo2Time;
        else if (comboCount == 2)
            return combo3Time;
        else if (comboCount == 3)
            return combo4Time;
        else
            return combo1Time;
    }

    private IEnumerator E_ComboOff()
    {        
        yield return new WaitForSeconds(comboTerm);
        playerAnimator.SetBool("attackComboOn", false);
    }

}
