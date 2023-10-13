using Assets.Scripts;
using Assets.Scripts.RepPoolObject;
using Assets.Scripts.Tower.FinderEnemyes;
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
    private RaycastHit2D[] results;
    [SerializeField] private ContactFilter2D contactFilter;
    private GameManagerInGame _gameManager;
    private IPlayableParticle _coldEffectSystem;

    private SoundBox _soundShoot;

    public override void StartGame()
    {
        _coldEffectSystem = new ColdParticle(_coldEfect);
        FinderEnemyesSystem = new FinderEnemyes(this.gameObject);
        _spriteRendererTower.sprite = _spritesTower[0];
        results = new RaycastHit2D[10];
        _gameManager = FindObjectOfType<GameManagerInGame>();
    }

    public override void UpdateGame()
    {
        if (FinderEnemyesSystem.TargetEnemy == null)
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
        
        RotationSystem.Rotate(FinderEnemyesSystem.TargetEnemy);

        Physics2D.Raycast(transform.position, (_shootPoint.position - transform.position), contactFilter, results, _firingRadius);

        foreach (var result in results)
        {
            if (result)
            {
                if(result.collider.gameObject.TryGetComponent(out IFrozen frozenObj))
                {
                    frozenObj.Freeze();
                }
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
