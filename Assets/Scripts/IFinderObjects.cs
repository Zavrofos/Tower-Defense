using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public interface IFinderObjects
    {
        IEnumerable<GameObject> Find(string tag, Vector3 position);
    }
}