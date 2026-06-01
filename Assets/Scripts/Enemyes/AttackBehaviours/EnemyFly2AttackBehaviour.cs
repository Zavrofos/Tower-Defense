using System;
using Assets.Scripts.RepPoolObject;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Enemyes.AttackBehaviours
{
    public class EnemyFly2AttackBehaviour : MonoBehaviour, IAttackBehaviour
    {
        public ParticleSystem AttackParticleSystem;
        public int Damage;
        public bool Attacking { get; set; }

        [SerializeField] private AudioClip _attackAudio;

        private SoundBox _soundBox;
        private bool _isDestroyed;

        public void Attack(IDamageSystem target)
        {
            AttackRoutine(target).Forget();
        }

        private async UniTask AttackRoutine(IDamageSystem target)
        {
            Attacking = true;

            // подготовка
            if (!await WaitAndCheck(target, 0.2f)) return;

            PlayParticles();

            // первый удар
            if (!await WaitAndDamage(target, 0.2f)) return;

            // второй
            if (!await WaitAndDamage(target, 1f)) return;

            // третий
            if (!await WaitAndDamage(target, 1f)) return;

            // завершение
            if (!await WaitAndCheck(target, 0.5f)) return;

            StopParticles();
            Attacking = false;
        }

        private async UniTask<bool> WaitAndDamage(IDamageSystem target, float delay)
        {
            if (!await WaitAndCheck(target, delay))
                return false;

            target.ApplayDamage(Damage);
            return true;
        }

        private async UniTask<bool> WaitAndCheck(IDamageSystem target, float delay)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delay));

            if (_isDestroyed)
                return false;

            Attacking = IsTargetValid(target);

            if (!Attacking)
                StopParticles();

            return Attacking;
        }

        private bool IsTargetValid(IDamageSystem target)
        {
            return target != null && !target.IsStartDestroyAnimation;
        }

        private void PlayParticles()
        {
            if (AttackParticleSystem && !AttackParticleSystem.isPlaying)
                AttackParticleSystem.Play();

            PlayAttackSound();
        }

        private void StopParticles()
        {
            if (AttackParticleSystem && AttackParticleSystem.isPlaying)
                AttackParticleSystem.Stop();

            StopAttackSound();
        }

        private void PlayAttackSound()
        {
            if (!_attackAudio || _soundBox)
                return;

            _soundBox = (SoundBox)GameManager.Instance.ObjectPooler.SpawnFromPool(
                PolledObjectType.SoundBox, transform.position, transform.rotation);
            _soundBox.Play(_attackAudio, true);
        }

        private void StopAttackSound()
        {
            if (!_soundBox)
                return;

            _soundBox.Stop();
            _soundBox = null;
        }

        private void OnDestroy()
        {
            _isDestroyed = true;
            StopAttackSound();
        }
    }
}