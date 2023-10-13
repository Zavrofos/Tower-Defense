using Assets.Scripts.RepPoolObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.RepPoolObject
{
    public class ObjectPooler : MonoBehaviour
    {
        private Dictionary<string, MonoPool<PooledObject>> _poolsMap;
        [SerializeField] private List<PoolInfo> _pools;

        public static ObjectPooler Instance;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        public void Initialize()
        {
            _poolsMap = new Dictionary<string, MonoPool<PooledObject>>();

            foreach (var pool in _pools)
            {
                MonoPool<PooledObject> newPool = new MonoPool<PooledObject>(pool.Prefab, pool.Count, transform);
                _poolsMap.Add(pool.Prefab.Tag, newPool);
            }
        }

        public PooledObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
        {
            if (!_poolsMap.ContainsKey(tag))
            {
                throw new System.Exception($"This tag: {tag} is not exist");
            }

            PooledObject objFromPool = _poolsMap[tag].GetFreeElement();
            objFromPool.transform.position = position;
            objFromPool.transform.rotation = rotation;
            return objFromPool;
        }

        public void ReturnToPool(PooledObject returnedObject)
        {
            if (!_poolsMap.ContainsKey(returnedObject.Tag))
            {
                throw new System.Exception($"This tag: {tag} is not exist");
            }

            _poolsMap[returnedObject.Tag].ReturnToPool(returnedObject);
        }
    }
}

