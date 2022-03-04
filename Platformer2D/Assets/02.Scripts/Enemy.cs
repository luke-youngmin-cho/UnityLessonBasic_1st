using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public int _hp;
    public int hp
    {
        set
        {
            int tmpValue = value;
            if (tmpValue <= 0)
                tmpValue = 0;
            _hp = tmpValue;
            hpBar.value = (float)_hp / hpMax;
        }
        get { return _hp; }
    }
    public int hpMax;
    public Slider hpBar;

    public int damage = 2;

    private void Awake()
    {
        hp = hpMax;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go == null) return;

        if(go.layer == LayerMask.NameToLayer("Player"))
        {
            Player player = go.GetComponent<Player>();
            PlayerController controller = go.GetComponent<PlayerController>();
            controller.KnockBack();
            player.Hurt(damage);
        }
    }
}
