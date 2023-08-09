using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHight : Tower
{
    public Transform _shootPoint;
    public Bullet[] _bulletPrefabs;
    public float _delayTimeToShoot;
    public SpriteRenderer _spriteRendererTower;
    public Sprite[] _spritesTower;
    public Bullet _currentBullet;
    public Transform _targetEnemy;
    public Vector2 DirectionToShoot;
    public float _timeToShoot;
    public AudioSource AudioExplosion;

    private RaycastHit2D[] results;
    private ContactFilter2D contactFilter;

    public override void StartGame()
    {
        InvokeRepeating("UpdateTarget", 0, 1f);
        _spriteRendererTower.sprite = _spritesTower[0];
        _currentBullet = _bulletPrefabs[0];
        results = new RaycastHit2D[10];
        contactFilter = new ContactFilter2D();
        contactFilter.useTriggers = true;
        contactFilter.SetLayerMask(LayerMask.GetMask("Enemy"));
    }

    public override void UpdateGame()
    {
        DirectionToShoot = GetDirectionToShoot();

        if (_targetEnemy == null)
        {
            return;
        }

        Vector2 direction = _targetEnemy.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        _partToRotate.rotation = Quaternion.Lerp(_partToRotate.rotation, rotation, Time.deltaTime * _speedRotation);

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
        if (enemy != null && enemy.gameObject == _targetEnemy.gameObject)
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
            bullet.StartPosition = PositionTower.position;
            bullet.distanceBullet = _firingRadius;
            bullet.Tower = this;
            _timeToShoot = 0;
        }
    }

    public override void Improve()
    {
        _spriteRendererTower.sprite = _spritesTower[1];
        _currentBullet = _bulletPrefabs[1];
    }

    public override void UpdateTarget()
    {
        GameObject[] enemyes = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (var enemy in enemyes)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance < _firingRadius)
        {
            _targetEnemy = nearestEnemy.transform;
        }
        else
        {
            _targetEnemy = null;
        }
    }

    public override Vector2 GetDirectionToShoot()
    {
        Vector3 worldposition = transform.TransformPoint(_partToRotate.position);
        Vector3 worldPositionPointToShoot = transform.TransformPoint(_shootPoint.position);
        return worldPositionPointToShoot - worldposition;
    }
}
