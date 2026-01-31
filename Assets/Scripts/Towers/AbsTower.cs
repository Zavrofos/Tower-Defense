using System;
using Assets.Scripts;
using Assets.Scripts.Tower;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enemyes.EnemyWithForceField;
using Assets.Scripts.GlobalShop;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbsTower : MonoBehaviour
{
    public IDamageSystem DamageSystem;
    public IRotateable RotationSystem;
    public IDeceleration DecelerationSystem;
    protected IFinderObjects FinderNearestEnemies;

    public Vector3 InitRotationImageInShop;
    public Sprite _icon;
    public string _label;
    public int _price;
    public int _upgradePrice;
    public float _firingRadius;
    public string _description;
    public Transform PartToRotate;
    public float SpeedRotation;
    public GlobalShopItemType Type;
    public float CurrentSpeedRotation { get; set; }

    public Sprite Icon => _icon;
    public string Label => _label;
    public int Price => _price;
    public int UpgradePrice => _upgradePrice;
    public float FiringRadius => _firingRadius;
    public string Description => _description;

    public EnemyType[] TargetsEnemyType;

    public bool IsDecelerated { get; set; }
    public Color InitialColor;
    public Color DecelerateColor;
    private IDisposable _findNearestEnemiesWithForceFields;

    private void Start()
    {
        CurrentSpeedRotation = SpeedRotation;
        DamageSystem = GetComponent<IDamageSystem>();
        FinderNearestEnemies = new CircleFinderObjects(FiringRadius);
        StartCheckNearestEnemiesWithForceField();
        StartGame();
    }

    private void Update()
    {
        UpdateGame();
    }

    public abstract void StartGame();
    public abstract void UpdateGame();
    public abstract void Improve();
    public virtual void Shoot() { }
    public virtual Vector2 GetDirectionToShoot() { return Vector2.zero; }


    public void ApplayDamage(int damage)
    {
        DamageSystem.ApplayDamage(damage);
    }

    protected Transform GetNearestEnemy(IEnumerable<GameObject> enemies)
    {
        float shortestDistance = Mathf.Infinity;
        Transform nearestEnemy = null;

        foreach(var type in TargetsEnemyType)
        {
            foreach (var enemy in enemies)
            {
                if(enemy.TryGetComponent(out Enemy en) && en.Type == type)
                {
                    float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);

                    if (distanceToEnemy < shortestDistance)
                    {
                        shortestDistance = distanceToEnemy;
                        nearestEnemy = enemy.transform;
                    }
                }
            }
        }

        return nearestEnemy;
    }

    private void StartCheckNearestEnemiesWithForceField()
    {
        _findNearestEnemiesWithForceFields = Observable
            .Interval(TimeSpan.FromSeconds(0.2f))
            .Subscribe((_) =>
            {
                bool enableDecelerate = FinderNearestEnemies
                    .Find("Enemy", transform.position)
                    .Any(enemy => enemy.TryGetComponent(out ForceField _));

                if (enableDecelerate && !IsDecelerated)
                    SetDeceleration(true);
                
                if(!enableDecelerate && IsDecelerated)
                    SetDeceleration(false);
            })
            .AddTo(this);
    }

    private void SetDeceleration(bool value)
    {
        IsDecelerated = value;
        DecelerationSystem?.SetDeceleration(value);
    }
}
