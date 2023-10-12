using Assets.Scripts.RepPoolObject;
using System;
using UnityEngine;


namespace Assets.Scripts.RepPoolObject
{
    [Serializable]
    public class PoolInfo
    {
        public PooledObject Prefab;
        public int Count;
    }
}

