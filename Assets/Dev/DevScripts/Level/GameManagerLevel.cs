using System.Collections.Generic;
using Dev.DevScripts.Game;
using UnityEngine;

namespace Dev.DevScripts.Level
{
    public class GameManagerLevel : MonoBehaviour
    {
        [HideInInspector] public GameModel Model;
        public LevelViewDev View;
        public List<IPresenter> Presenters;
        public List<IUpdatable> Updaters;

        private void Awake()
        {
            Model = GameManagerDev.Instance.Model;

            Presenters = new()
            {
            
            };

            Updaters = new()
            {

            };
        }

        private void Update()
        {
            foreach(var updater in Updaters)
            {
                updater.Update(Time.deltaTime);
            }
        }

        private void OnEnable()
        {
            foreach(var presenter in Presenters)
            {
                presenter.Subscribe();
            }
        }

        private void OnDisable()
        {
            foreach (var presenter in Presenters)
            {
                presenter.Unsubscribe();
            }
        }
    }
}
