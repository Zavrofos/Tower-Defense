using System.Collections;
using UnityEngine;

namespace Assets.Dev.DevScripts.Game.Pause
{
    public class PressingPauseUpdater : IUpdatable
    {
        private GameModel _model;

        public PressingPauseUpdater(GameModel model)
        {
            _model = model;
        }

        public void Update(float deltaTime)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                _model.PressPause();
            }
        }
    }
}