using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Tower.DamageSystem
{
    public class DamageSystemForLaserTower : AbsDamageSystem
    {
        [SerializeField] private SpriteRenderer _secondPartTowerSpriteRenderer;
        protected override IEnumerator ChangeColorForHit()
        {
            if (_secondPartTowerSpriteRenderer.enabled) _secondPartTowerSpriteRenderer.color = ApplayDamageColor;
            SpriteRender.color = ApplayDamageColor;
            yield return new WaitForSeconds(0.2f);
            SpriteRender.color = CurrentColor;
            if (_secondPartTowerSpriteRenderer.enabled) _secondPartTowerSpriteRenderer.color = CurrentColor;
        }
        
        protected override async UniTask DestroyTower()
        {
            await base.DestroyTower();
            
            AbsTower.enabled = false;
            DestructorAnimator.SetTrigger("Destruct");
            
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            
            if(IsDestroyed)
                return;
            
            if (_secondPartTowerSpriteRenderer.enabled)
                _secondPartTowerSpriteRenderer.enabled = false;
            
            SpriteRender.enabled = false;
            
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            
            if(IsDestroyed)
                return;
            
            Destroy(gameObject);
        }
    }
}