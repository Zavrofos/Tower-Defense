using Assets.Scripts;
using Assets.Scripts.RepPoolObject;
using Assets.Scripts.Tower.RotationSystem;
using Towers.DecelerationSystems;
using UnityEngine;

namespace Towers.ShootTowers
{
    public class ShootTower : AbsTower
    {
        public Transform _shootPoint;
        public Bullet[] _bulletPrefabs;
        public float _delayTimeToShoot;
        public SpriteRenderer _spriteRendererTower;
        public Sprite[] _spritesTower;
        public Fire _fire;
        public Bullet _currentBullet;
        public float _timeToShoot;
        
        [SerializeField] private AudioClip _shootAudio;

        public float CurrentDelayTimeToShoot { get; set; }

        private IFinderObjects _finderObjectsSystem;

        public override void StartGame()
        {
            _finderObjectsSystem = new RaycastFinderObjects(_shootPoint, _firingRadius);
            RotationSystem = new RotateTargeting(this);
            DecelerationSystem = new DecelerationForShootTower(this);
            _spriteRendererTower.sprite = _spritesTower[0];
            _currentBullet = _bulletPrefabs[0];
            CurrentDelayTimeToShoot = _delayTimeToShoot;
        }

        public override void UpdateGame()
        {
            Transform targetEnemy = GetNearestEnemy(FinderNearestEnemies.Find("Enemy", transform.position));

            if (targetEnemy == null)
                return;

            RotationSystem.Rotate(targetEnemy);

            foreach (var target in _finderObjectsSystem.Find("Enemy", transform.position))
            {
                if (target != null &&
                    target.TryGetComponent(out Enemy enemy))
                {
                    foreach (var type in TargetsEnemyType)
                    {
                        if (enemy.Type == type && enemy.gameObject == targetEnemy.gameObject)
                            Shoot();
                    }
                    break;
                }
            }
        }

        public override void Shoot()
        {
            _timeToShoot += Time.deltaTime;
            if (_timeToShoot >= CurrentDelayTimeToShoot)
            {
                Vector2 direction = GetDirectionToShoot().normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

                Bullet bullet = Instantiate(_currentBullet, _shootPoint.position, rotation);
                bullet.Init(direction, _firingRadius, PartToRotate.position);
                
                if(_fire != null)
                {
                    _fire.gameObject.SetActive(true);
                    PlaySound();
                    _timeToShoot = 0;
                    return;
                }

                _timeToShoot = 0;
            }
        }

        private void PlaySound()
        {
            if(_shootAudio == null)
                return;
            
            SoundBox soundBox = (SoundBox)GameManager.Instance.ObjectPooler.SpawnFromPool(PolledObjectType.SoundBox, transform.position, transform.rotation);
            soundBox.Play(_shootAudio, false);
        }

        public override void Improve()
        {
            _spriteRendererTower.sprite = _spritesTower[1];
            _currentBullet = _bulletPrefabs[1];

            if (_fire != null)
            {
                _fire.Transform.localScale = new Vector2(2, 1);
            }
        }
    
        public override Vector2 GetDirectionToShoot()
        {
            return _shootPoint.position - transform.position;
        }
    }
}
