using System;
using System.Collections;
using System.Net;
using UnityEngine;

public class TowerLaser : Tower
{
    public Transform _startPoint;
    public Transform _endPoint;
    public Laser Lazer;

    public GameObject _secondPartTowerImprove;
    public Transform _startPointImprove;
    public Transform _endPointImprove;
    public Laser LazerImprove;

    public float LaserSpawnRate;
    public LayerMask enemyLayer;
    public int _damage;

    public bool IsImproved = false;

    public AudioSource AudioLaser;

    public override void StartGame()
    {
        _endPoint.localPosition = new Vector2(0, 0);
        Lazer.LineRenderer.SetPosition(0, _startPoint.localPosition);
        Lazer.LineRenderer.SetPosition(1, _endPoint.localPosition);

        _endPointImprove.localPosition = new Vector2(0, 0);
        LazerImprove.LineRenderer.SetPosition(0, _startPointImprove.localPosition);
        LazerImprove.LineRenderer.SetPosition(1, _endPointImprove.localPosition);
    }

    public override void UpdateGame()
    {
        if (CheckEnemyes())
        {
            WorkTower();
        }
        else
        {
            StopTower();
        }
    }

    public override IEnumerator ChangeColorForHit()
    {
        if(IsImproved) _secondPartTowerImprove.gameObject.GetComponent<SpriteRenderer>().color = ApplayDamageColor;
        SpriteRender.color = ApplayDamageColor;
        yield return new WaitForSeconds(0.2f);
        SpriteRender.color = CurrentColor;
        if(IsImproved) _secondPartTowerImprove.gameObject.GetComponent<SpriteRenderer>().color = CurrentColor;
    }

    public bool CheckEnemyes()
    {
        Collider2D[] enemyes = Physics2D.OverlapCircleAll(transform.position, _firingRadius, enemyLayer);
        if (enemyes.Length == 0)
        {
            return false;
        }
        return true;
    }

    public void WorkTower()
    {
        if (_endPoint.localPosition.y < _firingRadius)
        {
            if(!IsImproved)
            {
                OnLazer();
            }
            else
            {
                OnLazer();
                OnLazerImprove();
            }
        }
        else
        {
            Lazer.BoxCollider.enabled = true;
            if(IsImproved) LazerImprove.BoxCollider.enabled = true;
            _partToRotate.Rotate(Vector3.forward * _speedRotation * Time.deltaTime);
        }
    }

    public void StopTower()
    {
        if (_partToRotate.rotation.eulerAngles.z > 0.5f || _partToRotate.rotation.eulerAngles.z < -0.5f)
        {
            _partToRotate.Rotate(Vector3.forward * _speedRotation * Time.deltaTime);
        }
        else if (_endPoint.localPosition.y > 0)
        {
            if(!IsImproved)
            {
                OffLazer();
            }
            else
            {
                OffLazer();
                OffLazerImprove();
            }
        }
    }

    public void OnLazer()
    {
        if(!AudioLaser.isPlaying) AudioLaser.Play();
        float positionY = _endPoint.localPosition.y;
        positionY += LaserSpawnRate * Time.deltaTime;
        Vector2 newPosition = new Vector2(0, positionY);
        _endPoint.localPosition = newPosition;
        Lazer.LineRenderer.SetPosition(1, _endPoint.localPosition);
    }

    public void OffLazer()
    {
        Lazer.BoxCollider.enabled = false;
        float positionY = _endPoint.localPosition.y;
        positionY -= LaserSpawnRate * Time.deltaTime;
        Vector2 newPosition = new Vector2(0, positionY);
        _endPoint.localPosition = newPosition;
        Lazer.LineRenderer.SetPosition(1, _endPoint.localPosition);
    }

    public void OnLazerImprove()
    {
        float positionY = _endPointImprove.localPosition.y;
        positionY += LaserSpawnRate * Time.deltaTime;
        Vector2 newPosition = new Vector2(0, positionY);
        _endPointImprove.localPosition = newPosition;
        LazerImprove.LineRenderer.SetPosition(1, _endPointImprove.localPosition);
    }

    public void OffLazerImprove()
    {
        LazerImprove.BoxCollider.enabled = false;
        float positionY = _endPointImprove.localPosition.y;
        positionY -= LaserSpawnRate * Time.deltaTime;
        Vector2 newPosition = new Vector2(0, positionY);
        _endPointImprove.localPosition = newPosition;
        LazerImprove.LineRenderer.SetPosition(1, _endPointImprove.localPosition);
    }

    public override void Improve()
    {
        _endPoint.localPosition = new Vector2(0, 0);
        Lazer.LineRenderer.SetPosition(1, _endPoint.localPosition);
        Lazer.BoxCollider.enabled = false;
        _secondPartTowerImprove.SetActive(true);
        IsImproved = true;
    }

    public void GiveDamageEnemy(Enemy enemy)
    {
        if (enemy is EnemyFly) return;
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
