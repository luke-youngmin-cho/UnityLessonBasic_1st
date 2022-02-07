using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 특정 시간 간격으로 enemy 를 생성하는 스크립트
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnDelay;
    float elapsedTime;

    Transform tr;
    private void Awake()
    {
        tr = gameObject.transform;
    }
    void Update()
    {
        if (elapsedTime > spawnDelay)
        {
            Instantiate(enemyPrefab, tr);
            tr.DetachChildren();
            elapsedTime = 0;
        }
        elapsedTime += Time.deltaTime;
    }
}
