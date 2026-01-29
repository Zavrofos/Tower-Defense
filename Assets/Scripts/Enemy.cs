using Assets.Scripts;
using System.Collections;
using Assets.Scripts.Enemyes.AttackBehaviours;
using Assets.Scripts.Enemyes.MoveBehaviours;
using UnityEngine;

public class Enemy : MonoBehaviour, IFrozen, IApplayDamage
{
    public int _health;
    public int _damage;
    public int _reward;
    public SpriteRenderer _spriteRenderer;
    public Animator Animator;
    public Color _applayDamageColor;
    public Color _freezeColor;
    public Color _initialColor;
    public Color _currentColor;
    public bool _isFrozen;
    public EnemyType Type;

    public IMoveBehaviour MoveBehaviour;
    public IAttackBehaviour AttackBehaviour;

    private void Awake()
    {
        MoveBehaviour = GetComponent<IMoveBehaviour>();
        AttackBehaviour = GetComponent<IAttackBehaviour>();
    }

    private void Start()
    {
        _initialColor = _spriteRenderer.color;
        _currentColor = _initialColor;
    }

    private void Update()
    {
        MoveBehaviour?.Move();
    }

    public virtual void ApplayDamage(int damage)
    {
        _health -= damage;
        StartCoroutine(ChangeColorForHit());
        if (_health <= 0)
        {
            GameManagerInGame gameManager = GameManager.Instance.CurrentGameManagerLevel;
            gameManager.AddCoins(_reward);
            Spawner spawner = GameManager.Instance.CurrentSpawner;
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
        MoveBehaviour.Speed = 0.5f;
        _currentColor = _freezeColor;
        _spriteRenderer.color = _currentColor;
        _isFrozen = true;
        yield return new WaitForSeconds(5);
        MoveBehaviour.Speed = 1;
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

    public void Attack(IDamageSystem target)
    {
        AttackBehaviour?.Attack(target);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Home>(out Home home))
        {
            home.ApplayDamage(_damage);
        }
    }
}
