using Assets.Dev.DevScripts;
using System.Collections.Generic;
using UnityEngine;

public class StarterGame : MonoBehaviour
{
    [HideInInspector] public GameModel GameModel;
    public GameView GameView;
    public List<IPresenter> Presenters;
    public List<IUpdatable> Updaters
        ;

    private void Awake()
    {
        GameModel = new GameModel();
        Presenters = new List<IPresenter>()
        {

        };

        Updaters = new List<IUpdatable>()
        {

        };
    }

    private void Update()
    {
        foreach (var updater in Updaters)
        {
            updater.Update();
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
