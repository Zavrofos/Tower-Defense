using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;
    public event Action<float, float> Wounded;
    public event Action Killed;

    public void ApplayDamage(int damage)
    {
        _health -= damage;
        if(_health <= 0)
        {
            Killed?.Invoke();
        }
        Wounded?.Invoke(_health, _maxHealth);
    }
}
