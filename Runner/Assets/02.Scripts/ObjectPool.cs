using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool _instance;
    public static ObjectPool instance
    {
        get
        {
            if (_instance == null)
                _instance = Instantiate(Resources.Load<ObjectPool>("ObjectPool"));
            return _instance;
        }
    }
    List<PoolElement> poolElements = new List<PoolElement>();
    List<GameObject> spawnedObjects = new List<GameObject>();
    Dictionary<string, Queue<GameObject>> spawnedQueueDictionary = new Dictionary<string, Queue<GameObject>>();

    public void AddPoolElement(PoolElement poolElement)
    {
        poolElements.Add(poolElement);
        Debug.Log($"{poolElement.tag} is registered");
    }

    public static void InstantiateAllElements()
    {
        foreach (var poolElement in instance.poolElements)
        {
            instance.spawnedQueueDictionary.Add(poolElement.tag, new Queue<GameObject>());
            for (int i = 0; i < poolElement.size; i++)
            {
                GameObject obj = instance.CreateNewObject(poolElement.tag, poolElement.prefab);
                instance.ArrangePool(obj);
                instance.spawnedQueueDictionary[obj.name].Enqueue(obj);
            }
        }
    }
    
    public static void ReturnToPool(GameObject obj)
    {
        if (!instance.spawnedQueueDictionary.ContainsKey(obj.name))
            throw new Exception($"Pool doesn't include {obj.name}");

        obj.SetActive(false);
        obj.transform.SetParent(instance.transform);
        instance.spawnedQueueDictionary[obj.name].Enqueue(obj);
    }

    public static GameObject SpawnFromPool(string tag, Vector3 position) =>
        instance.Spawn(tag, position);

    private GameObject Spawn(string tag, Vector3 position)
    {
        if (!spawnedQueueDictionary.ContainsKey(tag))
            throw new Exception($"Pool doesn't contains {tag}");

        Queue<GameObject> queue = spawnedQueueDictionary[tag];
        if(queue.Count == 0)
        {
            PoolElement poolElement = poolElements.Find(x => x.tag == tag);
            var obj = CreateNewObject(poolElement.tag, poolElement.prefab);
            ArrangePool(obj);
        }
        GameObject objectToSpawn = queue.Dequeue();
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = Quaternion.identity;
        objectToSpawn.SetActive(true);

        return objectToSpawn;   
    }

    private GameObject CreateNewObject(string tag, GameObject prefab)
    {
        //Debug.Log($"Create new {tag}, {prefab.name}");
        GameObject obj = Instantiate(prefab, transform);
        obj.name = tag;
        obj.SetActive(false);
        return obj;
    }

    private void ArrangePool(GameObject obj)
    {
        bool isSameNameExist = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            if(i == transform.childCount - 1)
            {
                obj.transform.SetSiblingIndex(i);
                spawnedObjects.Insert(i, obj);
                break;
            }
            else if(transform.GetChild(i).name == obj.name)
                isSameNameExist = true;
            else if (isSameNameExist)
            {
                obj.transform.SetSiblingIndex(i);
                spawnedObjects.Insert(i, obj);
                break;
            }
        }
    }

}

[System.Serializable]
public class PoolElement
{
    public string tag;
    public GameObject prefab;
    public int size;
}
