using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMedium : Bullet
{
    private void Start()
    {
        RotateBullet();
    }
    private void RotateBullet()
    {
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;
    }
}
