using Assets.Scripts;
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
    [SerializeField] private AudioSource AudioCold;
    private GameManagerInGame _gameManager;

    public override void StartGame()
    {
        FinderEnemyesSystem = new FinderEnemyes(this.gameObject);
        _spriteRendererTower.sprite = _spritesTower[0];
        results = new RaycastHit2D[10];
        _gameManager = FindObjectOfType<GameManagerInGame>();
    }

    public override void UpdateGame()
    {
        if (FinderEnemyesSystem.TargetEnemy == null)
        {
            if (_coldEfect.isPlaying) _coldEfect.Stop();
            return;
        }
        _coldEfect.gameObject.SetActive(true);
        if (!_coldEfect.isPlaying) _coldEfect.Play();
        if (!AudioCold.isPlaying && !_gameManager.IsPouse) AudioCold.Play();
        
        RotationSystem.Rotate(FinderEnemyesSystem.TargetEnemy);

        Physics2D.Raycast(transform.position, (_shootPoint.position - transform.position), contactFilter, results, _firingRadius);

        foreach (var result in results)
        {
            if (result)
            {
                if(result.collider.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    enemy.Freeze();
                    break;
                }
            }
        }
    }
    
    public override void Improve()
    {
        _spriteRendererTower.sprite = _spritesTower[1];
    }
}
