using System.Collections;
using UnityEngine;

namespace Assets.Scripts.RepPoolObject
{
    public abstract class PooledObject : MonoBehaviour, IPooledObject
    {
        public abstract string Tag { get; }
    }
}