using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Tower.AttackSystem
{
    public abstract class AbsWeaponSystem : MonoBehaviour, IWeaponSystem
    {
        [SerializeField] protected AudioSource _audioAttack;
        public abstract void Attack();
    }
}