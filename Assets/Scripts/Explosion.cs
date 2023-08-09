using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Transform Transform;
    public ParticleSystem ExplosionParticle;

    private void Start()
    {
        ExplosionParticle.Play();
        Destroy(gameObject, 0.5f);
    }
}
