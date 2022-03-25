using System;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    private int _hp;
    public int hp
    {
        set
        {
            if (value > 0)
            {
                _hp = value;
                hpSlider.value = (float)_hp / hpMax;
            }
            else
                gameObject.SetActive(false);
        }
        get
        {
            return _hp;
        }
    }
    public int hpMax;
    public Slider hpSlider;
    private void OnEnable()
    {
        hp = hpMax;
    }

    private void OnDisable()
    {
        ObjectPool.ReturnToPool(gameObject);
    }
}