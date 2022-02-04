using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBoarder : MonoBehaviour
{    
    // OnCollision Event  는 
    // Rigidbody 와 Collider 또는
    // Collider 와 RigidBody 가 충돌할 떄 호출되는 이벤틀함수.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == null) return;

        Destroy(collision.gameObject);
    }
}
