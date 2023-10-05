using Assets.Scripts.Tower.FinderEnemyes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinderEnemyes : IFinderEnemyesSystem
{
    private Transform _targetEnemy;
    private GameObject _tower;

    public FinderEnemyes(GameObject tower)
    {
        _tower = tower;
    }

    Transform IFinderEnemyesSystem.TargetEnemy => _targetEnemy;

    public void FindNearbyEnemy()
    {
        GameObject[] enemyes = Find();
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

    private GameObject[] Find()
    {
        Enemy[] enemyes = GameObject.FindObjectsOfType<Enemy>();
        List<GameObject> result = new List<GameObject>();

        foreach(var enemy in enemyes)
        {
            foreach(var type in _tower.GetComponent<AbsTower>().TargetsEnemyType)
            {
                if(type == enemy.Type)                {
                    result.Add(enemy.gameObject);
                }
            }
        }

        return result.ToArray();
    }
}
