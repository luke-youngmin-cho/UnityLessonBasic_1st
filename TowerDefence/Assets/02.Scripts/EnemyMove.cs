using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Transform tr;

    public int wayPointIndex;
    public float speed = 1f;
    public Transform nextWayPoint;
    float originPosY;
    private void Awake()
    {
        tr = transform;
        originPosY = tr.position.y;
    }
    private void Start()
    {
        nextWayPoint = WayPoints.points[0];
    }

    private void OnDisable()
    {
        ObjectPool.ReturnToPool(gameObject);
    }

    private void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(nextWayPoint.position.x, originPosY, nextWayPoint.position.z);
        Vector3 dir = (targetPos - tr.position).normalized;
        if (Vector3.Distance(tr.position, targetPos) < 0.1f)
        {
            if (WayPoints.TryGetNextWayPoint(wayPointIndex, out nextWayPoint))
            {
                wayPointIndex++;
            }   
            else
            {
                OnReachedToEnd();
            }
        }

        tr.Translate(dir * speed * Time.fixedDeltaTime, Space.World);
    }
    private void OnReachedToEnd()
    {
        gameObject.SetActive(false);
    }
    
}
