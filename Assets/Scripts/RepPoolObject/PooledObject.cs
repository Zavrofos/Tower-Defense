using UnityEngine;

namespace Assets.Scripts.RepPoolObject
{
    public enum PolledObjectType
    {
        BulletLow,
        BulletLowPlus,
        BulletMedium,
        BulletMediumPlus,
        BulletHigh,
        BulletHighPlus,
        Rocket,
        Mine,
        Meteor,
        ExplosionRocket,
        ExplosionMine
    }
    
    public abstract class PooledObject : MonoBehaviour, IPooledObject
    {
        public abstract PolledObjectType Type { get; }
    }
}