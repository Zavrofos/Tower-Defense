using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Tower.RotationSystem
{
    public class Rotator : IRotateable
    {
        private AbsTower _absTower;

        public Rotator(AbsTower absTower)
        {
            _absTower = absTower;
        }

        public void Rotate(Transform target = null)
        {
            _absTower.PartToRotate.Rotate(Vector3.forward * _absTower.CurrentSpeedRotation * Time.deltaTime);
        }
    }
}