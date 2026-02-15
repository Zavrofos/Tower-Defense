using Assets.Scripts;
using Assets.Scripts.RepPoolObject;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;


public class Explosion : PooledObject
{
    [SerializeField] private PolledObjectType _type;
    public override PolledObjectType Type => _type;

    [SerializeField] private ParticleSystem _explosionParticle;
    public int Damage;
    public float DamageRadius;

    private IPlayableParticle _playParticleSystem;
    private IGivingEffects _givingEffectsSystem;
    private IFinderObjects _finderObjectsSystem;

    private void Awake()
    {
        _playParticleSystem = new ExplosionParticle(_explosionParticle, DamageRadius);
        _givingEffectsSystem = new DamageEffect(Damage);
        _finderObjectsSystem = new CircleFinderObjects(DamageRadius);
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
        // SoundBox audio = (SoundBox)ObjectPooler.Instance.SpawnFromPool("SoundBox",
        //     transform.position,
        //     transform.rotation);
        // audio.PlaySound(type);
    }

    private IEnumerator TurnOff(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        GameManager.Instance.ObjectPooler.ReturnToPool(this);
    }
}
