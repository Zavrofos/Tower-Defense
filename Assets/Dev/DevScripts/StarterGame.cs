using Assets.Dev.DevScripts;
using Assets.Dev.DevScripts.Main_Menu;
using System.Collections.Generic;
using UnityEngine;

public class StarterGame : MonoBehaviour
{
    
    [HideInInspector] public GameModel GameModel;
    public GameView GameView;
    public List<IPresenter> Presenters;
    public List<IUpdatable> Updaters;

    #region Singleton
    [HideInInspector] public static StarterGame Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    private void Start()
    {
        GameModel = new GameModel();
        Presenters = new List<IPresenter>()
        {

        };

        Updaters = new List<IUpdatable>()
        {
            new RotationObjectsUpdater(GameView)
        };

        Enable();
    }

    private void Update()
    {
        foreach (var updater in Updaters)
        {
            updater.Update(Time.deltaTime);
        }
    }

    private void Enable()
    {
        foreach(var presenter in Presenters)
        {
            presenter.Subscribe();
        }
    }

    private void Disable()
    {
        foreach (var presenter in Presenters)
        {
            presenter.Unsubscribe();
        }
    }
}
