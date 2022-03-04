using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public int _hp;
    public int hp
    {
        set
        {
            int tmpValue = value;
            if(tmpValue <= 0)
                tmpValue = 0;
            _hp = tmpValue;
            hpText.text = _hp.ToString();
            hpBar.value = (float)_hp / hpMax;
        }
        get { return _hp; }
    }
    public int hpMax;
    public Slider hpBar;
    public Text hpText;

    public bool invincible = false;
    public float invincibleTime = 0.5f;

    private void Awake()
    {
        hp = hpMax;
    }

    public void Hurt(int damage)
    {
        if (invincible) return;
        
        StartCoroutine(E_SetInvincible());

        hp -= damage;
    }
    IEnumerator E_SetInvincible()
    {
        invincible = true;
        yield return new WaitForSeconds(invincibleTime);
        invincible = false;
    }
}
