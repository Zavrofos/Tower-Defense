using Assets.Scripts;
using Assets.Scripts.ObjectPooler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] public int Damage;
    public Vector2 Direction;
    public Vector3 StartPosition;
    public float distanceBullet;
    public float DamageRadius;
    public bool IsExplosive;
    public BulletType BulletType;

    private void FixedUpdate()
    {
        _rigidbody.AddForce(Direction * _speed);
        Destroy();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IApplayDamage damagedObj))
        {
            damagedObj.ApplayDamage(Damage);
            Hit();
        }
    }

    private void Destroy()
    {
        Vector2 vectorBetweenObjects = StartPosition - transform.position;
        float distance = vectorBetweenObjects.magnitude;
        if(distance >= distanceBullet)
        {
            Hit();
        }
    }

    private void Hit()
    {
        if(IsExplosive)
        {
            ObjectPooler.Instance.SpawnFromPool("Explosion" + BulletType, 
                transform.position, 
                Quaternion.identity); 
        }

        Destroy(gameObject);
    }
}
