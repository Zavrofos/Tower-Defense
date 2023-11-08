using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Dev.DevScripts.Main_Menu
{
    public class MainMenuManager : MonoBehaviour
    {
        public GameModel Model;
        public MainMenuView View;

        public List<IPresenter> Presenters;
        public List<IUpdatable> Updaters;

        private void Awake()
        {
            Model = GameManagerDev.Instance.Model;

            Presenters = new()
            {
                new PlayGamePresenter(View),
                new QuitGamePresenter(View),
                new ButtonLevelsMenuInMainMenuPresenter(View, Model),
                new OpenSettingsMenuPresenterInMainMenu(View)
            };

            Updaters = new()
            {
                new RotationObjectsUpdater(View)
            };
        }

        private void Update()
        {
            foreach (var updater in Updaters)
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