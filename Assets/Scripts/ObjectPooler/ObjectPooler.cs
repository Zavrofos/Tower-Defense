using Assets.Scripts.ObjectPooler;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    private Dictionary<string, Queue<GameObject>> _poolsMap;
    [SerializeField] private List<Pool> _pools;

    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _poolsMap = new Dictionary<string, Queue<GameObject>>();

        foreach(var pool in _pools)
        {
            Queue<GameObject> newPool = new Queue<GameObject>();

            for(int i = 0; i < pool.Count; i++)
            {
                GameObject poolObj = Instantiate(pool.Prefab, transform);
                poolObj.SetActive(false);
                newPool.Enqueue(poolObj);
            }

            _poolsMap.Add(pool.Tag, newPool);
        }
    }


    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation, GameObject sender)
    {
        if (!_poolsMap.ContainsKey(tag))
        {
            throw new System.Exception($"This tag: {tag} is not exist");
        }

        GameObject objToSpawn = _poolsMap[tag].Dequeue();
        objToSpawn.SetActive(true);
        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;

        IPooledObject pooledObject = objToSpawn.GetComponent<IPooledObject>();

        if(pooledObject != null)
        {
            pooledObject.OnObjectSpawn(sender);
        }

        _poolsMap[tag].Enqueue(objToSpawn);

        return objToSpawn;
    }
}

[Serializable]
public class Pool
{
    public string Tag;
    public GameObject Prefab;
    public int Count;
}
