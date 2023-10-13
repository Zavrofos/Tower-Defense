using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public class RaycastFinderObjects : IFinderObjects
    {
        private Transform _targetDirection;
        private float _distance;
        
        public RaycastFinderObjects(Transform targetDirection,float distance)
        {
            _targetDirection = targetDirection;
            _distance = distance;
        }

        public IEnumerable<GameObject> Find(string layerMask, Vector3 position)
        {
            RaycastHit2D hit = Physics2D.Raycast(position,
                (_targetDirection.position - position) * _distance,
                 _distance, 
                LayerMask.GetMask(layerMask));

            Debug.DrawRay(position, (_targetDirection.position - position) * _distance, Color.red);

            if(hit)
            {
                yield return hit.collider.gameObject;
            }

        }
    }
}