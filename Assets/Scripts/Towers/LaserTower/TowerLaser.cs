using Assets.Scripts;
using Assets.Scripts.RepPoolObject;
using Assets.Scripts.Tower.RotationSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Towers.DecelerationSystems;
using UnityEngine;

public class TowerLaser : AbsTower
{
    public Transform _startPoint;
    public Transform _endPoint;
    public Laser Lazer;

    public GameObject _secondPartTowerImprove;
    public SpriteRenderer FirstPartTowerSpriteRenderer;
    public SpriteRenderer _secondPartTowerSpriteRenderer;
    public Transform _startPointImprove;
    public Transform _endPointImprove;
    public Laser LazerImprove;

    public float LaserSpawnRate;
    public LayerMask enemyLayer;
    public int _damage;

    public bool IsImproved = false;
    private SoundBox _soundLaser;

    public float SpeedLaserOnOff = 1;
    public float CurrentSpeedLaserOnOff { get; set; }

    public Gradient InitialLaserColor;
    public Gradient DecelerateLaserColor;

    public override void StartGame()
    {
        RotationSystem = new Rotator(this);
        DecelerationSystem = new DecelerationForLaserTower(this);
        _endPoint.localPosition = new Vector2(0, 0);
        Lazer.LineRenderer.SetPosition(0, _startPoint.localPosition);
        Lazer.LineRenderer.SetPosition(1, _endPoint.localPosition);

        _endPointImprove.localPosition = new Vector2(0, 0);
        LazerImprove.LineRenderer.SetPosition(0, _startPointImprove.localPosition);
        LazerImprove.LineRenderer.SetPosition(1, _endPointImprove.localPosition);
        CurrentSpeedLaserOnOff = SpeedLaserOnOff;
    }

    public override void UpdateGame()
    {
        Transform targetEnemy = GetNearestEnemy(FinderNearestEnemies.Find("Enemy", transform.position));

        if (targetEnemy != null)
        {
            WorkTower();
        }
        else
        {
            StopTower();
        }
    }

    public void WorkTower()
    {
        if (_endPoint.localPosition.y < _firingRadius)
        {
            OnLazer();
        }
        else
        {
            Lazer.BoxCollider.enabled = true;
            if (IsImproved) LazerImprove.BoxCollider.enabled = true;
            RotationSystem.Rotate();
        }
    }

    public void StopTower()
    {
        if (PartToRotate.rotation.eulerAngles.z > 0.5f || 
            PartToRotate.rotation.eulerAngles.z < -0.5f)
        {
            RotationSystem.Rotate();
        }
        else if (_endPoint.localPosition.y > 0)
        {
            OffLazer();
        }
    }

    public void OnLazer()
    {
        if(_soundLaser == null)
        {
            _soundLaser = (SoundBox)ObjectPooler.Instance.SpawnFromPool("SoundBox",
            transform.position,
            transform.rotation);
            _soundLaser.PlaySound(SoundType.Laser);
        }

        float positionY = _endPoint.localPosition.y;
        positionY += LaserSpawnRate * CurrentSpeedLaserOnOff * Time.deltaTime;
        Vector2 newPosition = new Vector2(0, positionY);
        _endPoint.localPosition = newPosition;
        Lazer.LineRenderer.SetPosition(1, _endPoint.localPosition);

        if(IsImproved)
        {
            float positionY1 = _endPointImprove.localPosition.y;
            positionY += LaserSpawnRate * CurrentSpeedLaserOnOff * Time.deltaTime;
            Vector2 newPosition1 = new Vector2(0, positionY);
            _endPointImprove.localPosition = newPosition;
            LazerImprove.LineRenderer.SetPosition(1, _endPointImprove.localPosition);
        }
    }

    public void OffLazer()
    {
        if (_soundLaser != null)
        {
            _soundLaser = null;
        }

        Lazer.BoxCollider.enabled = false;
        float positionY = _endPoint.localPosition.y;
        positionY -= LaserSpawnRate * CurrentSpeedLaserOnOff * Time.deltaTime;
        Vector2 newPosition = new Vector2(0, positionY);
        _endPoint.localPosition = newPosition;
        Lazer.LineRenderer.SetPosition(1, _endPoint.localPosition);

        if(IsImproved)
        {
            LazerImprove.BoxCollider.enabled = false;
            float positionY1 = _endPointImprove.localPosition.y;
            positionY1 -= LaserSpawnRate * CurrentSpeedLaserOnOff * Time.deltaTime;
            Vector2 newPosition1 = new Vector2(0, positionY1);
            _endPointImprove.localPosition = newPosition1;
            LazerImprove.LineRenderer.SetPosition(1, _endPointImprove.localPosition);
        }
    }

    public override void Improve()
    {
        _endPoint.localPosition = new Vector2(0, 0);
        Lazer.LineRenderer.SetPosition(1, _endPoint.localPosition);
        Lazer.BoxCollider.enabled = false;
        _secondPartTowerImprove.SetActive(true);
        IsImproved = true;

        if (_soundLaser != null)
        {
            _soundLaser = null;
        }
    }

    public void GiveDamageEnemy(Enemy enemy)
    {
        if (enemy.Type is EnemyType.Fly) return;
        enemy.ApplayDamage(_damage);
    }

    private void OnEnable()
    {
        Lazer.FoundEnemy += GiveDamageEnemy;
        LazerImprove.FoundEnemy += GiveDamageEnemy;
    }

    private void OnDisable()
    {
        Lazer.FoundEnemy -= GiveDamageEnemy;
        LazerImprove.FoundEnemy -= GiveDamageEnemy;
    }
}
