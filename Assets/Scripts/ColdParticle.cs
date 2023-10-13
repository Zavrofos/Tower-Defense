using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class ColdParticle : IPlayableParticle
    {
        private ParticleSystem _coldEffect;

        public ColdParticle(ParticleSystem coldEffect)
        {
            _coldEffect = coldEffect;
        }

        public void Play()
        {
            if (!_coldEffect.isPlaying)
            {
                _coldEffect.Play();
            }
        }

        public void Stop()
        {
            if(_coldEffect.isPlaying)
            {
                _coldEffect.Stop();
            }
        }
    }
}