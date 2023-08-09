using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public LineRenderer LineRenderer;
    public BoxCollider2D BoxCollider;
    public event Action<Enemy> FoundEnemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            FoundEnemy?.Invoke(enemy);
        }
    }
}
