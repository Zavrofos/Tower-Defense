using System;
using System.Collections.Generic;
using System.Threading;
using Assets.Scripts.RepPoolObject;
using Assets.Scripts.Tower.DamageSystem;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.MeteorsAbility
{
    public class Meteor : PooledObject
    {
        [SerializeField] private PolledObjectType _type;
        public override PolledObjectType Type => _type;
        
        public Animator Animator;
        public SpriteRenderer MeteorSpriteRenderer;
        public float MeteorSpeed;
        public float MeteorSetScaleSpeed;
        private Vector2 FinishMeteorScale = Vector2.zero;
        private Vector2 FromDirection = new Vector2(2, 1);

        [SerializeField] private List<AudioClip> _explosionAudioClips;
        
        private IFinderObjects _finderObjectsSystem;
        private IGivingEffects _givingEffectsSystem;

        private void Awake()
        {
            _finderObjectsSystem = new CircleFinderObjects(2);
            _givingEffectsSystem = new DamageEffect(5);
        }

        public async UniTask PlayMeteorsShower(CancellationToken token)
        {
            SetupInitialState();

            await FlyToTarget(token);
            
            if(token.IsCancellationRequested)
                return;
            
            int randomAudio = Random.Range(0, _explosionAudioClips.Count - 1);
            
            SoundBox soundBox = (SoundBox)GameManager.Instance.ObjectPooler.SpawnFromPool(PolledObjectType.SoundBox, Vector3.zero, Quaternion.identity);
            soundBox.Play(_explosionAudioClips[randomAudio], false);
            
            await PlayDestruction(token);
            
            if(token.IsCancellationRequested)
                return;
            
            GameManager.Instance.ObjectPooler.ReturnToPool(this);
        }
        
        private void SetupInitialState()
        {
            MeteorSpriteRenderer.transform.position = (Vector2)transform.position + FromDirection.normalized * 10f;
            MeteorSpriteRenderer.transform.localScale = Vector3.one;
            MeteorSpriteRenderer.gameObject.SetActive(true);
            Animator.ResetTrigger("Destruct");
        }
        
        private async UniTask FlyToTarget(CancellationToken token)
        {
            while (true)
            {
                if(token.IsCancellationRequested)
                    return;
                
                MeteorSpriteRenderer.transform.position = Vector2.MoveTowards(MeteorSpriteRenderer.transform.position, 
                    transform.position, MeteorSpeed * Time.deltaTime);

                if (Vector2.Distance(MeteorSpriteRenderer.transform.position, transform.position) < 0.05f)
                    break;

                await UniTask.Yield();
            }
        }
        
        private async UniTask PlayDestruction(CancellationToken token)
        {
            Animator.gameObject.SetActive(true);
            Animator.SetTrigger("Destruct");
            
            foreach(var target in _finderObjectsSystem.Find("Enemy", transform.position))
                _givingEffectsSystem.SetEffect(target);

            foreach (var target in _finderObjectsSystem.Find("Tower", transform.position))
                if(target.TryGetComponent(out DamageSystem damageSystem))
                    damageSystem.ApplayDamage(5);

            while (MeteorSpriteRenderer.transform.localScale.x > 0.05f)
            {
                MeteorSpriteRenderer.transform.localScale = Vector2.Lerp(MeteorSpriteRenderer.transform.localScale,
                        FinishMeteorScale, MeteorSetScaleSpeed * Time.deltaTime);

                await UniTask.Yield();
                
                if(token.IsCancellationRequested)
                    return;
            }
            
            Animator.gameObject.SetActive(false);
        }
    }
}