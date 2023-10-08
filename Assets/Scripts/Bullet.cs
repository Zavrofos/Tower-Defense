using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D _rigidbody;
    [SerializeField] protected float _speed;
    [SerializeField] public int Damage;
    public Vector2 Direction;
    public AbsTower Tower;
    public Vector3 StartPosition;
    public float distanceBullet;
    public float DamageRadius;
    public bool IsExplosive;

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

    protected virtual void Destroy()
    {

        Vector2 vectorBetweenObjects = StartPosition - transform.position;
        float distance = vectorBetweenObjects.magnitude;
        if(distance >= distanceBullet)
        {
            Hit();
        }
    }

    protected virtual void Hit()
    {
        if(IsExplosive)
        {
            ObjectPooler.Instance.SpawnFromPool("Explosion", 
                transform.position, 
                Quaternion.identity, 
                this.gameObject); 
        }

        Destroy(gameObject);
    }
}
