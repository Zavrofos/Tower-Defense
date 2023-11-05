using System.Collections;
using UnityEngine;

namespace Assets.Dev.DevScripts.Main_Menu
{
    public class RotationObjectsUpdater : IUpdatable
    {
        private GameView _gameView;

        public RotationObjectsUpdater(GameView gameView)
        {
            _gameView = gameView;
        }

        public void Update(float deltaTime)
        {
            if (_gameView.MainMenuView == null) 
            {
                return;
            }

            foreach(var rotationObject in _gameView.MainMenuView.RotationObjects)
            {
                rotationObject.transform.Rotate(rotationObject.transform.forward * rotationObject.SpeedRotation * deltaTime);
            }
        }
    }
}