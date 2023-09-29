using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class TowerOfCold : AbsTower
{
    public Transform _shootPoint;
    public float _delayTimeToShoot;
    public SpriteRenderer _spriteRendererTower;
    public Sprite[] _spritesTower;
    public Vector2 DirectionToShoot;
    public float _timeToShoot;
    public ParticleSystem _coldEfect;
    public LayerMask enemyLayer;

    private RaycastHit2D[] results;
    private ContactFilter2D contactFilter;
    [SerializeField] private AudioSource AudioCold;
    private GameManagerInGame _gameManager;



    public override void StartGame()
    {
        _spriteRendererTower.sprite = _spritesTower[0];
        results = new RaycastHit2D[10];
        contactFilter = new ContactFilter2D();
        contactFilter.useTriggers = true;
        contactFilter.SetLayerMask(LayerMask.GetMask("Enemy"));
        _gameManager = FindObjectOfType<GameManagerInGame>();
    }

    public override void UpdateGame()
    {
        if (FinderEnemyesSystem.TargetEnemy == null)
        {
            if (_coldEfect.isPlaying) _coldEfect.Stop();
            return;
        }
        _coldEfect.gameObject.SetActive(true);
        if (!_coldEfect.isPlaying) _coldEfect.Play();
        if (!AudioCold.isPlaying && !_gameManager.IsPouse) AudioCold.Play();
        Vector2 direction = FinderEnemyesSystem.TargetEnemy.position - transform.position;
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
