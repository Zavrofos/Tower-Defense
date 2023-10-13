using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Tower.RotationSystem
{
    public class Rotator : IRotateable
    {
        private Transform _rotationObject;
        private float _speedRotation;

        public Rotator(Transform rotationObject, float speedRotation)
        {
            _rotationObject = rotationObject;
            _speedRotation = speedRotation;
        }

        public void Rotate(Transform target = null)
        {
            _rotationObject.Rotate(Vector3.forward * _speedRotation * Time.deltaTime);
        }
    }
}