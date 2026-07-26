using System;
using Assets.Scripts;
using Assets.Scripts.RepPoolObject;
using UnityEngine;

public class Home : MonoBehaviour
{
    private float _maxHealth  = 100;
    public float Health { get; private set; } = 100;
    public event Action Killed;

    public void ApplayDamage(int damage)
    {
        Health -= damage;

        SoundBox  soundBox= (SoundBox)GameManager.Instance.ObjectPooler.SpawnFromPool(PolledObjectType.SoundBox, Vector3.zero, Quaternion.identity);
        soundBox.Play(GameManager.Instance.GameAssets.LifeDamageAudio, false);

        if(Health <= 0)
            Killed?.Invoke();

        GameManager.Instance.GameOverlay.HealthBar.OnValueChanged(Health, _maxHealth);
    }

    public void AddHealth(int count)
    {
        Health += count;
        Health = Health > _maxHealth ? _maxHealth : Health;
        GameManager.Instance.GameOverlay.HealthBar.OnValueChanged(Health, _maxHealth);
    }
}
