using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    [SerializeField] private GameObject go_poolType;
    [SerializeField] private int i_initialSize;
    private HashSet<IPoolable> p_objects;
    private int i_aliveObjects;

    public Pool(int size, GameObject type)
    {
        go_poolType = type;
        i_initialSize = size;
        p_objects = new HashSet<IPoolable>();
        i_aliveObjects = size;

    }

    public void InitializePool()
    {
        p_objects = new HashSet<IPoolable>();
        i_aliveObjects = i_initialSize;
        for (int i = 0; i < i_initialSize; i++)
            AddNewObject();
    }

    
    public GameObject SpawnObject()
    {
        if(i_aliveObjects > 0)
        {
            GameObject objectToSpawn = GetFromPool();

            if(objectToSpawn != null)
            {
                i_aliveObjects--;
                return objectToSpawn;
            }
        }

        GameObject newGo = AddNewObject();
        newGo.transform.parent = null;
        return newGo;
    }

    public void ReturnToPool(GameObject objectToPool)
    {
        objectToPool.transform.position = PoolManager.x.transform.position;
        objectToPool.transform.parent = PoolManager.x.transform;
        objectToPool.SetActive(false);
        i_aliveObjects++;
    }

    public GameObject GetPoolType()
    {
        return go_poolType;
    }

    private GameObject AddNewObject()
    {
        if (p_objects == null)
            p_objects = new HashSet<IPoolable>();

        GameObject newGo = GameObject.Instantiate(go_poolType);
        newGo.name = newGo.name.Replace("(Clone)", "");
        newGo.transform.parent = PoolManager.x.transform;
        IPoolable poolRef = newGo.GetComponent<IPoolable>();
        if (poolRef != null)
            p_objects.Add(poolRef);

        newGo.SetActive(false);
        return newGo;
    }

    private GameObject GetFromPool()
    {
        foreach(IPoolable poolRef in p_objects)
            if (!poolRef.GetGameObject().activeInHierarchy)
            {
                GameObject goRef = poolRef.GetGameObject();
                goRef.SetActive(true);
                goRef.transform.parent = null;
                return goRef;
            }
        return null;
    }
}
