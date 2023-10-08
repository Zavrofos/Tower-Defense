using Assets.Scripts;
using System.Collections;
using UnityEngine;


public class Ability : MonoBehaviour
{
    [SerializeField] public int Damage;
    public float DamageRadius;
    [HideInInspector] public ButtonAbility ButtonAbility;
    public bool IsExplosive;

    protected void Destroy()
    {
        if(IsExplosive)
        {
            ObjectPooler.Instance.SpawnFromPool("Explosion", transform.position, Quaternion.identity, this.gameObject);
        }

        Destroy(gameObject);
    }
}
