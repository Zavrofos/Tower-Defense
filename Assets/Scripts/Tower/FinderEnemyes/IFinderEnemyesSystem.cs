using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Tower.FinderEnemyes
{
    public interface IFinderEnemyesSystem
    {
        public Transform TargetEnemy { get; }
        void FindNearbyEnemy();
    }
}