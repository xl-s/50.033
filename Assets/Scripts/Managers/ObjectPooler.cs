using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ObjectType
{
    goombaEnemy = 0,
    greenEnemy = 1
}

[System.Serializable]
public class ObjectPoolItem
{
    public int amount;
    public GameObject prefab;
    public bool expandPool;
    public ObjectType type;
}

public class ExistingPoolItem
{
    public GameObject gameObject;
    public ObjectType type;

    public ExistingPoolItem(GameObject gameObject, ObjectType type)
    {
        this.gameObject = gameObject;
        this.type = type;
    }
}

public class ObjectPooler : MonoBehaviour
{
    public List<ObjectPoolItem> itemsToPool;
    public List<ExistingPoolItem> pooledObjects;
    public static ObjectPooler SharedInstance;

    void Awake()
    {
        SharedInstance = this;
        this.pooledObjects = new List<ExistingPoolItem>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amount; i++)
            {
                this.AddToPool(item);
            }
        }
    }

    GameObject AddToPool(ObjectPoolItem item)
    {
        GameObject pickup = (GameObject)Instantiate(item.prefab);
        pickup.SetActive(false);
        pickup.transform.parent = this.transform;
        pooledObjects.Add(new ExistingPoolItem(pickup, item.type));
        return pickup;
    }

    public void DeactivateAllObjects()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            pooledObjects[i].gameObject.SetActive(false);
        }
    }

    public GameObject GetPooledObject(ObjectType type)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].gameObject.activeInHierarchy && pooledObjects[i].type == type)
            {
                return pooledObjects[i].gameObject;
            }
        }

        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.type == type)
            {
                if (item.expandPool)
                {
                    return this.AddToPool(item);
                }
            }
        }

        return null;
    }
}