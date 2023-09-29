using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Tower.TowerInfo
{
    public class ShooterTowerInfo : AbsTowerInfo
    {
        [SerializeField] protected Transform _shootPoint;
        [SerializeField] protected Bullet[] _bulletPrefabs;
        [SerializeField] protected float _delayTimeToShoot;
        [SerializeField] protected SpriteRenderer _spriteRendererTower;
        [SerializeField] protected Sprite[] _spritesTower;
        [SerializeField] protected Bullet _currentBullet;
        [SerializeField] protected AudioSource _audioShoot;

        public Transform ShootPoint { get; }
        public Bullet[] BulletPrefabs { get; }
        public float DelayTimeToShoot { get; }
        public SpriteRenderer SpriteRendererTower { get; }
        public Sprite[] SpritesTower { get; }
        public Bullet CurrentBullet { get; }
        public AudioSource AudioShoot { get; }
}
}