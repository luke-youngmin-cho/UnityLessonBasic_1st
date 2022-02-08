using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    public float damage;
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
        Vector3 deltaMove = Vector3.forward * speed * Time.deltaTime;
        tr.Translate(deltaMove, Space.World);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == null) return;

        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().HP -= damage;
            Destroy(this.gameObject);
        }
    }
}
