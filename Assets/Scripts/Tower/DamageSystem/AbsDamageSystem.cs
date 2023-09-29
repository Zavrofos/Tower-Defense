using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsDamageSystem : MonoBehaviour, IDamageSystem
{
    [SerializeField] protected int _health;
    [SerializeField] protected SpriteRenderer SpriteRender;
    [SerializeField] protected Color ApplayDamageColor;
    protected Color CurrentColor;

    private void Start()
    {
        CurrentColor = SpriteRender.color;
    }

    public void ApplayDamage(int damage)
    {
        _health -= damage;
        StartCoroutine(ChangeColorForHit());
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected abstract IEnumerator ChangeColorForHit();
}

    

