using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Assets.Scripts
{
    public class DamageEffect : IGivingEffects
    {
        private int _damage;

        public DamageEffect(int damage)
        {
            _damage = damage;
        }

        public void SetEffect(GameObject target)
        {
            IApplayDamage applayDamageObject = target.GetComponent<IApplayDamage>();
            applayDamageObject.ApplayDamage(_damage);
        }
    }
}