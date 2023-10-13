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
    [SerializeField] private int _damage;
    [SerializeField] private float _damageRadius;

    private IPlayableParticle _playParticleSystem;
    private IGivingEffects _givingEffectsSystem;
    private IFinderObjects _finderObjectsSystem;

    private void Awake()
    {
        _playParticleSystem = new ExplosionParticle(_explosionParticle, _damageRadius);
        _givingEffectsSystem = new DamageEffect(_damage);
        _finderObjectsSystem = new CircleFinderObjects(_damageRadius);
    }

    public void ExplosonPlay()
    {
        _playParticleSystem.Play();

        foreach(var target in _finderObjectsSystem.Find("Enemy", transform.position))
        {
            _givingEffectsSystem.SetEffect(target);
        }

        PlaySound(SoundType.Explosion);
        StartCoroutine(TurnOff(0.5f));
    }

    private void PlaySound(SoundType type)
    {
        SoundBox audio = (SoundBox)ObjectPooler.Instance.SpawnFromPool("SoundBox",
            transform.position,
            transform.rotation);
        audio.PlaySound(type);
    }

    private IEnumerator TurnOff(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ObjectPooler.Instance.ReturnToPool(this);
    }
}
