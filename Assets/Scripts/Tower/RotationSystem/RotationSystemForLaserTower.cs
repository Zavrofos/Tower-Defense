using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Tower.RotationSystem
{
    public class RotationSystemForLaserTower : AbsRotationSystem
    {
        public override void Rotate(Transform target = null)
        {
            _partToRotate.Rotate(Vector3.forward * _speedRotation * Time.deltaTime);
        }
    }
}