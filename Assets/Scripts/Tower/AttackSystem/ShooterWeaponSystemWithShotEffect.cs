using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Tower.AttackSystem
{
    public class ShooterWeaponSystemWithShotEffect : ShooterWeaponSystem
    {
        [SerializeField] protected GameManager _shotEfect;
        protected override void Shoot()
        {
            base.Shoot();
            _shotEfect.gameObject.SetActive(true);
        }
    }
}