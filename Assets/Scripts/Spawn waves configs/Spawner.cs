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

    private GameManagerInGame _gameManager;

    private int _currentTemplateNumber;

    private void Start()
    {
        _waves = IsTest ? _wavesConfigTest : _wavesConfig;
        SetWave(_currentWaveNumber);
        _gameManager = FindObjectOfType<GameManagerInGame>();

        foreach(var wave in _waves.Waves)
        {
            _countEnemyesInLevel += wave.Templates.Length;
        }
    }

    private void Update()
    {
        if (IsWin) return;
        if (CurrentCountOfEnemyesKilled >= _countEnemyesInLevel) 
        {
            IsWin = true;
            _gameManager.IsPouse = true;
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
                Debug.Log("(test) 1");
                _isNextWaveActive = false;
                CurrentCountOfEnemyesKilledInCurrentWave = 0;
                if(_currentWaveNumber == _waves.Waves.Count - 1)
                {
                    Debug.Log("(test) 2");
                    _currentWave = null;
                }
                else
                {
                    Debug.Log("(test) 3");
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
                Debug.Log("(test) instantiated 1");
                return;
            }
            Debug.Log("(test) instantiated 2");
            InstantiateEnemy(_currentTemplateNumber);
            _currentTemplateNumber++;
        }
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
