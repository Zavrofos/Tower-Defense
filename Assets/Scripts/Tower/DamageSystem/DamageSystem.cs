using System.Collections;
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
    }
}