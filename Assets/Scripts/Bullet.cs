using System;
using Assets.Scripts;
using Assets.Scripts.RepPoolObject;
using UniRx;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;

    public int Damage;
    public Vector2 Direction;
    public Vector3 StartPosition;
    public float distanceBullet;
    public bool IsExplosive;
    public bool IsImproved;

    public void Init(Vector2 direction, float distance, Vector3 startPos)
    {
        Direction = direction.normalized;
        StartPosition = startPos;
        distanceBullet = distance;

        _rigidbody.linearVelocity = Direction * _speed;
    }
    
    private void Update()
    {
        float distance = Vector2.Distance(StartPosition, transform.position);

        if (distance >= distanceBullet)
            Hit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IApplayDamage damagedObj))
        {
            damagedObj.ApplayDamage(Damage);
            Hit();
        }
    }

    private void Hit()
    {
        if (IsExplosive)
            BlowUp();

        Destroy(gameObject);
    }

    private void BlowUp()
    {
        PolledObjectType type = IsImproved 
            ? PolledObjectType.ExplosionBulletHighPlus 
            : PolledObjectType.ExplosionBulletHigh;

        PooledObject pooledObj = GameManager.Instance.ObjectPooler.SpawnFromPool(type, transform.position, Quaternion.identity);
        Explosion explosion = (Explosion)pooledObj;
        explosion.ExplosonPlay();
    }
}
