using Assets.Scripts;
using Assets.Scripts.RepPoolObject;
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
            BlowUp();
        }

        Destroy(gameObject);
    }

    private void BlowUp()
    {
        PooledObject pooledObj = ObjectPooler.Instance.SpawnFromPool("Explosion" + AbilityType,
                transform.position,
                Quaternion.identity);

        Explosion explosion = (Explosion)pooledObj;
        explosion.ExplosonPlay();
    }
}
