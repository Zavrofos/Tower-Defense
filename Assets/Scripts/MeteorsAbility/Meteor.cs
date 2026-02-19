using System;
using System.Collections.Generic;
using System.Threading;
using Assets.Scripts.RepPoolObject;
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