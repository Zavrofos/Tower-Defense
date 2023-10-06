using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IFrozen
{
    public float Speed;
    public int Damage;
    private bool _isFrozen;
    private void Update()
    {
        transform.Rotate(Vector3.forward * -Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<AbsTower>(out AbsTower tower))
        {
            tower.ApplayDamage(Damage);
        }
    }

    private IEnumerator FreezeCoroutine()
    {
        Speed = 100;
        _isFrozen = true;
        yield return new WaitForSeconds(5);
        Speed = 200;
        _isFrozen = false;
    }

    public void Freeze()
    {
        if (!_isFrozen)
        {
            StartCoroutine(FreezeCoroutine());
        }
    }
}
