using System;
using System.Collections.Generic;
using UnityEngine;
public class GroundDetector : MonoBehaviour
{
    public float offset = 0.1f;
    public bool isDetected;
    public LayerMask groundLayer;
    Transform tr;
    CapsuleCollider col;
    Vector3 detectVec;

    private void Awake()
    {
        tr = GetComponent<Transform>();
        col = GetComponent<CapsuleCollider>();
        detectVec = Vector3.down * (col.height / 2 + offset);
    }

    private void Update()
    {
        // 오버랩된 그라운드가 존재하면 true, 아니면 false
        isDetected = Physics.OverlapBox(tr.position + detectVec,
                                        new Vector3(col.radius, offset, col.radius),
                                        Quaternion.identity,
                                        groundLayer).Length > 0 ? true : false;
    }

    private void OnDrawGizmosSelected()
    {
        tr = GetComponent<Transform>();
        col = GetComponent<CapsuleCollider>();
        detectVec = Vector3.down * (col.height / 2 + offset);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(tr.position + detectVec,
                            new Vector3(col.radius, offset, col.radius));
    }

}
