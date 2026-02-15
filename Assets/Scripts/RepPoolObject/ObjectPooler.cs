using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.RepPoolObject
{
    public class ObjectPooler : MonoBehaviour
    {
        private Dictionary<PolledObjectType, MonoPool<PooledObject>> _poolsMap;
        [SerializeField] private List<PoolInfo> _pools;
        
        public void CreatePool()
        {
            _poolsMap = new Dictionary<PolledObjectType, MonoPool<PooledObject>>();

            foreach (var pool in _pools)
            {
                MonoPool<PooledObject> newPool = new MonoPool<PooledObject>(pool.Prefab, pool.Count, transform);
                _poolsMap.Add(pool.Prefab.Type, newPool);
            }
        }

        public void ClearPool()
        {
            if(_poolsMap == null)
                return;

            foreach (var mono in _poolsMap.Values)
                mono.ClearPool();
            
            _poolsMap.Clear();
        }

        public PooledObject SpawnFromPool(PolledObjectType type, Vector3 position, Quaternion rotation)
        {
            if (!_poolsMap.ContainsKey(type))
            {
                throw new System.Exception($"This tag: {type} is not exist");
            }

            PooledObject objFromPool = _poolsMap[type].GetFreeElement();
            objFromPool.transform.position = position;
            objFromPool.transform.rotation = rotation;
            return objFromPool;
        }

        public void ReturnToPool(PooledObject returnedObject)
        {
            if (!_poolsMap.ContainsKey(returnedObject.Type))
                throw new System.Exception($"This tag: {tag} is not exist");

            _poolsMap[returnedObject.Type].ReturnToPool(returnedObject);
        }
    }
}

