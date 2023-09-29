using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Tower.TowerInfo
{
    public class LaserTowerInfo : AbsTowerInfo
    {
        [SerializeField] protected Transform _startPointLaser;
        [SerializeField] protected Transform _endPointLaser;
        [SerializeField] protected Laser _lazer;

        [SerializeField] protected GameObject _secondPartTower;
        [SerializeField] protected SpriteRenderer _secondPartTowerSpriteRenderer;
        [SerializeField] protected Laser _lazerImprove;
        [SerializeField] protected Transform _startPointLaserImprove;
        [SerializeField] protected Transform _endPointLaserImprove;
        
        [SerializeField] protected float _laserSpawnRate;
        [SerializeField] protected LayerMask _enemyLayer;
        [SerializeField] protected int _damage;
        protected bool _isImproved = false;



        public Laser Lazer { get; }
        public Transform StartPointLaser { get; }
        public Transform EndPointLaser { get; }
        
        public GameObject SecondPartTower { get; }
        public SpriteRenderer SecondPartTowerSpriteRenderer { get; }
        public Laser LazerImprove { get; }
        public Transform StartPointLaserImprove { get; }
        public Transform EndPointLaserImprove { get; }
        
        public float LaserSpawnRate { get; }

        public LayerMask EnemyLayer { get; }
        public int Damage { get; }
        public bool IsImproved { get; }
    }
}