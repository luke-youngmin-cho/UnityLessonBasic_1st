using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hpMax;
    private float _hp;
    public float hp
    {
        set
        {
            if (value < 0)
            {
                value = 0;
                // do die
            }

            _hp = value;

            if (PlayerUI.instance != null)
            {
                PlayerUI.instance.SetHPBar(_hp / hpMax);
            }
        }

        get
        {
            return _hp;
        }

    }


    private void Awake()
    {
        hp = hpMax;
    }
}
