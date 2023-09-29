using Assets.Scripts.Tower;
using Assets.Scripts.Tower.FinderEnemyes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbsTower : MonoBehaviour
{
    public IFinderEnemyesSystem FinderEnemyesSystem;
    public IDamageSystem DamageSystem;
    public IRotation RotationSystem;

    public Transform _partToRotate;
    public Sprite _icon;
    public string _label;
    public int _price;
    public int _upgradePrice;
    public float _firingRadius;
    public float _speedRotation;
    public string _description;


    public Transform PartToRotate => _partToRotate;
    public Sprite Icon => _icon;
    public string Label => _label;
    public int Price => _price;
    public int UpgradePrice => _upgradePrice;
    public float FiringRadius => _firingRadius;
    public string Description => _description;
    
    private void Start()
    {
        DamageSystem = GetComponent<IDamageSystem>();
        FinderEnemyesSystem = GetComponent<IFinderEnemyesSystem>();
        RotationSystem = GetComponent<IRotation>();
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
}
