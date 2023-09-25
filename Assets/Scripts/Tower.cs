using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    public float _firingRadius;
    public Transform _partToRotate;
    public float _speedRotation;
    public int _upgradePrice;

    public Sprite _icon;
    public string _label;
    public int _price;
    public string _description;

    public int Health; 
    public SpriteRenderer SpriteRender;
    public Color ApplayDamageColor;
    public Color CurrentColor;

    public float FiringRadius => _firingRadius; 
    public Transform PositionTower => _partToRotate; 
    public Sprite Icon => _icon; 
    public string Label => _label; 
    public int Price => _price; 
    public int UpgradePrice => _upgradePrice;
    public string Description => _description;

    public virtual void StartGame() { }
    public virtual void UpdateGame() { }
    public virtual void Shoot() { }
    public virtual void Improve() { }
    public virtual void UpdateTarget() { }
    public virtual Vector2 GetDirectionToShoot() { return Vector2.zero; }

    private void Start()
    {
        CurrentColor = SpriteRender.color;
        StartGame();
    }
    
    private void Update()
    {
        UpdateGame();
    }

    public void ApplayDamage(int damage)
    {
        Health -= damage;
        StartCoroutine(ChangeColorForHit());
        if(Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public virtual IEnumerator ChangeColorForHit()
    {
        SpriteRender.color = ApplayDamageColor;
        yield return new WaitForSeconds(0.2f);
        SpriteRender.color = CurrentColor;
    }
}
