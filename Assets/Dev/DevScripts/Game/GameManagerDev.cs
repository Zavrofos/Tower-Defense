using Assets.Dev.DevScripts.Game;
using Assets.Dev.DevScripts.Game.LevelsMenu;
using Assets.Dev.DevScripts.Game.SettingsMenu;
using Assets.Dev.DevScripts.Game.Pause;
using Assets.Dev.DevScripts.Game.PauseMenu;
using Assets.Dev.DevScripts.Levels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Dev.DevScripts.Game.OptionsMenu;
using Dev.DevScripts.Game.LevelsMenu;
using Dev.DevScripts.Game.PauseMenu;

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
                new ButtonLevelsMenuInPauseMenuPresenter(View, Model),
                new ButtonCloseLevelsMenuPresenter(View, Model),
                new OpenLevelsMenuPresenter(View, Model),
                new CloseLevelsMenuPresenter(View, Model),
                new ButtonOpenSettingsMenuInPausePresenter(View, Model),
                new ButtonCloseSettingsMenuPresenter(View, Model),
                new OpenSettingsMenuPresenter(View, Model),
                new CloseSettingsMenuPresenter(View, Model),
                new InitializeSettingsMenuPresenter(View.SettingsMenuView, Model),
                new SetQualityPresenter(View.SettingsMenuView, Model.SettingsModel),
                new SetResolutionPresenter(View.SettingsMenuView, Model.SettingsModel),
                new SetFullscreenPresenter(View.SettingsMenuView, Model.SettingsModel),
                new SetVolumeMusicPresenter(View.SettingsMenuView, Model.SettingsModel),
                new SetVolumeGamePresenter(View.SettingsMenuView, Model.SettingsModel),
                new PressingEscapePresenter(View, Model),
                new ReturnToMainMenuPresenterInPause(),
                new QuitGamePresenterInPause(),
                new TurnOnPausePresenter(View, Model),
                new TurnOffPausePresenter(View, Model),
                new CreatingLevelButtonPresenter(View, Model)
            };

            Updaters = new()
            {
                new PressingEscapeUpdater(Model)
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