using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class CircleFinderObjects : IFinderObjects
    {
        private float _radius;
        private string _tag;

        public CircleFinderObjects(string tag, float radius)
        {
            _tag = tag;
            _radius = radius;
        }

        public IEnumerable<GameObject> Find(Vector3 position)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(position, _radius);
            
            foreach(var collider in colliders)
            {
                if(collider.gameObject.tag == _tag)
                {
                    yield return collider.gameObject;
                }
            }
        }
    }
}