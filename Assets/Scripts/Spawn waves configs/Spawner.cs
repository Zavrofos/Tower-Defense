using System;
using Assets.Scripts;
using Assets.Scripts.Spawn_waves_configs;
using Assets.Scripts.UIScripts;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _timeToSpawnNextWave;
    [SerializeField] private WavesConfig _wavesConfig;
    [SerializeField] private Transform _spawnPoint;

    private Wave _currentWave;
    private int _currentWaveNumber;

    private float _timeAfterLastSpawn;
    private float _timeAfterPreviousWave;
    private bool _isNextWaveActive;

    private int _countEnemyesInLevel;
    public int CurrentCountOfEnemyesKilled { get; set; }
    public int CurrentCountOfEnemyesKilledInCurrentWave { get; set; }
    
    public event Action OnWinLevel;
    
    private int _currentTemplateNumber;

    private IDisposable _startSpawnerDisposable;
    
    private void Awake()
    {
        GameManager.Instance.CurrentSpawner = this;
    }

    private void Start()
    {
        GameManager.Instance.GameOverlay.SetNextWaveButton.onClick.AddListener(SetNextWave);
        
        SetWave(_currentWaveNumber);
        
        foreach(var wave in _wavesConfig.Waves)
            _countEnemyesInLevel += wave.Templates.Length;
        
        _startSpawnerDisposable = Observable.EveryUpdate()
            .Subscribe((_) => StartSpawner())
            .AddTo(this);
        
        Observable.EveryUpdate()
            .Where((_) => CurrentCountOfEnemyesKilled >= _countEnemyesInLevel)
            .First()
            .Subscribe((_) =>
            {
                OnWinLevel?.Invoke();
                _startSpawnerDisposable?.Dispose();
                _startSpawnerDisposable = null;
            })
            .AddTo(this);
    }

    private void StartSpawner()
    {
        if (_currentWave == null)
            return;

        if(_isNextWaveActive)
        {
            _timeAfterPreviousWave += Time.deltaTime;
            
            if (!(_timeAfterPreviousWave >= _timeToSpawnNextWave) &&
                CurrentCountOfEnemyesKilledInCurrentWave != _currentWave.Templates.Length) 
                return;
            
            _isNextWaveActive = false;
            SetInteractableNextWaveButton(false);
            CurrentCountOfEnemyesKilledInCurrentWave = 0;
            
            if(_currentWaveNumber == _wavesConfig.Waves.Count - 1)
            {
                _currentWave = null;
            }
            else
            {
                _currentWaveNumber++;
                SetWave(_currentWaveNumber);
            }
            
            _timeAfterPreviousWave = 0;
            return;
        }

        _timeAfterLastSpawn += Time.deltaTime;

        if(_timeAfterLastSpawn >= _currentWave.Delay)
        {
            _timeAfterLastSpawn = 0;
            if(_currentTemplateNumber > _currentWave.Templates.Length - 1)
            {
                _isNextWaveActive = true;
                SetInteractableNextWaveButton(_currentWave != _wavesConfig.Waves[^1]);
                return;
            }
            
            InstantiateEnemy(_currentTemplateNumber);
            _currentTemplateNumber++;
        }
    }

    private void SetNextWave()
    {
        ClickSoundPlayGlobal.Instance.Play(GameManager.Instance.GameAssets.CLickAudioUI);
        _timeAfterPreviousWave = _timeToSpawnNextWave;
    }

    private void SetWave(int index)
    {
        _currentTemplateNumber = 0;
        _currentWave = _wavesConfig.Waves[index];
    }

    private void InstantiateEnemy(int _numberEnemyInWave)
    {
        Instantiate(_currentWave.Templates[_numberEnemyInWave], _spawnPoint.position, _spawnPoint.rotation, _spawnPoint);
    }
    
    private void SetInteractableNextWaveButton(bool value)
    {
        GameManager.Instance.GameOverlay.SetNextWaveButton.interactable = value;
    }
}

[System.Serializable]
public class Wave
{
    public GameObject[] Templates;
    public float Delay;
}
