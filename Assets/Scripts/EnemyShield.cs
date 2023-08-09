using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShield : Enemy
{
    [SerializeField] private Shield Shield;
    private bool _shieldBroken;
    public float SpeedAfterShilBroken;
    public override void ApplayDamage(int damage)
    {
        if(!_shieldBroken)
        {
            Shield.Health -= damage;
            Shield.ChangeColorForHitStart();
            if(Shield.Health <= 0)
            {
                _shieldBroken = true;
                Shield.Destroy(this);
            }
        }
        else
        {
            _health -= damage;
            StartCoroutine(ChangeColorForHit());
            if (_health <= 0)
            {
                GameManagerInGame gameManager = FindObjectOfType<GameManagerInGame>();
                gameManager.AddCoins(_reward);
                FindObjectOfType<Spawner>().CurrentCountOfEnemyesKilled++;
                Destroy(gameObject);
            }
        }
    }
}
