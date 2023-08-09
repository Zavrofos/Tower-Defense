using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHight : Bullet
{
    [SerializeField] private Explosion _explosion;
    [SerializeField] private float _damageRadius;

    private void Start()
    {
        RotateBullet();
    }

    public void RotateBullet()
    {
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;
    }

    protected override void Hit()
    {
        ExplosonPlay();
    }

    protected override void Destroy()
    {
        Vector2 vectorBetweenObjects = StartPosition - transform.position;
        float distance = vectorBetweenObjects.magnitude;
        if (distance >= distanceBullet)
        {
            ExplosonPlay();
        }
    }
    private void ExplosonPlay()
    {
        Explosion explosion = Instantiate(_explosion, transform.position, Quaternion.identity);
        ParticleSystem.ShapeModule shapeModule = explosion.ExplosionParticle.shape;
        shapeModule.radius = _damageRadius / 2;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _damageRadius);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                Enemy enemyObj = collider.gameObject.GetComponent<Enemy>();
                enemyObj.ApplayDamage(_damage);
            }
        }
        Tower.gameObject.GetComponent<TowerHight>().AudioExplosion.Play();
        Destroy(gameObject);
    }
}
