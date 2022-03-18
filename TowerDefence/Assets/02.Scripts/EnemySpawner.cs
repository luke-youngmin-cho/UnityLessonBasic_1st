using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] SpawnElement[] spawnElements;
    [System.Serializable]
    class SpawnElement
    {
        public PoolElement poolElement;
        public float spawnTimeGap;
        public bool done;
    }
    float[] spawnTimer;
    Transform tr;
    private void Awake()
    {
        tr = transform;
        spawnTimer = new float[spawnElements.Length];

        for (int i = 0; i < spawnElements.Length; i++)
        {
            spawnTimer[i] = spawnElements[i].spawnTimeGap;
            ObjectPool.instance.AddPoolElement(spawnElements[i].poolElement);
        }
    }
    private void Update()
    {
        for (int i = 0; i < spawnElements.Length; i++)
        {
            string tmpTag = spawnElements[i].poolElement.tag;
            int num = ObjectPool.GetSpawnedObjectNumber(tmpTag);

            if (spawnElements[i].done == false){

                if (num < spawnElements[i].poolElement.size)
                {
                    if (spawnTimer[i] < 0)
                    {
                        Spawn(tmpTag);
                        spawnTimer[i] = spawnElements[i].spawnTimeGap;
                    }
                    else
                        spawnTimer[i] -= Time.deltaTime;
                }
                else
                    spawnElements[i].done = true;
            }
        }
        
    }
    
    private void Spawn(string tag)
    {
        ObjectPool.SpawnFromPool(tag, tr.position);
    }
}
