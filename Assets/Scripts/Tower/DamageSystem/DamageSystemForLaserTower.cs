using System.Collections;
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
    }
}