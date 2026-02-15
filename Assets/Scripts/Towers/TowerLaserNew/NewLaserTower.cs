using System;
using System.Collections;
using System.Linq;
using Assets.Scripts.RepPoolObject;
using Assets.Scripts.Tower.RotationSystem;
using Cysharp.Threading.Tasks;
using Towers.DecelerationSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace Assets.Scripts.Tower.TowerLaserNew
{
    public class NewLaserTower : AbsTower
    {
        public Transform _shootPoint;
        public float _delayTimeToShoot;
        public SpriteRenderer _spriteRendererTower;
        public Sprite[] _spritesTower;
        public SoundType SoundShoot;
        public LineRenderer Laser;
        public float LaserSpeed;
        public Animator ShootAnimator;
        
        public Gradient InitialLaserColor;
        public Gradient DecelerateLaserColor;

        public float CurrentDelayTimeToShoot { get; set; }

        private IFinderObjects _finderObjectsSystem;
        private IFinderObjects _finderObjectsSystemForApplyDamageImproveTower;

        private bool _isImproved;

        public override void StartGame()
        {
            _finderObjectsSystem = new RaycastFinderObjects(_shootPoint, _firingRadius);
            _finderObjectsSystemForApplyDamageImproveTower = new CircleFinderObjects(1);
            RotationSystem = new RotateTargeting(this);
            DecelerationSystem = new DecelerationForNewLaserTower(this);

            _spriteRendererTower.sprite = _spritesTower[0];
            CurrentDelayTimeToShoot = _delayTimeToShoot;

            Laser.positionCount = 2;
            Laser.enabled = false;

            Laser.SetPosition(0, _shootPoint.position);
            Laser.SetPosition(1, _shootPoint.position);

            Laser.colorGradient = InitialLaserColor;
        }

        public override void UpdateGame()
        {
            Transform targetEnemy = GetNearestEnemy(FinderNearestEnemies.Find("Enemy", transform.position));

            if (targetEnemy == null)
                return;
            
            RotationSystem.Rotate(targetEnemy);
            
            if (!_canShoot) 
                return;
            
            foreach (var target in _finderObjectsSystem.Find("Enemy", transform.position))
            {
                if (target != null && target.TryGetComponent(out Enemy enemy))
                {
                    foreach (var type in TargetsEnemyType)
                    {
                        if (enemy.Type == type && enemy.gameObject == targetEnemy.gameObject)
                        {
                            Shoot(enemy).Forget();
                        }
                    }
                    
                    break;
                }
            }
        }

        private bool _canShoot = true;
        
        private async UniTask Shoot(Enemy enemy)
        {
            _canShoot = false;

            Laser.enabled = true;
            Laser.SetPosition(0, _shootPoint.position);

            Vector2 currentPoint = _shootPoint.position;

            while (enemy != null && Vector2.Distance(currentPoint, enemy.transform.position) > 0.1f)
            {
                Laser.SetPosition(0, _shootPoint.position);

                Vector2 direction =
                    ((Vector2)enemy.transform.position - currentPoint).normalized;

                currentPoint += direction * LaserSpeed * Time.deltaTime;
                Laser.SetPosition(1, currentPoint);

                await UniTask.Yield();
            }

            if (enemy != null)
            {
                if(!_isImproved)
                    enemy.ApplayDamage(1);
                else
                {
                    var center = enemy.transform.position;

                    var targets = _finderObjectsSystemForApplyDamageImproveTower
                        .Find("Enemy", center)
                        .ToArray()
                        .Select(t => t.GetComponent<Enemy>())
                        .Where(e => e != null)
                        .OrderBy(e => Vector2.Distance(e.transform.position, center))
                        .Take(2);

                    foreach (var target in targets)
                        target.ApplayDamage(1);

                    ShootAnimator.transform.position = center;
                    ShootAnimator.gameObject.SetActive(true);
                    ShootAnimator.SetTrigger("Play");

                    Observable.Timer(TimeSpan.FromSeconds(0.9f))
                        .Subscribe((_) => ShootAnimator.gameObject.SetActive(false)).AddTo(this);
                }
            }

            Laser.enabled = false;

            await UniTask.Delay(TimeSpan.FromSeconds(CurrentDelayTimeToShoot));
            
            _canShoot = true;
        }

        private void PlaySound(SoundType type)
        {
            // SoundBox sound = (SoundBox)ObjectPooler.Instance.SpawnFromPool("SoundBox",
            //     transform.position,
            //     transform.rotation);
            //
            // sound.PlaySound(type);
        }

        public override void Improve()
        {
            _isImproved = true;
            _spriteRendererTower.sprite = _spritesTower[1];
        }
    }
}