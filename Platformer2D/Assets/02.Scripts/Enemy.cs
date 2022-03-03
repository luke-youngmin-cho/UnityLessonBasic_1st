using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int _hp;
    public int hp
    {
        set
        {
            _hp = value;
        }
        get { return _hp; }
    }
    public int hpMax;

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
