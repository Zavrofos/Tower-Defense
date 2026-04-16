using Assets.Scripts.Tower;

namespace Towers.DecelerationSystems
{
    public class DecelerationForLaserTower : IDeceleration
    {
        private TowerLaser _towerLaser;

        public DecelerationForLaserTower(TowerLaser towerLaser)
        {
            _towerLaser = towerLaser;
        }

        public void SetDeceleration(bool value)
        {
            _towerLaser.CurrentSpeedRotation = value ? _towerLaser.SpeedRotation / 2 : _towerLaser.SpeedRotation;
            _towerLaser.CurrentSpeedLaserOnOff = value ? _towerLaser.SpeedLaserOnOff / 2 : _towerLaser.SpeedLaserOnOff;
            _towerLaser.FirstPartTowerSpriteRenderer.color = value ? _towerLaser.DecelerateColor : _towerLaser.InitialColor;
            _towerLaser._secondPartTowerSpriteRenderer.color = value ? _towerLaser.DecelerateColor : _towerLaser.InitialColor;
            _towerLaser.Lazer.LineRenderer.colorGradient = value ? _towerLaser.DecelerateLaserColor : _towerLaser.InitialLaserColor;
            _towerLaser.LazerImprove.LineRenderer.colorGradient = value ? _towerLaser.DecelerateLaserColor : _towerLaser.InitialLaserColor;
        }
    }
}