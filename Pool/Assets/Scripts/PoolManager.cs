using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager x;
    [SerializeField] Pool[] pools;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void InitializePools()
    {
        foreach (Pool poolRef in pools)
            poolRef.InitializePool();
    }

    public GameObject SpawnObject(GameObject objectToSpawn)
    {
        for (int i = 0; i < pools.Length; i++)
            if (pools[i].GetPoolType().name == objectToSpawn.name)
                return pools[i].SpawnObject();
        CreateNewPool(objectToSpawn);
        return SpawnObject(objectToSpawn);
    }

    public void ReturnObjectToPool(GameObject objectToPool)
    {
        for(int i = 0; i < pools.Length; i++)
        {
            if (pools[i].GetPoolType().name == objectToPool.name)
                pools[i].ReturnToPool(objectToPool);
        }
    }

    private void Init()
    {
        x = this;
        DontDestroyOnLoad(gameObject);
    }

    private void CreateNewPool(GameObject type)
    {
        Pool[] temp = new Pool[pools.Length+1];
        for (int i = 0; i < pools.Length; i++)
            temp[i] = pools[i];
        temp[pools.Length + 1] = new Pool(5, type);
        pools = temp;
    }
}
