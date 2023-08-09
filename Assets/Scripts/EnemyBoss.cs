using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{
    public Sword Swords;

    public override IEnumerator FreezeCoroutine()
    {
        _speed = 0.5f;
        Swords.Speed = 100;
        _currentColor = _freezeColor;
        _spriteRenderer.color = _currentColor;
        _isFrozen = true;
        yield return new WaitForSeconds(5);
        _speed = 1;
        Swords.Speed = 200;
        _currentColor = _initialColor;
        _spriteRenderer.color = _currentColor;
        _isFrozen = false;
    }
}
