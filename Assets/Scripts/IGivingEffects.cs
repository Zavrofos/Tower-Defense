using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public interface IGivingEffects 
    {
        public int Damage { get; }
        void SetEffect(GameObject target);
    }
}