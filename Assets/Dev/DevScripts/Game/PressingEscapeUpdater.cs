using UnityEngine;

namespace Dev.DevScripts.Game
{
    public class PressingEscapeUpdater : IUpdatable
    {
        private GameModel _model;

        public PressingEscapeUpdater(GameModel model)
        {
            _model = model;
        }

        public void Update(float deltaTime)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                _model.PressEscape();
            }
        }
    }
}