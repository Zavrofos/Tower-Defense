using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class TowerLow : AbsTower
{
    public Transform _shootPoint;
    public Bullet[] _bulletPrefabs;
    public float _delayTimeToShoot;
    public SpriteRenderer _spriteRendererTower;
    public Sprite[] _spritesTower;
    public Fire _fire;
    public Bullet _currentBullet;
    public Vector2 DirectionToShoot;
    public float _timeToShoot;
    public AudioSource AudioShoot;

    private RaycastHit2D[] results;
    [SerializeField] private ContactFilter2D contactFilter;

    public override void StartGame()
    {
        _spriteRendererTower.sprite = _spritesTower[0];
        _currentBullet = _bulletPrefabs[0];
        results = new RaycastHit2D[10];
    }

    public override void UpdateGame()
    {
        DirectionToShoot = GetDirectionToShoot();

        if(FinderEnemyesSystem.TargetEnemy == null)
        {
            return;
        }

        RotationSystem.Rotate(FinderEnemyesSystem.TargetEnemy);

        Physics2D.Raycast(transform.position, (_shootPoint.position - transform.position), contactFilter, results, _firingRadius);
        Debug.DrawRay(transform.position, (_shootPoint.position - transform.position) * _firingRadius, Color.red);

        foreach (var result in results)
        {
            if (result)
            {
                CheckEnemy(result);
                break;
            }
        }
    }
    public void CheckEnemy(RaycastHit2D hit)
    {
        hit.collider.TryGetComponent(out Enemy enemy);
        hit.collider.TryGetComponent(out EnemyFly enemyFly);

        if (enemy != null && enemy.gameObject == FinderEnemyesSystem.TargetEnemy.gameObject || enemyFly != null && enemyFly.gameObject == FinderEnemyesSystem.TargetEnemy.gameObject)
        {
            Shoot();
        }
    }

    public override void Shoot()
    {
        _timeToShoot += Time.deltaTime;
        if (_timeToShoot >= _delayTimeToShoot)
        {
            Bullet bullet = Instantiate(_currentBullet, _shootPoint.position, Quaternion.identity);
            bullet.Direction = DirectionToShoot;
            bullet.StartPosition = RotationSystem.PartToRotate.position;
            bullet.distanceBullet = _firingRadius;
            _timeToShoot = 0;
            _fire.gameObject.SetActive(true);
            AudioShoot.Play();
        }
    }

    public override void Improve()
    {
        _spriteRendererTower.sprite = _spritesTower[1];
        _currentBullet = _bulletPrefabs[1];
        _fire.Transform.localScale = new Vector2(2, 1);
    }
    
    public override Vector2 GetDirectionToShoot()
    {
        Vector3 worldposition = transform.TransformPoint(RotationSystem.PartToRotate.position);
        Vector3 worldPositionPointToShoot = transform.TransformPoint(_shootPoint.position);
        return worldPositionPointToShoot - worldposition;
    }
}
