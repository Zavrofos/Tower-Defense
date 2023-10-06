using Assets.Scripts.Tower.FinderEnemyes;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Tower : AbsTower
{
    public Transform _shootPoint;
    public Bullet[] _bulletPrefabs;
    public float _delayTimeToShoot;
    public SpriteRenderer _spriteRendererTower;
    public Sprite[] _spritesTower;
    public Fire _fire;
    public Bullet _currentBullet;
    public float _timeToShoot;
    public AudioSource AudioShoot;

    private RaycastHit2D[] results;
    [SerializeField] private ContactFilter2D contactFilter;

    public override void StartGame()
    {
        FinderEnemyesSystem = new FinderEnemyes(this.gameObject);
        _spriteRendererTower.sprite = _spritesTower[0];
        _currentBullet = _bulletPrefabs[0];
        results = new RaycastHit2D[3];
    }

    public override void UpdateGame()
    {
        if(FinderEnemyesSystem.TargetEnemy == null)
        {
            return;
        }

        RotationSystem.Rotate(FinderEnemyesSystem.TargetEnemy);

        Physics2D.Raycast(transform.position, (_shootPoint.position - transform.position), contactFilter, results, _firingRadius);
        
        foreach (var result in results)
        {
            if (result.collider != null &&
                result.collider.gameObject.TryGetComponent(out Enemy enemy))
            {
                foreach (var type in TargetsEnemyType)
                {
                    if (enemy.Type == type &&
                        enemy.gameObject == FinderEnemyesSystem.TargetEnemy.gameObject)
                    {
                        Shoot();
                    }
                }
                break;
            }
        }
    }

    public override void Shoot()
    {
        _timeToShoot += Time.deltaTime;
        if (_timeToShoot >= _delayTimeToShoot)
        {
            Vector2 direction = GetDirectionToShoot();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            Bullet bullet = Instantiate(_currentBullet, _shootPoint.position, rotation);
            bullet.Direction = direction;
            bullet.StartPosition = RotationSystem.PartToRotate.position;
            bullet.distanceBullet = _firingRadius;
            

            if(bullet is BulletHight)
            {
                bullet.Tower = this;
                _timeToShoot = 0;
                return;
            }

            _fire.gameObject.SetActive(true);
            AudioShoot.Play();
            _timeToShoot = 0;
        }
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
