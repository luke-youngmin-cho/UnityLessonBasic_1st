using System;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public bool isGuided = false;
    public Transform targetGuide;
    public float speed;
    private Vector3 moveVec;
    Transform tr;
    private void Awake()
    {
        tr = transform;
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

    public void SetMoveVector(Vector3 dir)
    {
        moveVec = dir * speed;
    }
}