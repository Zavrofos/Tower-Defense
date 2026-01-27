using System;
using Assets.Scripts;
using Assets.Scripts.Spawn_waves_configs;
using UnityEngine;

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
    public int CurrentCountOfEnemyesKilled;
    public int CurrentCountOfEnemyesKilledInCurrentWave;

    public bool IsWin;

    private int _currentTemplateNumber;

    public event Action<bool> OnSetNextWave;

    private void Awake()
    {
        GameManager.Instance.CurrentSpawner = this;
    }

    private void Start()
    {
        _waves = IsTest ? _wavesConfigTest : _wavesConfig;
        SetWave(_currentWaveNumber);
        
        foreach(var wave in _waves.Waves)
        {
            _countEnemyesInLevel += wave.Templates.Length;
        }
    }

    private void OnDestroy()
    {
        OnSetNextWave = null;
    }

    private void Update()
    {
        if (IsWin) return;
        if (CurrentCountOfEnemyesKilled >= _countEnemyesInLevel) 
        {
            IsWin = true;
            GameManager.Instance.CurrentGameManagerLevel.IsPouse = true;
            AudioManager.Instance.PauseAudio();
            return;
        }

        if (_currentWave == null)
        {
            return;
        }

        if(_isNextWaveActive)
        {
            _timeAfterPreviousWave += Time.deltaTime;
            if(_timeAfterPreviousWave >= _timeToSpawnNextWave || CurrentCountOfEnemyesKilledInCurrentWave == _currentWave.Templates.Length)
            {
                _isNextWaveActive = false;
                OnSetNextWave?.Invoke(false);
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
            }
            return;
        }

        _timeAfterLastSpawn += Time.deltaTime;

        if(_timeAfterLastSpawn >= _currentWave.Delay)
        {
            _timeAfterLastSpawn = 0;
            if(_currentTemplateNumber > _currentWave.Templates.Length - 1)
            {
                _isNextWaveActive = true;
                OnSetNextWave?.Invoke(_currentWave != _waves.Waves[^1]);
                return;
            }
            
            InstantiateEnemy(_currentTemplateNumber);
            _currentTemplateNumber++;
        }
    }

    public void SetNextWave()
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
}

[System.Serializable]
public class Wave
{
    public GameObject[] Templates;
    public float Delay;
}
