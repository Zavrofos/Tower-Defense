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

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audio = GameAssets.Instance.SoundAudioClips;
        }

        public void PlaySound(SoundType type)
        {
            foreach(var clip in _audio)
            {
                if(clip.Sound == type)
                {
                    _audioSource.clip = clip.AudioClip;
                    _audioSource.outputAudioMixerGroup = clip.Output;
                    break;
                }
            }
            _audioSource.Play();
            StartCoroutine(TurnOff(_audioSource.clip.length));
        }

        private IEnumerator TurnOff(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            ObjectPooler.Instance.ReturnToPool(this);
        }
    }
}