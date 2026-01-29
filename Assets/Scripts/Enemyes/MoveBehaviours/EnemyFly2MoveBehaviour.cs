using System;
using System.Collections.Generic;
using Assets.Scripts.Enemyes.AttackBehaviours;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Enemyes.MoveBehaviours
{
    public class EnemyFly2MoveBehaviour : MonoBehaviour, IMoveBehaviour
    {
        [field: SerializeField] public float Speed { get; set; }
        
        private Transform[] _pointsOfWay;
        private int _currentPointOfWay;
        private Enemy _enemy;
        private bool _isDestroyed;

        private void Awake()
        {
            _pointsOfWay = GameManager.Instance.CurrentGameManagerLevel.PointsOfWayForEnemy;
            _enemy = GetComponent<Enemy>();
        }

        public void Move()
        {
            if(_enemy.AttackBehaviour.Attacking)
                return;
            
            var point = _pointsOfWay[_currentPointOfWay].position;
            if(transform.position != point)
            {
                transform.position = Vector2.MoveTowards(transform.position, point, Speed * Time.deltaTime);
                Vector2 direction = point - transform.position;
                _enemy.Animator.SetFloat("X", direction.x);
                _enemy.Animator.SetFloat("Y", direction.y);
            }
            else
            {
                if(_currentPointOfWay >= _pointsOfWay.Length - 1)
                {
                    GameManager.Instance.CurrentSpawner.CurrentCountOfEnemyesKilled++;
                    Destroy(gameObject);
                }
                
                _currentPointOfWay++;
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("PointToAttackTowers"))
            {
                RaycastHit2D hit = Physics2D.Raycast(
                    transform.position, 
                    other.transform.up, 
                    2f, 
                    LayerMask.GetMask("Tower"));
                
                if (!hit || !hit.collider.gameObject.TryGetComponent(out IDamageSystem applyDamage)) 
                    return;
                
                // _enemy.AttackBehaviour?.Attack(applyDamage);
                // _enemy.Animator.SetFloat("X", other.transform.up.x);
                // _enemy.Animator.SetFloat("Y", other.transform.up.y);
                
                StartAttack(other.transform, applyDamage).Forget();
            }
        }

        private async UniTask StartAttack(Transform attackPoint, IDamageSystem targetToAttack)
        {
            await UniTask.WaitUntil(() => Vector2.Distance(transform.position, attackPoint.position) <= 0.1f);
            if(_isDestroyed) return;

            EnemyFly2AttackBehaviour attackBehaviour = (EnemyFly2AttackBehaviour)_enemy.AttackBehaviour;
            attackBehaviour.Attack(targetToAttack);
            attackBehaviour.AttackParticleSystem.transform.rotation = attackPoint.rotation;
            _enemy.Animator.SetFloat("X", attackPoint.up.x);
            _enemy.Animator.SetFloat("Y", attackPoint.up.y);
        }

        private void OnDestroy()
        {
            _isDestroyed = true;
        }
    }
}