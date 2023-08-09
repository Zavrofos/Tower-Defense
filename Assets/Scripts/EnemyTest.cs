using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public Animator Animator;
    public Transform[] PointsOfWay;
    public Transform PathPointsOfWay;
    public float Speed;
    private int CurrentPointOfWay;


    private void Start()
    {
        PointsOfWay = new Transform[PathPointsOfWay.childCount];
        for (int i = 0; i < PointsOfWay.Length; i++)
        {
            PointsOfWay[i] = PathPointsOfWay.GetChild(i);
        }
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        var point = PointsOfWay[CurrentPointOfWay].position;
        if (transform.position != point)
        {
            transform.position = Vector2.MoveTowards(transform.position, point, Speed * Time.deltaTime);
        }
        else
        {
            if (CurrentPointOfWay >= PointsOfWay.Length - 1)
            {
                Destroy(gameObject);
            }

            if (CurrentPointOfWay < PointsOfWay.Length - 1)
            {
                Vector2 direction = PointsOfWay[CurrentPointOfWay + 1].position - transform.position;
                Debug.Log(direction);
                Animator.SetFloat("X", direction.x);
                Animator.SetFloat("Y", direction.y);
                //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            CurrentPointOfWay++;
        }
    }
}
