using Assets.Dev.DevScripts;
using Assets.Dev.DevScripts.Game.LevelsMenu;
using Assets.Dev.DevScripts.Levels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            new OpenLevelsMenuPresenterInGame(View),
            new CloseLevelsMenuPresenterInGame(View),
            new OpenSettingsMenuPresenterInGame(View),
            new CloseSettingsMenuPresenterInGame(View)
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
