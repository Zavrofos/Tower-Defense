using System;
using System.Collections;
using UnityEngine;

namespace Assets.Dev.DevScripts.Game.PauseMenu
{
    public class PauseModel 
    {
        public event Action TurnedOnPause;
        public event Action TurnedOffPause;

        public void TurnOnPause()
        {
            TurnedOnPause?.Invoke();
        }

        public void TurnOffPause()
        {
            TurnedOffPause?.Invoke();
        }
    }
}