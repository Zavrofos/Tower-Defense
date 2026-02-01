using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Tower.DamageSystem
{
    public class DamageSystem : AbsDamageSystem
    {
        protected override IEnumerator ChangeColorForHit()
        {
            SpriteRender.color = ApplayDamageColor;
            yield return new WaitForSeconds(0.2f);
            SpriteRender.color = CurrentColor;
        }

        protected override async UniTask DestroyTower()
        {
            await base.DestroyTower();
            
            AbsTower.enabled = false;
            DestructorAnimator.SetTrigger("Destruct");
            
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            
            if(IsDestroyed)
                return;
            
            SpriteRender.enabled = false;
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            
            if(IsDestroyed)
                return;
            
            Destroy(gameObject);
        }
    }
}