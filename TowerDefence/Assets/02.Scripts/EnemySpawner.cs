using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] PoolElement[] poolElements;
    private List<GameObject> enemies;
    public int spawnNum;
    public float spawnTimeGap;
    private float elapsedTime;
    Transform tr;
    private void Awake()
    {
        tr = transform;
        enemies = new List<GameObject>();
        foreach (PoolElement poolElement in poolElements)
        {
            ObjectPool.instance.AddPoolElement(poolElement);
        }
    }
    private void Update()
    {
        int num = enemies.Count;
        if(num < spawnNum)
        {
            if (elapsedTime > spawnTimeGap)
            {
                Spawn();
                elapsedTime = 0;
            }
            elapsedTime += Time.deltaTime;
        }
    }
    
    private void Spawn()
    {
        
    }
}
