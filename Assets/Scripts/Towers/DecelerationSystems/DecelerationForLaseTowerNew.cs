using Assets.Scripts.Tower;
using Assets.Scripts.Tower.TowerLaserNew;

namespace Towers.DecelerationSystems
{
    public class DecelerationForLaseTowerNew : IDeceleration
    {
        private NewLaserTower _towerLaser;

        public DecelerationForLaseTowerNew(NewLaserTower towerLaser)
        {
            _towerLaser = towerLaser;
        }

        public void SetDeceleration(bool value)
        {
            _towerLaser.CurrentSpeedRotation = value ? _towerLaser.CurrentSpeedRotation / 2 : _towerLaser.CurrentSpeedRotation;
            _towerLaser._spriteRendererTower.color = value ? _towerLaser.DecelerateColorTower : _towerLaser.InitialColorTower;
            _towerLaser.LaserSprite.color = value ? _towerLaser.DecelerateLaserColor : _towerLaser.InitialLaserColor;
        }
    }
}