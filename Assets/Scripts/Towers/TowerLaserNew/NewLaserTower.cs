using System;
using System.Linq;
using System.Threading;
using Assets.Scripts.Tower.RotationSystem;
using Cysharp.Threading.Tasks;
using Towers.DecelerationSystems;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Tower.TowerLaserNew
{
    public class NewLaserTower : AbsTower
    {
        public SpriteRenderer LaserSprite;
        public Transform _shootPoint;
        public float _delayTimeToShoot;
        public SpriteRenderer _spriteRendererTower;
        public Sprite[] _spritesTower;
        public SoundType SoundShoot;
        public float LaserSpeed;
        public Animator ShootAnimator;

        public Color InitialColorTower;
        public Color DecelerateColorTower;
        public Color InitialLaserColor;
        public Color DecelerateLaserColor;

        public float CurrentDelayTimeToShoot { get; set; }

        private IFinderObjects _finderObjectsSystem;
        private IFinderObjects _finderObjectsSystemForApplyDamageImproveTower;

        private bool _isImproved;
        private CancellationTokenSource _cancellationTokenSource;

        public override void StartGame()
        {
            _finderObjectsSystem = new RaycastFinderObjects(_shootPoint, _firingRadius);
            _finderObjectsSystemForApplyDamageImproveTower = new CircleFinderObjects(2);
            RotationSystem = new RotateTargeting(this);
            DecelerationSystem = new DecelerationForLaseTowerNew(this);

            _spriteRendererTower.sprite = _spritesTower[0];
            CurrentDelayTimeToShoot = _delayTimeToShoot;

            Vector2 size = LaserSprite.size;
            size.y = 0;
            LaserSprite.size = size;

            _cancellationTokenSource = new CancellationTokenSource();
        }

        public override void UpdateGame()
        {
            Transform targetEnemy = GetNearestEnemy(FinderNearestEnemies.Find("Enemy", transform.position));

            if (targetEnemy == null)
                return;
            
            if(_rotation)
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
                            Shoot(enemy, _cancellationTokenSource.Token).Forget();
                        }
                    }
                    
                    break;
                }
            }
        }

        private bool _canShoot = true;
        private bool _rotation = true;
        
        private async UniTask Shoot(Enemy enemy, CancellationToken token)
        {
            _canShoot = false;
            _rotation = false;
            
            Vector2 currentPoint = _shootPoint.position;
            float sizeY = Vector2.Distance(enemy.transform.position, currentPoint);
            
            Vector2 size = LaserSprite.size;
            size.y = sizeY;
            LaserSprite.size = size;

            await UniTask.Yield(token).SuppressCancellationThrow();
            
            if(token.IsCancellationRequested)
                return;
            
            if (enemy != null)
            {
                var center = enemy.transform.position;
                
                if (enemy.name.Contains("Boss"))
                {
                    enemy.ApplayDamage(_isImproved ? 50 : 30);
                }
                else
                {
                    if(!_isImproved)
                        enemy.ApplayDamage(500);
                    else
                    {
                        var targets = _finderObjectsSystemForApplyDamageImproveTower
                            .Find("Enemy", center)
                            .ToArray()
                            .Select(t => t.GetComponent<Enemy>())
                            .Where(e => e != null)
                            .OrderBy(e => Vector2.Distance(e.transform.position, center))
                            .Take(2);

                        foreach (var target in targets)
                            target.ApplayDamage(500);
                    }
                }

                if (_isImproved)
                {
                    ShootAnimator.transform.position = center;
                    ShootAnimator.gameObject.SetActive(true);
                    ShootAnimator.SetTrigger("Play");

                    Observable.Timer(TimeSpan.FromSeconds(0.9f))
                        .Subscribe((_) => ShootAnimator.gameObject.SetActive(false)).AddTo(this);
                }
            }

            await UniTask.Delay(TimeSpan.FromSeconds(0.2f), cancellationToken: token).SuppressCancellationThrow();
            
            if(token.IsCancellationRequested)
                return;
            
            size = LaserSprite.size;
            size.y = 0;
            LaserSprite.size = size;
            _rotation = true;

            await UniTask.Delay(TimeSpan.FromSeconds(CurrentDelayTimeToShoot), cancellationToken: token).SuppressCancellationThrow();
            
            if(token.IsCancellationRequested)
                return;
            
            _canShoot = true;
        }

        public override void Improve()
        {
            _isImproved = true;
            _spriteRendererTower.sprite = _spritesTower[1];
        }

        public override void Destroy()
        {
            base.Destroy();
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }
    }
}