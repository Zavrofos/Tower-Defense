using Assets.Scripts.Tower;
using Assets.Scripts.Tower.TowerLaserNew;

namespace Towers.DecelerationSystems
{
    public class DecelerationForNewLaserTower : IDeceleration
    {
        private NewLaserTower _newLaserTower;

        public DecelerationForNewLaserTower(NewLaserTower newLaserTower)
        {
            _newLaserTower = newLaserTower;
        }

        public void SetDeceleration(bool value)
        {
            _newLaserTower.CurrentSpeedRotation = value ? _newLaserTower.CurrentSpeedRotation / 2 : _newLaserTower.CurrentSpeedRotation;
            _newLaserTower.CurrentDelayTimeToShoot = value ? _newLaserTower._delayTimeToShoot * 2 : _newLaserTower._delayTimeToShoot;
            _newLaserTower._spriteRendererTower.color = value ? _newLaserTower.DecelerateColor : _newLaserTower.InitialColor;
            _newLaserTower.Laser.colorGradient = value ? _newLaserTower.DecelerateLaserColor : _newLaserTower.InitialLaserColor;
        }
    }
}