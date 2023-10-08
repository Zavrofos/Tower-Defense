using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ObjectPooler
{
    public interface IPooledObject 
    {
        void OnObjectSpawn();
    }
}