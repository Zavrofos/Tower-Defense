using System;
using Assets.Scripts;
using Assets.Scripts.Spawn_waves_configs;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _timeToSpawnNextWave;
    [SerializeField] private bool IsTest;
    [SerializeField] private WavesConfig _wavesConfig;
    [SerializeField] private WavesConfig _wavesConfigTest;
    private WavesConfig _waves;
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
    
    private void Awake()
    {
        GameManager.Instance.CurrentSpawner = this;
    }

    private void Start()
    {
        GameManager.Instance.GameOverlay.SetNextWaveButton.onClick.AddListener(SetNextWave);
        
        _waves = IsTest ? _wavesConfigTest : _wavesConfig;
        SetWave(_currentWaveNumber);
        
        foreach(var wave in _waves.Waves)
        {
            _countEnemyesInLevel += wave.Templates.Length;
        }
    }

    private void Update()
    {
        if (CurrentCountOfEnemyesKilled >= _countEnemyesInLevel) 
        {
            OnWinLevel?.Invoke();
            return;
        }

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
            
            if(_currentWaveNumber == _waves.Waves.Count - 1)
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
                SetInteractableNextWaveButton(_currentWave != _waves.Waves[^1]);
                return;
            }
            
            InstantiateEnemy(_currentTemplateNumber);
            _currentTemplateNumber++;
        }
    }

    private void SetNextWave()
    {
        _timeAfterPreviousWave = _timeToSpawnNextWave;
    }

    private void SetWave(int index)
    {
        _currentTemplateNumber = 0;
        _currentWave = _waves.Waves[index];
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
