using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Enemyes.AttackBehaviours
{
    public class EnemyFly2AttackBehaviour : MonoBehaviour, IAttackBehaviour
    {
        public ParticleSystem AttackParticleSystem;
        public bool Attacking { get; set; }

        private bool _isDestroyed;

        public void Attack(IDamageSystem target)
        {
            AttackTower(target).Forget();
        }

        private async UniTask AttackTower(IDamageSystem target)
        {
            Attacking = true;
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
            Attacking = target != null;
            if(_isDestroyed || !Attacking) return;
            AttackParticleSystem.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
            Attacking = target != null;
            if(_isDestroyed || !Attacking) return;
            target.ApplayDamage(1);
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            Attacking = target != null;
            if(_isDestroyed || !Attacking) return;
            target.ApplayDamage(1);
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            Attacking = target != null;
            if(_isDestroyed || !Attacking) return;
            target.ApplayDamage(1);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            Attacking = target != null;
            if(_isDestroyed || !Attacking) return;
            AttackParticleSystem.Stop();
            Attacking = false;
        }

        private void OnDestroy()
        {
            _isDestroyed = true;
        }
    }
}