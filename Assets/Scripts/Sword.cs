using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float Speed;
    public int Damage;
    private void Update()
    {
        transform.Rotate(Vector3.forward * -Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Tower>(out Tower tower))
        {
            tower.ApplayDamage(Damage);
        }
    }
}
