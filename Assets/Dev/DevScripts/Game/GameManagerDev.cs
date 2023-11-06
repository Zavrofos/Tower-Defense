using Assets.Dev.DevScripts.Game;
using Assets.Dev.DevScripts.Game.LevelsMenu;
using Assets.Dev.DevScripts.Game.OptionsMenu;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Dev.DevScripts
{
    public class GameManagerDev : MonoBehaviour
    {
        [HideInInspector] public GameModel Model;
        public GameViewDev View;
        public List<IPresenter> Presenters;
        public List<IUpdatable> Updaters;

        public static GameManagerDev Instance;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            Model = new GameModel();

            Presenters = new()
            {
                new InitializeLevelMenuPresenter(Model, View),
                new CloseLevelsMenuPresenter(View.LevelsMenuView),
                new InitializeSettingsMenuPresenter(View.SettingsMenuView, Model),
                new CloseSettingsMenuPresenter(View.SettingsMenuView),
                new SetQualityPresenter(View.SettingsMenuView, Model.SettingsModel),
                new SetResolutionPresenter(View.SettingsMenuView, Model.SettingsModel),
                new SetFullscreenPresenter(View.SettingsMenuView, Model.SettingsModel),
                new SetVolumeMusicPresenter(View.SettingsMenuView, Model.SettingsModel),
                new SetVolumeGamePresenter(View.SettingsMenuView, Model.SettingsModel),
            };

            Updaters = new()
            {

            };
        }

        private void Start()
        {
            Model.Initialize();
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
            if (Instance != null) return;

            foreach (var presenter in Presenters)
            {
                presenter.Unsubscribe();
            }
        }
    }
}