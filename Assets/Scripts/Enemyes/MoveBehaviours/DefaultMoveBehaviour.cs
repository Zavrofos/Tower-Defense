using System;
using UnityEngine;

namespace Assets.Scripts.Enemyes.MoveBehaviours
{
    public class DefaultMoveBehaviour : MonoBehaviour, IMoveBehaviour
    {
        [field: SerializeField] public float Speed { get; set; }
        public float CurrentSpeed { get; set; }

        private Transform[] _pointsOfWay;
        private int _currentPointOfWay;
        private Enemy _enemy;

        private void Awake()
        {
            _pointsOfWay = GameManager.Instance.CurrentGameManagerLevel.PointsOfWayForEnemy;
            _enemy = GetComponent<Enemy>();
        }
        
        public void Move()
        {
            var point = _pointsOfWay[_currentPointOfWay].position;
            if(transform.position != point)
            {
                transform.position = Vector2.MoveTowards(transform.position, point, Speed * Time.deltaTime);
            }
            else
            {
                if(_currentPointOfWay >= _pointsOfWay.Length - 1)
                {
                    GameManager.Instance.CurrentSpawner.CurrentCountOfEnemyesKilled++;
                    Destroy(gameObject);
                }
            
                if(_currentPointOfWay < _pointsOfWay.Length - 1)
                {
                    Vector2 direction = _pointsOfWay[_currentPointOfWay + 1].position - transform.position;
                    _enemy.Animator.SetFloat("X", direction.x);
                    _enemy.Animator.SetFloat("Y", direction.y);
                }
                _currentPointOfWay++;
            }
        }
    }
}