using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.RepPoolObject
{
    public class MonoPool<T> where T : PooledObject
    {
        private T _prefab;
        private Transform _conteiner;
        private List<T> _pool;

        public MonoPool(T prefab, int count, Transform conteiner)
        {
            _prefab = prefab;
            _conteiner = conteiner;
            CreatePool(count);
        }

        private void CreatePool(int count)
        {
            _pool = new List<T>();

            for(int i =0; i < count; i++)
            {
                CreateObject();
            }
        }

        private T CreateObject(bool isActiveByDefault = false)
        {
            T pooledObj = GameObject.Instantiate(_prefab, _conteiner);
            pooledObj.gameObject.SetActive(isActiveByDefault);
            _pool.Add(pooledObj);
            return pooledObj;
        }

        private bool HasFreeElement(out T element)
        {
            foreach(var el in _pool)
            {
                if(!el.gameObject.activeInHierarchy)
                {
                    element = el;
                    element.gameObject.SetActive(true);
                    return true;
                }
            }

            element = null;
            return false;
        }

        public T GetFreeElement()
        {
            if(HasFreeElement(out T element))
            {
                return element;
            }

            return CreateObject(true);
        }

        public void ReturnToPool(PooledObject returnedObject)
        {
            returnedObject.gameObject.SetActive(false);
        }
    }
}