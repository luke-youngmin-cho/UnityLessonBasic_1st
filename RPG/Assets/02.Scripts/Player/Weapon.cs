using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private float _damage;
    public float damage
    {
        set
        {
            _damage = value;
        }
    }
    public LayerMask targetLayer;

    

    private void OnCollisionEnter(Collision collision)
    {
        if (1<<collision.gameObject.layer == targetLayer)
        {
            if (collision.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemy.Hurt(_damage);
            }
        }
    }
}
