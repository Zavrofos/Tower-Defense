using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityRocket : Ability
{
    [SerializeField] private Transform _rocketTransform;
    [SerializeField] private float _speed;
    
    private Vector2 _direction;

    private void Start()
    {
        _direction = transform.position - _rocketTransform.position;
    }

    private void Update()
    {
        float distance = (transform.position - _rocketTransform.position).magnitude;
        if(distance < 0.5f)
        {
            Destroy();
        }
        _rocketTransform.Translate(_direction * _speed * Time.deltaTime);
    }
}
