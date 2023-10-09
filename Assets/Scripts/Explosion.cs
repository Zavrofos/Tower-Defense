using Assets.Scripts.ObjectPooler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour, IPooledObject
{
    [SerializeField] private ParticleSystem _explosionParticle;
    [SerializeField] private AudioSource _audioExplosion;

    private void OnEnable()
    {
        Invoke(nameof(TurnOff), 0.5f);
    }

    public void OnObjectSpawn(GameObject sender)
    {
        if(sender.TryGetComponent(out Bullet bullet))
        {
            ExplosonPlay(bullet.Damage, bullet.DamageRadius);
        }

        if(sender.TryGetComponent(out Ability ability))
        {
            ExplosonPlay(ability.Damage, ability.DamageRadius);
        }
    }

    private void ExplosonPlay(int damage, float damageRadius)
    {
        ParticleSystem.ShapeModule shapeModule = _explosionParticle.shape;
        shapeModule.radius = damageRadius / 2;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, damageRadius);
        _explosionParticle.Play();
        foreach (var collider in colliders)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                Enemy enemyObj = collider.gameObject.GetComponent<Enemy>();
                enemyObj.ApplayDamage(damage);
            }
        }
        _audioExplosion.Play();
    }

    private void TurnOff()
    {
        gameObject.SetActive(false);
    }
}
