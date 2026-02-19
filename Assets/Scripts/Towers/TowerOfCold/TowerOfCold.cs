using Assets.Scripts;
using Assets.Scripts.RepPoolObject;
using Assets.Scripts.Tower.RotationSystem;
using Towers.DecelerationSystems;
using UnityEngine;

public class TowerOfCold : AbsTower
{
    public Transform _shootPoint;
    public float _delayTimeToShoot;
    public SpriteRenderer _spriteRendererTower;
    public Sprite[] _spritesTower;
    public float _timeToShoot;
    public ParticleSystem _coldEfect;
    private IPlayableParticle _coldEffectSystem;
    private IFinderObjects _finderObjectsSystem;

    [SerializeField] private AudioClip _shootAudio;
    private bool _isAudioPlaying;
    private SoundBox _soundBox;

    public override void StartGame()
    {
        _coldEffectSystem = new ColdParticle(_coldEfect);
        _finderObjectsSystem = new RaycastFinderObjects(_shootPoint, _firingRadius);
        DecelerationSystem = new DecelerationForTowerOfCold(this);
        RotationSystem = new RotateTargeting(this);
        _spriteRendererTower.sprite = _spritesTower[0];
    }

    public override void UpdateGame()
    {
        Transform targetEnemy = GetNearestEnemy(FinderNearestEnemies.Find("Enemy", transform.position));

        if (targetEnemy == null)
        {
            SwitchAudio(false);
            _coldEffectSystem.Stop();
            return;
        }

        _coldEffectSystem.Play();

        SwitchAudio(true);
        
        RotationSystem.Rotate(targetEnemy);

        foreach(var target in _finderObjectsSystem.Find("Enemy",transform.position))
        {
            if (target.TryGetComponent(out IFrozen frozenObj))
            {
                frozenObj.Freeze();
            }
        }
    }

    private void SwitchAudio(bool value)
    {
        if(_isAudioPlaying == value)
            return;

        if (value)
        {
            _soundBox = (SoundBox)GameManager.Instance.ObjectPooler.SpawnFromPool(PolledObjectType.SoundBox, transform.position, transform.rotation);
            _soundBox.Play(_shootAudio, true);
        }
        else if(_soundBox)
        {
            _soundBox.Stop();
            _soundBox = null;
        }

        _isAudioPlaying = value;
    }
    
    public override void Improve()
    {
        _spriteRendererTower.sprite = _spritesTower[1];
    }
}
