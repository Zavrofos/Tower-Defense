using Assets.Scripts;
using Assets.Scripts.RepPoolObject;
using UnityEngine;

namespace GameOverlayWindow
{
    public class RocketAbility : MonoBehaviour
    {
        [SerializeField] private Transform _rocket;
        [SerializeField] private float _speed;
        [SerializeField] private AudioClip _startAudioClip;
        [SerializeField] private AudioClip _endAudioClip;
        private Vector2 _direction;

        private SoundBox _startSoundBox;
        private SoundBox _stopSoundBox;

        private void Start()
        {
            _direction = (transform.position - _rocket.position).normalized;
            _startSoundBox = (SoundBox) GameManager.Instance.ObjectPooler.SpawnFromPool(PolledObjectType.SoundBox, Vector3.zero, Quaternion.identity);
            _startSoundBox.Play(_startAudioClip, false);
        }

        private void Update()
        {
            float distance = (_rocket.position - transform.position).magnitude;
            
            if(distance < 0.5f)
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
            _startSoundBox.Stop();
            _stopSoundBox = (SoundBox) GameManager.Instance.ObjectPooler.SpawnFromPool(PolledObjectType.SoundBox, Vector3.zero, Quaternion.identity);
            _stopSoundBox.Play(_endAudioClip, false);
            explosion.ExplosonPlay();
        }
    }
}