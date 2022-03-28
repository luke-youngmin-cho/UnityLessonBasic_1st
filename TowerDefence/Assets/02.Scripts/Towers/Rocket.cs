using System;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public bool isGuided = false;
    public Transform targetGuide;
    public float speed;
    private int _damage;
    public LayerMask touchLayer;
    public LayerMask targetLayer;
    public float explosionRange;

    private Vector3 moveVec;
    Transform tr;
    private void Awake()
    {
        tr = transform;
    }

    private void Update()
    {
        Collider[] cols = Physics.OverlapSphere(tr.position, 1f, touchLayer);
        if (cols.Length > 0)
            Explode();
    }
    private void FixedUpdate()
    {
        if (isGuided)
        {   
            tr.LookAt(targetGuide);
            moveVec = (targetGuide.position - tr.position).normalized * speed;
        }
        tr.Translate(moveVec);
    }

    public void Setup(Vector3 dir, Transform target , int damage)
    {
        moveVec = dir * speed;
        targetGuide = target;
        _damage = damage;
    }


    private void Explode()
    {
        Collider[] enemiesCol = Physics.OverlapSphere(tr.position, explosionRange, targetLayer);
        foreach (var enemyCol in enemiesCol)
        {
            enemyCol.GetComponent<Enemy>().hp -= _damage;
        }
        Destroy(gameObject);
    }
}