using Assets.Scripts;
using Assets.Scripts.RepPoolObject;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class TowerOfCold : AbsTower
{
    public Transform _shootPoint;
    public float _delayTimeToShoot;
    public SpriteRenderer _spriteRendererTower;
    public Sprite[] _spritesTower;
    public float _timeToShoot;
    public ParticleSystem _coldEfect;
    private GameManagerInGame _gameManager;
    private IPlayableParticle _coldEffectSystem;
    private IFinderObjects _finderObjectsSystem;

    private SoundBox _soundShoot;

    public override void StartGame()
    {
        _coldEffectSystem = new ColdParticle(_coldEfect);
        _finderObjectsSystem = new RaycastFinderObjects(_shootPoint, _firingRadius);
        _spriteRendererTower.sprite = _spritesTower[0];
        _gameManager = FindObjectOfType<GameManagerInGame>();
    }

    public override void UpdateGame()
    {
        Transform targetEnemy = GetNearestEnemy(FinderNearestEnemies.Find("Enemy", transform.position));

        if (targetEnemy == null)
        {
            _coldEffectSystem.Stop();
            if(_soundShoot != null)
            {
                ObjectPooler.Instance.ReturnToPool(_soundShoot);
                _soundShoot = null;
            }
            return;
        }

        _coldEffectSystem.Play();

        if(_soundShoot == null)
        {
            _soundShoot = (SoundBox)ObjectPooler.Instance.SpawnFromPool("SoundBox",
            transform.position,
            transform.rotation);
            _soundShoot.PlaySound(SoundType.Cold);
        }
        
        RotationSystem.Rotate(targetEnemy);

        foreach(var target in _finderObjectsSystem.Find("Enemy",transform.position))
        {
            if (target.TryGetComponent(out IFrozen frozenObj))
            {
                frozenObj.Freeze();
            }
        }
    }
    
    public override void Improve()
    {
        _spriteRendererTower.sprite = _spritesTower[1];
    }

    private void OnDisable()
    {
        if(_soundShoot != null)
        {
            ObjectPooler.Instance.ReturnToPool(_soundShoot);
        }
    }
}
