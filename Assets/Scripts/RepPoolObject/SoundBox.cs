using System;
using System.Collections;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.RepPoolObject
{
    public class SoundBox : PooledObject
    {
        [SerializeField] private PolledObjectType _type;
        public override PolledObjectType Type => _type;
        
        [field: SerializeField] public AudioSource AudioSource { get; private set; }

        private IDisposable _checkPauseDisposable;
        
        private Coroutine _turnOffCoroutine;
        private bool _isPaused;

        public event Action<SoundBox> OnFinished;
        
        public void Play(AudioClip audioClip, bool loop)
        {
            if(!audioClip)
                return;

            _checkPauseDisposable?.Dispose();
            _checkPauseDisposable = null;
            _isPaused = false;
            AudioSource.clip = audioClip;
            AudioSource.loop = loop;

            AudioSource.Play();
            
            if(!loop)
                _turnOffCoroutine = StartCoroutine(TurnOff());

            _checkPauseDisposable = Observable
                .EveryUpdate()
                .Subscribe((_) => Pause())
                .AddTo(this);
        }

        private void Pause()
        {
            if (Time.timeScale == 0 && AudioSource.isPlaying)
            {
                Debug.Log("Pause");
                AudioSource.Pause();
                _isPaused = true;
            }
            else if(Time.timeScale > 0 && _isPaused)
            {
                Debug.Log("UnPause");
                AudioSource.UnPause();
                _isPaused = false;
            }
        }

        public void Stop()
        {
            AudioSource.Stop();

            if (_turnOffCoroutine != null)
            {
                StopCoroutine(_turnOffCoroutine);
                _turnOffCoroutine = null;
            }

            _checkPauseDisposable?.Dispose();

            OnFinished?.Invoke(this);
            OnFinished = null;
            
            GameManager.Instance.ObjectPooler.ReturnToPool(this);
        }

        private IEnumerator TurnOff()
        {
            while (AudioSource.isPlaying || _isPaused)
                yield return null;

            _checkPauseDisposable?.Dispose();

            OnFinished?.Invoke(this);   
            OnFinished = null;          

            GameManager.Instance.ObjectPooler.ReturnToPool(this);
        }

        private void OnDestroy()
        {
            if (_turnOffCoroutine != null)
            {
                StopCoroutine(_turnOffCoroutine);
                _turnOffCoroutine = null;
            }
            
            AudioSource.Stop();
        }
    }
}