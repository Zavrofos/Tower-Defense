using System;
using Assets.Scripts;
using Assets.Scripts.RepPoolObject;
using System.Collections;
using UnityEngine;

public class Explosion : PooledObject
{
    [SerializeField] private PolledObjectType _type;
    public override PolledObjectType Type => _type;

    [SerializeField] private ParticleSystem _explosionParticle;
    public int Damage;
    public float DamageRadius;
    [SerializeField] private AudioClip _explosionAudio;

    private IPlayableParticle _playParticleSystem;
    private IGivingEffects _givingEffectsSystem;
    private IFinderObjects _finderObjectsSystem;
    
    private Coroutine _turnOffCoroutine;

    private void Awake()
    {
        _playParticleSystem = new ExplosionParticle(_explosionParticle, DamageRadius);
        _givingEffectsSystem = new DamageEffect(Damage);
        _finderObjectsSystem = new CircleFinderObjects(DamageRadius);
    }

    public void ExplosonPlay()
    {
        SoundBox soundBox = (SoundBox)GameManager.Instance.ObjectPooler.SpawnFromPool(PolledObjectType.SoundBox, Vector3.zero, Quaternion.identity);
        soundBox.Play(_explosionAudio, false);
        
        _playParticleSystem.Play();

        foreach(var target in _finderObjectsSystem.Find("Enemy", transform.position))
        {
            if (target.transform.childCount > 0 && target.transform.GetChild(0).TryGetComponent(out Shield shield))
                shield.ApplayDamage(_givingEffectsSystem.Damage);
            else
                _givingEffectsSystem.SetEffect(target);
        }
        
        _turnOffCoroutine = StartCoroutine(TurnOff(0.5f));
    }

    private IEnumerator TurnOff(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        GameManager.Instance.ObjectPooler.ReturnToPool(this);
    }

    private void OnDestroy()
    {
        if (_turnOffCoroutine != null)
        {
            StopCoroutine(_turnOffCoroutine);
            _turnOffCoroutine = null;
        }
    }
}
