using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMapTrigger : MonoBehaviour
{
    [SerializeField] LayerMask mapSpanwerLayer;
    private void OnTriggerEnter(Collider other)
    {
        if(1<<other.gameObject.layer == mapSpanwerLayer)
        {
            MapSpawner.RemoveFirstAndSpawnNew();
        }
    }
}
