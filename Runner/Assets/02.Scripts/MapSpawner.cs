using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class MapSpawner : MonoBehaviour
{
    public static MapSpawner instance;
    public static float mapMoveSpeed = 1f;

    [SerializeField] List<Transform> mapTiles;
    [SerializeField] Transform map;
    [SerializeField] LinkedList<Transform> spawnedMapTiles = new LinkedList<Transform>();

    public static void RemoveFirstAndSpawnNew()
    {
        Destroy(instance.spawnedMapTiles.First().gameObject);
        instance.spawnedMapTiles.RemoveFirst();
        instance.SpawnRandomMapTile();
    }

    private void Awake()
    {
        instance = this;
        for (int i = 0; i < map.childCount; i++)
            spawnedMapTiles.AddLast(map.GetChild(i));
    }

    private void FixedUpdate()
    {
        MoveMapTiles();
    }

    private void MoveMapTiles()
    {   
        foreach (var maptile in spawnedMapTiles)
            maptile.position += Vector3.back * mapMoveSpeed * Time.fixedDeltaTime;
    }


    private void SpawnRandomMapTile()
    {
        int randomIndex = Random.Range(0, mapTiles.Count);
        Transform lastMapTile = spawnedMapTiles.Last();
        float lastMapTileLength = lastMapTile.GetComponent<BoxCollider>().size.z;
        float currentMapTileLength = mapTiles[randomIndex].GetComponent<BoxCollider>().size.z;
        Vector3 spawnPos = lastMapTile.position + Vector3.forward * ((lastMapTileLength + currentMapTileLength) / 2);
        Transform spawnedMapTile =  Instantiate(mapTiles[randomIndex],
                                                spawnPos,
                                                Quaternion.identity);
        spawnedMapTiles.AddLast(spawnedMapTile);
    }

}
