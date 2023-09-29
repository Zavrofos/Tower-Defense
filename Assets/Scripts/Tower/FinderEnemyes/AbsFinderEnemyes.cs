using Assets.Scripts.Tower.FinderEnemyes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsFinderEnemyes : MonoBehaviour, IFinderEnemyesSystem
{
    [HideInInspector] private Transform _targetEnemy;

    Transform IFinderEnemyesSystem.TargetEnemy => _targetEnemy;

    private void Start()
    {
        InvokeRepeating("FindNearbyEnemy", 0, 1);
    }

    public void FindNearbyEnemy()
    {
        GameObject[] enemyes = FindEnemyes();
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        float FiringRadius = gameObject.GetComponent<AbsTower>().FiringRadius;

        foreach (var enemy in enemyes)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance < FiringRadius)
        {
            _targetEnemy = nearestEnemy.transform;
        }
        else
        {
            _targetEnemy = null;
        }
    }

    public abstract GameObject[] FindEnemyes();
}
