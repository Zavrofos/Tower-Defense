using Assets.Scripts;
using Assets.Scripts.RepPoolObject;
using UnityEngine;

namespace GameOverlayWindow
{
    public class RocketAbility : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private Vector2 _direction;

        private void Start()
        {
            _direction = transform.position - transform.position;
        }

        private void Update()
        {
            float distance = (transform.position - transform.position).magnitude;
            
            if(distance < 0.2f)
                Destroy();
            
            transform.Translate(_direction * _speed * Time.deltaTime);
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