using Assets.Scripts.Tower;
using Towers.ShootTowers;
using UnityEngine;

namespace Towers.DecelerationSystems
{
    public class DecelerationForShootTower : IDeceleration
    {
        private ShootTower _shootTower;

        public DecelerationForShootTower(ShootTower shootTower)
        {
            _shootTower = shootTower;
        }

        public void SetDeceleration(bool value)
        {
            _shootTower.CurrentSpeedRotation = value ? _shootTower.SpeedRotation / 2 : _shootTower.SpeedRotation;
            _shootTower.CurrentDelayTimeToShoot = value ? _shootTower._delayTimeToShoot * 2 : _shootTower._delayTimeToShoot;
            _shootTower._spriteRendererTower.color = value ? _shootTower.DecelerateColor : _shootTower.InitialColor;

            if (_shootTower._fire && _shootTower.TryGetComponent(out SpriteRenderer spriteRenderer))
                spriteRenderer.color = value ? _shootTower.DecelerateColor : _shootTower.InitialColor;
        }
    }
}
