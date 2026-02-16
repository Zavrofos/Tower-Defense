using Assets.Scripts;
using Assets.Scripts.RepPoolObject;
using UnityEngine;

namespace GameOverlayWindow
{
    public class RocketAbility : MonoBehaviour
    {
        [SerializeField] private Transform _rocket;
        [SerializeField] private float _speed;
        private Vector2 _direction;

        private void Start()
        {
            _direction = (transform.position - _rocket.position).normalized;
        }

        private void Update()
        {
            float distance = (_rocket.position - transform.position).magnitude;
            
            if(distance < 0.2f)
                Destroy();
            
            _rocket.Translate(_direction * _speed * Time.deltaTime);
        }

        private void Destroy()
        {
            BlowUp();
            Destroy(gameObject);
        }

        private void BlowUp()
        {
            PooledObject pooledObj = GameManager.Instance.ObjectPooler.SpawnFromPool(PolledObjectType.ExplosionRocket, transform.position, Quaternion.identity);
            Explosion explosion = (Explosion)pooledObj;
            explosion.ExplosonPlay();
        }
    }
}