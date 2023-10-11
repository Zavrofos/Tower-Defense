using Assets.Scripts.ObjectPooler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour, IPooledObject
{
    [SerializeField] private ParticleSystem _explosionParticle;
    [SerializeField] private AudioSource _audioExplosion;
    [SerializeField] private int _damage;
    [SerializeField] private float _damageRadius;

    private void OnEnable()
    {
        Invoke(nameof(TurnOff), 0.5f);
    }

    public void OnObjectSpawn()
    {
        ExplosonPlay();
    }

    private void ExplosonPlay()
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
                enemyObj.ApplayDamage(_damage);
            }
        }
        _audioExplosion.Play();
    }

    private void TurnOff()
    {
        gameObject.SetActive(false);
    }
}
