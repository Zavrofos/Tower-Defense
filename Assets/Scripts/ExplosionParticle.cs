using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class ExplosionParticle : IPlayableParticle
    {
        private ParticleSystem _explosionParticle;
        private float _radius;
        public ExplosionParticle(ParticleSystem explosionParticle, float radius)
        {
            _explosionParticle = explosionParticle;
            _radius = radius;
        }

        public void Play()
        {
            ParticleSystem.ShapeModule shapeModule = _explosionParticle.shape;
            shapeModule.radius = _radius / 2;
            _explosionParticle.Play();
        }
    }
}