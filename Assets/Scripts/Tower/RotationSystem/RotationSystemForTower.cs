using Assets.Scripts.Tower.FinderEnemyes;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Tower.RotationSystem
{
    public class RotationSystemForTower : AbsRotationSystem
    {
        public override void Rotate(Transform target = null)
        {
            Vector2 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            _partToRotate.rotation = Quaternion.Lerp(_partToRotate.rotation, rotation, Time.deltaTime * _speedRotation);
        }
    }
}