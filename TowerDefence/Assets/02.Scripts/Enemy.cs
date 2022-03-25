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
            _hp = value;
            hpSlider.value = (float)_hp / hpMax;
        }
        get
        {
            return _hp;
        }
    }
    public int hpMax;
    public Slider hpSlider;
    private void Awake()
    {
        hp = hpMax;
    }
}