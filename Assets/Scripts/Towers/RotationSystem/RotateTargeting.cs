using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Tower.RotationSystem
{
    public class RotateTargeting : IRotateable
    {
        private AbsTower _absTower;

        public RotateTargeting(AbsTower absTower)
        {
            _absTower = absTower;
        }
            
        public void Rotate(Transform target = null)
        {
            Vector2 direction = target.position - _absTower.PartToRotate.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            _absTower.PartToRotate.rotation = Quaternion.Lerp(_absTower.PartToRotate.rotation, rotation,
                Time.deltaTime * _absTower.CurrentSpeedRotation);
        }
    }
}