using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Tower.AttackSystem
{
    public class ShooterWeaponSystem : AbsWeaponSystem
    {
        private Bullet CurrentBullet;
        [SerializeField] private List<Bullet> _bullets;
        [SerializeField] private Transform _shootPoint;

        public override void Attack()
        {
            Shoot();
        }

        protected virtual void Shoot()
        {
            Bullet bullet = Instantiate(CurrentBullet, _shootPoint.position, Quaternion.identity);
            bullet.Direction = GetDirectionToShoot();
            bullet.StartPosition = transform.position;
            bullet.distanceBullet = GetComponent<AbsTower>().FiringRadius;
            _audioAttack.Play();
        }

        private Vector2 GetDirectionToShoot()
        {
            Vector3 worldposition = transform.position;
            Vector3 worldPositionPointToShoot = transform.TransformPoint(_shootPoint.position);
            return worldPositionPointToShoot - worldposition;
        }
    }
}