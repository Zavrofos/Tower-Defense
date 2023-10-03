using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Tower.TowerInfo
{
    public class ShooterTowerWithShotEfectInfo : ShooterTowerInfo
    {
        [SerializeField] protected GameObject _shotEfect;
        public GameObject ShotEfect { get; }
    }
}