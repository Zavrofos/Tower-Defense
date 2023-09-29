using Assets.Scripts.Tower.FinderEnemyes;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Tower.RotationSystem
{
    public abstract class AbsRotationSystem : MonoBehaviour, IRotation
    {
        [SerializeField] protected Transform _partToRotate;
        [SerializeField] protected float _speedRotation;
        public Transform PartToRotate => _partToRotate;

        public abstract void Rotate(Transform target = null);
    }
}