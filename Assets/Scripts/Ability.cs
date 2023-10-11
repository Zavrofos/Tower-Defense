using Assets.Scripts;
using System.Collections;
using UnityEngine;


public class Ability : MonoBehaviour
{
    [SerializeField] public int Damage;
    public float DamageRadius;
    [HideInInspector] public ButtonAbility ButtonAbility;
    public bool IsExplosive;
    public AbilityType AbilityType;

    protected void Destroy()
    {
        if(IsExplosive)
        {
            ObjectPooler.Instance.SpawnFromPool("Explosion" + AbilityType, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
