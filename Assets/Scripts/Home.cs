using System;
using Assets.Scripts;
using Assets.Scripts.RepPoolObject;
using UnityEngine;

public class Home : MonoBehaviour
{
    private float _maxHealth  = 100;
    private float _health = 100;
    public event Action Killed;

    public void ApplayDamage(int damage)
    {
        _health -= damage;
        
        SoundBox  soundBox= (SoundBox)GameManager.Instance.ObjectPooler.SpawnFromPool(PolledObjectType.SoundBox, Vector3.zero, Quaternion.identity);
        soundBox.Play(GameManager.Instance.GameAssets.LifeDamageAudio, true);
        
        if(_health <= 0)
            Killed?.Invoke();
        
        GameManager.Instance.GameOverlay.HealthBar.OnValueChanged(_health, _maxHealth);
    }

    public void AddHealth(int count)
    {
        _health += count;
        _health = _health > _maxHealth ? _maxHealth : _health;
        GameManager.Instance.GameOverlay.HealthBar.OnValueChanged(_health, _maxHealth);
    }
}
