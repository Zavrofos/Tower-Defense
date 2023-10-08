using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionParticle;
    [SerializeField] private float _damageRadius;

    public void ExplosonPlay(int damage, AudioSource audioShoot)
    {
        ParticleSystem.ShapeModule shapeModule = _explosionParticle.shape;
        shapeModule.radius = _damageRadius / 2;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _damageRadius);
        _explosionParticle.Play();
        foreach (var collider in colliders)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                Enemy enemyObj = collider.gameObject.GetComponent<Enemy>();
                enemyObj.ApplayDamage(damage);
            }
        }
        audioShoot.Play();

        if(TryGetComponent<Bullet>(out Bullet bullet))
        {
            bullet.enabled = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<SpriteRenderer>().enabled = false;
        }
        
        

        if(TryGetComponent<Ability>(out Ability ability))
        {
            ability.enabled = false;
            GetComponentInChildren<SpriteRenderer>().enabled = false;
        }

        Destroy(gameObject, 0.5f);
    }
}
