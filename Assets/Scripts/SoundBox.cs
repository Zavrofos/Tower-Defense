using Assets.Scripts.Enums;
using Assets.Scripts.RepPoolObject;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class SoundBox : PooledObject
    {
        [SerializeField] private string _tag;
        public override string Tag => _tag;
        private AudioSource _audioSource;
        private SoundAudioClip[] _audio;
        private SoundAudioClip _currentAudioClip;
        public SoundAudioClip CurrentAudioClip => _currentAudioClip;


        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audio = GameAssets.Instance.SoundAudioClips;
        }

        public void PlaySound(SoundType type)
        {
            foreach(var clip in _audio)
            {
                if (clip.Sound == type)
                {
                    _currentAudioClip = clip;
                    break;
                }
            }

            if(_currentAudioClip == null)
            {
                throw new System.Exception("This audio is not Exist");
            }

            _audioSource.clip = _currentAudioClip.AudioClip;
            _audioSource.outputAudioMixerGroup = _currentAudioClip.Output;

            if(_currentAudioClip.SoundCategory == SoundCategory.Loop ||
                _currentAudioClip.SoundCategory == SoundCategory.BackgrounMelody)
            {
                _audioSource.loop = true;
            }
            else
            {
                StartCoroutine(TurnOff(_audioSource.clip.length));
            }

            _audioSource.Play();
        }

        private IEnumerator TurnOff(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            ObjectPooler.Instance.ReturnToPool(this);
        }

        private void OnDisable()
        {
            _currentAudioClip = null;
        }
    }
}