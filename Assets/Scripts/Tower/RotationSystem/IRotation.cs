using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRotation
{
    public Transform PartToRotate { get; }
    void Rotate(Transform target = null);
}
