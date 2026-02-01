using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class AbsDamageSystem : MonoBehaviour, IDamageSystem
{
    [SerializeField] protected int _health;
    [SerializeField] protected SpriteRenderer SpriteRender;
    [SerializeField] protected Color ApplayDamageColor;
    [SerializeField] protected Animator DestructorAnimator;
    [SerializeField] protected AbsTower AbsTower;
    protected Color CurrentColor;
    protected bool IsDestroyed;
    public bool IsStartDestroyAnimation { get; set; }

    private void Start()
    {
        CurrentColor = SpriteRender.color;
    }

    public void ApplayDamage(int damage)
    {
        _health -= damage;
        
        if(_health > 0) 
            StartCoroutine(ChangeColorForHit());
        else if(!IsStartDestroyAnimation)
            DestroyTower().Forget();
    }

    protected abstract IEnumerator ChangeColorForHit();

    protected virtual async UniTask DestroyTower()
    {
        IsStartDestroyAnimation = true;
    }

    private void OnDestroy()
    {
        IsDestroyed = true;
    }
}

    

