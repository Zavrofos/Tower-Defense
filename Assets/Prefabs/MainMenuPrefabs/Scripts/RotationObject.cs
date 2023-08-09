using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObject : MonoBehaviour
{
    [SerializeField] private RectTransform _transform;
    [SerializeField] private float _speed;

    private void Update()
    {
        _transform.Rotate(Vector3.forward * _speed * Time.deltaTime);
    }
}
