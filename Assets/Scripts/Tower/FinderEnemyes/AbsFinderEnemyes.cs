using Assets.Scripts.Tower.FinderEnemyes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsFinderEnemyes : IFinderEnemyesSystem
{
    [HideInInspector] private Transform _targetEnemy;
    private GameObject _tower;

    public AbsFinderEnemyes(GameObject tower)
    {
        _tower = tower;
    }

    Transform IFinderEnemyesSystem.TargetEnemy => _targetEnemy;

    public void FindNearbyEnemy()
    {
        GameObject[] enemyes = FindEnemyes();
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        float FiringRadius = _tower.GetComponent<AbsTower>().FiringRadius;

        foreach (var enemy in enemyes)
        {
            float distanceToEnemy = Vector2.Distance(_tower.transform.position, enemy.transform.position);
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

    protected abstract GameObject[] FindEnemyes();
}
