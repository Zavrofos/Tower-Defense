using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D _rigidbody;
    [SerializeField] protected float _speed;
    [SerializeField] protected int _damage;
    public Vector2 Direction;
    public AbsTower Tower;
    public Vector3 StartPosition;
    public float distanceBullet;

    private void FixedUpdate()
    {
        
        _rigidbody.AddForce(Direction * _speed);
        Destroy();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Shield shield))
        {
            shield.ApplayDamage(_damage);
            Hit();
            return;
        }

        if(collision.TryGetComponent(out Enemy enemy))
        {
            GiveDamage(enemy);
            Hit();
        }
    }

    protected virtual void Destroy()
    {

        Vector2 vectorBetweenObjects = StartPosition - transform.position;
        float distance = vectorBetweenObjects.magnitude;
        if(distance >= distanceBullet)
        {
            Destroy(gameObject);
        }
    }


    protected virtual void Hit()
    {
        Destroy(gameObject);
    }

    protected void GiveDamage(Enemy enemy)
    {
        enemy.ApplayDamage(_damage);
    }
}
