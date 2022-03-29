using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public static BuffManager instance;
    private void Awake()
    {
        instance = this;
    }

    public static IEnumerator ActiveBuff(Enemy enemy, Buff buff)
    {
        buff.OnActive(enemy);

        bool doBuff = true;
        float timeMark = Time.time;
        while (doBuff &&
               Time.time - timeMark < buff.duration && // 게임시간 - 버프발동시간 < 버프지속시간
               enemy != null)
        {
            doBuff = buff.OnDuration(enemy);
            yield return null; // 해당 반복문이 프레임당 한번 실행되게 하기위함.
        }

        if(enemy != null)
            buff.OnDeactive(enemy);        
    }
}
