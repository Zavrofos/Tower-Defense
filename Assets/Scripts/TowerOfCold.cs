using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class TowerOfCold : Tower
{
    public Transform _shootPoint;
    public float _delayTimeToShoot;
    public SpriteRenderer _spriteRendererTower;
    public Sprite[] _spritesTower;
    public Vector2 DirectionToShoot;
    public float _timeToShoot;
    public ParticleSystem _coldEfect;
    public Transform _targetEnemy;
    public LayerMask enemyLayer;

    private RaycastHit2D[] results;
    private ContactFilter2D contactFilter;
    [SerializeField] private AudioSource AudioCold;
    private GameManagerInGame _gameManager;

    

    public override void StartGame()
    {
        InvokeRepeating("UpdateTarget", 0, 1f);
        _spriteRendererTower.sprite = _spritesTower[0];
        results = new RaycastHit2D[10];
        contactFilter = new ContactFilter2D();
        contactFilter.useTriggers = true;
        contactFilter.SetLayerMask(LayerMask.GetMask("Enemy"));
        _gameManager = FindObjectOfType<GameManagerInGame>();
    }

    public override void UpdateGame()
    {
        if (_targetEnemy == null)
        {
            if(_coldEfect.isPlaying)_coldEfect.Stop();
            //_coldEfect.gameObject.SetActive(false);
            return;
        }
        _coldEfect.gameObject.SetActive(true);
        if(!_coldEfect.isPlaying)_coldEfect.Play();
        if (!AudioCold.isPlaying && !_gameManager.IsPouse) AudioCold.Play();
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
                Enemy enemy = result.collider.gameObject.GetComponent<Enemy>();
                enemy.Freeze();
                break;
            }
        }
    }
    public bool CheckEnemyes()
    {
        Collider2D[] enemyes = Physics2D.OverlapCircleAll(transform.position, _firingRadius, enemyLayer);
        if (enemyes.Length == 0)
        {
            return false;
        }
        return true;
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

    public override void Improve()
    {
        _spriteRendererTower.sprite = _spritesTower[1];
    }

    public override Vector2 GetDirectionToShoot()
    {
        Vector3 worldposition = transform.TransformPoint(_partToRotate.position);
        Vector3 worldPositionPointToShoot = transform.TransformPoint(_shootPoint.position);
        return worldPositionPointToShoot - worldposition;
    }
}
