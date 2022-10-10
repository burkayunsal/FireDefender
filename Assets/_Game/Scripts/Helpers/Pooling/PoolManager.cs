using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class PoolManager : Singleton<PoolManager>
{
    [System.Serializable]
    internal struct Pool
    {
        internal Queue<PoolObject> pooledObjects;
        [MustBeAssigned]
        [SerializeField] internal PoolObject objectPrefab;
        [PositiveValueOnly]
        [SerializeField] internal int poolSize;
    }

    [SerializeField]
    private Pool[] pools;


    private void Start()
    {
        CreatePools();
    }

    void CreatePools()
    {
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i].pooledObjects = new Queue<PoolObject>();

            for (int j = 0; j < pools[i].poolSize; j++)
            {
                PoolObject _po = Instantiate(pools[i].objectPrefab, transform);

                pools[i].pooledObjects.Enqueue(_po);

                _po.OnCreated();
            }

        }
    }

    public T GetObject<T>() where T : PoolObject
    {
        for (int i = 0; i < pools.Length; i++)
        {
            if(typeof(T) == pools[i].objectPrefab.GetType())
            {
                T t = pools[i].pooledObjects.Dequeue() as T;
                pools[i].pooledObjects.Enqueue(t);
                t.OnSpawn();
                return t;
            }
        }
        return default;
    }

}
