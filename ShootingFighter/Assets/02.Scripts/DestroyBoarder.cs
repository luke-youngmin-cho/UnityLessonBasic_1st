using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBoarder : MonoBehaviour
{    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == null) return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Destroy(collision.gameObject);
        }

    }
}
