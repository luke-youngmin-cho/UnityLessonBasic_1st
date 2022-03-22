using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerInfo info;

    private void OnDisable()
    {
        ObjectPool.ReturnToPool(gameObject);
    }
}
