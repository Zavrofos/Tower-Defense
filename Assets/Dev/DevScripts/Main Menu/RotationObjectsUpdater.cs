using System.Collections;
using UnityEngine;

namespace Assets.Dev.DevScripts.Main_Menu
{
    public class RotationObjectsUpdater : IUpdatable
    {
        private MainMenuView _view;

        public RotationObjectsUpdater(MainMenuView view)
        {
            _view = view;
        }

        public void Update(float deltaTime)
        {
            foreach(var rotationObject in _view.RotationObjects)
            {
                rotationObject.transform.Rotate(rotationObject.transform.forward * rotationObject.SpeedRotation * deltaTime);
            }
        }
    }
}