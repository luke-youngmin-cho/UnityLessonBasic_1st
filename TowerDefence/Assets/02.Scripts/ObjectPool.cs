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
    Dictionary<string, Queue<GameObject>> spawnedQueueDictionrary = new Dictionary<string, Queue<GameObject>>();

    public void AddPoolElement(PoolElement poolElement)
    {
        poolElements.Add(poolElement);
        Debug.Log($"{poolElement.tag} is added on ObjectPool");
    }

    private void Start()
    {
        foreach (PoolElement poolElement in poolElements)
        {
            spawnedQueueDictionrary.Add(poolElement.tag, new Queue<GameObject>());
            for (int i = 0; i < poolElement.size; i++)
            {
                GameObject obj = CreateNewObject(poolElement.tag, poolElement.prefab);
                ArrangePool(obj);
            }
        }
    }

    

    private GameObject CreateNewObject(string tag, GameObject prefab)
    {
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
