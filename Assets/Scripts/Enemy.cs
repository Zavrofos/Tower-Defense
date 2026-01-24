using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IFrozen, IApplayDamage
{
    public int _health;
    public int _damage;
    public int _reward;
    public float _speed;
    public GameObject[] PointsOfWayForEnemy;
    public SpriteRenderer _spriteRenderer;
    public Animator Animator;
    public Color _applayDamageColor;
    public Color _freezeColor;
    public Color _initialColor;
    public Color _currentColor;
    public Transform[] _pointsOfWay;
    public int _currentPointOfWay;
    public bool _isFrozen;
    public EnemyType Type;

    private void Start()
    {
        _pointsOfWay = new Transform[PointsOfWayForEnemy[GameManager.Instance.CurrentGameData.CurrentLevel - 1].transform.childCount];
        for(int i = 0; i < _pointsOfWay.Length; i++)
        {
            _pointsOfWay[i] = PointsOfWayForEnemy[GameManager.Instance.CurrentGameData.CurrentLevel - 1].transform.GetChild(i);
        }
        _initialColor = _spriteRenderer.color;
        _currentColor = _initialColor;
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        var point = _pointsOfWay[_currentPointOfWay].position;
        if(transform.position != point)
        {
            transform.position = Vector2.MoveTowards(transform.position, point, _speed * Time.deltaTime);
        }
        else
        {
            if(_currentPointOfWay >= _pointsOfWay.Length - 1)
            {
                FindObjectOfType<Spawner>().CurrentCountOfEnemyesKilled++;
                Destroy(gameObject);
            }
            
            if(_currentPointOfWay < _pointsOfWay.Length - 1)
            {
                Vector2 direction = _pointsOfWay[_currentPointOfWay + 1].position - transform.position;
                Animator.SetFloat("X", direction.x);
                Animator.SetFloat("Y", direction.y);
            }
            _currentPointOfWay++;
        }
    }

    public virtual void ApplayDamage(int damage)
    {
        _health -= damage;
        StartCoroutine(ChangeColorForHit());
        if (_health <= 0)
        {
            GameManagerInGame gameManager = FindObjectOfType<GameManagerInGame>();
            gameManager.AddCoins(_reward);
            Spawner spawner = FindObjectOfType<Spawner>();
            spawner.CurrentCountOfEnemyesKilled++;
            spawner.CurrentCountOfEnemyesKilledInCurrentWave++;
            Destroy(gameObject);
        }
    }

    public IEnumerator ChangeColorForHit()
    {
        _spriteRenderer.color = _applayDamageColor;
        yield return new WaitForSeconds(0.2f);
        _spriteRenderer.color = _currentColor;
    }

    public virtual IEnumerator FreezeCoroutine()
    {
        _speed = 0.5f;
        _currentColor = _freezeColor;
        _spriteRenderer.color = _currentColor;
        _isFrozen = true;
        yield return new WaitForSeconds(5);
        _speed = 1;
        _currentColor = _initialColor;
        _spriteRenderer.color = _currentColor;
        _isFrozen = false;
    }

    public void Freeze()
    {
        if(!_isFrozen)
        {
            StartCoroutine(FreezeCoroutine());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Home>(out Home home))
        {
            home.ApplayDamage(_damage);
        }
    }

    public void ChangeSpeed(float speed)
    {
        _speed = speed;
    }
}
