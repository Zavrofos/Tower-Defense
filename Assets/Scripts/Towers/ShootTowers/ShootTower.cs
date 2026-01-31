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
        [SerializeField] private SoundType SoundShoot;

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
            Debug.Log("UpdateGame 1");
            Transform targetEnemy = GetNearestEnemy(FinderNearestEnemies.Find("Enemy", transform.position));

            if (targetEnemy == null)
            {
                Debug.Log("UpdateGame 2");
                return;
            }

            RotationSystem.Rotate(targetEnemy);

            foreach (var target in _finderObjectsSystem.Find("Enemy", transform.position))
            {
                Debug.Log($"UpdateGame 3, target name - {target.name}");
                if (target != null &&
                    target.TryGetComponent(out Enemy enemy))
                {
                    
                    Debug.Log("UpdateGame 4");
                    foreach (var type in TargetsEnemyType)
                    {
                        
                        Debug.Log("UpdateGame 5"); 
                        if (enemy.Type == type &&
                            enemy.gameObject == targetEnemy.gameObject)
                        {
                            Debug.Log("UpdateGame 6");
                            Shoot();
                        }
                    }
                    break;
                }
            }
        }

        public override void Shoot()
        {
            Debug.Log("Shoot 1");
            _timeToShoot += Time.deltaTime;
            if (_timeToShoot >= CurrentDelayTimeToShoot)
            {
                Debug.Log("Shoot 2");
                Vector2 direction = GetDirectionToShoot();
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

                Bullet bullet = Instantiate(_currentBullet, _shootPoint.position, rotation);
                Debug.Log("(test) bullet");
                bullet.Direction = direction;
                bullet.StartPosition = PartToRotate.position;
                bullet.distanceBullet = _firingRadius;
            
                if(_fire != null)
                {
                    Debug.Log("Shoot 3");
                    _fire.gameObject.SetActive(true);
                    PlaySound(SoundShoot);
                    _timeToShoot = 0;
                    return;
                }

                _timeToShoot = 0;
            }
        }

        private void PlaySound(SoundType type)
        {
            SoundBox sound = (SoundBox)ObjectPooler.Instance.SpawnFromPool("SoundBox",
                transform.position,
                transform.rotation);

            sound.PlaySound(type);
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
            Vector3 worldposition = transform.TransformPoint(transform.position);
            Vector3 worldPositionPointToShoot = transform.TransformPoint(_shootPoint.position);
            return worldPositionPointToShoot - worldposition;
        }
    }
}
