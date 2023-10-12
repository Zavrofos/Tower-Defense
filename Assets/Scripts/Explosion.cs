using Assets.Scripts;
using Assets.Scripts.RepPoolObject;
using Assets.Scripts.Tower;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : PooledObject
{
    [SerializeField] private string _tag;
    public override string Tag => _tag;

    [SerializeField] private ParticleSystem _explosionParticle;
    [SerializeField] private AudioSource _audioExplosion;
    [SerializeField] private int _damage;
    [SerializeField] private float _damageRadius;

    private IPlayableParticle _playParticleSystem;
    private IGivingEffects _givingEffectsSystem;
    private IFinderObjects _finderObjectsSystem;

    private void Awake()
    {
        _playParticleSystem = new ExplosionParticle(_explosionParticle, _damageRadius);
        _givingEffectsSystem = new DamageEffect(_damage);
        _finderObjectsSystem = new CircleFinderObjects("Enemy", _damageRadius);
    }

    public void ExplosonPlay()
    {
        _playParticleSystem.Play();

        foreach(var target in _finderObjectsSystem.Find(transform.position))
        {
            _givingEffectsSystem.SetEffect(target);
        }

        _audioExplosion.Play();
        StartCoroutine(TurnOff(_audioExplosion.clip.length));
    }

    private IEnumerator TurnOff(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ObjectPooler.Instance.ReturnToPool(this);
    }
}
