using Assets.Scripts.Tower;

namespace Towers.DecelerationSystems
{
    public class DecelerationForTowerOfCold : IDeceleration
    {
        private TowerOfCold _towerOfCold;

        public DecelerationForTowerOfCold(TowerOfCold towerOfCold)
        {
            _towerOfCold = towerOfCold;
        }

        public void SetDeceleration(bool value)
        {
            _towerOfCold.CurrentSpeedRotation = value ? _towerOfCold.CurrentSpeedRotation / 2 : _towerOfCold.CurrentSpeedRotation;
            _towerOfCold._spriteRendererTower.color = value ? _towerOfCold.DecelerateColor : _towerOfCold.InitialColor;
        }
    }
}