using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Tower.RotationSystem
{
    public class RotateTargeting : IRotateable
    {
        private Transform _rotateObject;
        private float _speedRotation;

        public RotateTargeting(Transform rotateObject, float speedRotation)
        {
            _rotateObject = rotateObject;
            _speedRotation = speedRotation;
        }
            
        public void Rotate(Transform target = null)
        {
            Vector2 direction = target.position - _rotateObject.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            _rotateObject.rotation = Quaternion.Lerp(_rotateObject.rotation, rotation,
                Time.deltaTime * _speedRotation);
        }
    }
}