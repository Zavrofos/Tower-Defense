using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class CircleFinderObjects : IFinderObjects
    {
        private float _radius;

        public CircleFinderObjects(float radius)
        {
            _radius = radius;
        }

        public IEnumerable<GameObject> Find(string layerMask, Vector3 position)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(position, _radius);
            
            foreach(var collider in colliders)
            {
                if(collider.gameObject.layer == LayerMask.NameToLayer(layerMask))
                {
                    yield return collider.gameObject;
                }
            }
        }
    }
}