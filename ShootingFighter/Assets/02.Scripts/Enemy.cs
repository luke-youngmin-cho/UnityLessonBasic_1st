using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    Transform tr;
    private void Awake()
    {
        tr = gameObject.GetComponent<Transform>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 deltaMove = Vector3.back * speed * Time.deltaTime;
        tr.Translate(deltaMove, Space.World);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("PlayerWeapon"))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
