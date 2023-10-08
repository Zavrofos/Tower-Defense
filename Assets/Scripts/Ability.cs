using Assets.Scripts;
using System.Collections;
using UnityEngine;


public class Ability : MonoBehaviour
{
    [SerializeField] protected int _damage;
    public float DamageRadius;
    [HideInInspector] public ButtonAbility ButtonAbility;

    protected void Destroy()
    {
        GetComponent<Explosion>().ExplosonPlay(_damage, ButtonAbility.AudioExplosion);
    }
}
