using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Bullet
{
    public float explosionRange;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == null) return;

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRange);
        int length = colliders.Length;
        for (int i = 0; i < length; i++)
        {
            if(colliders[i].gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, colliders[i].transform.position);
                float ratio = (1 - distance / explosionRange);
                colliders[i].gameObject.GetComponent<Enemy>().HP -= damage * ratio;
            }
        }
        Destroy(this.gameObject);
    }
}
