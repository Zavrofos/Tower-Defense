using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _timeToSpawnNextWave;
    [SerializeField] private List<Wave> _waves;
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

    private void Start()
    {
        SetWave(_currentWaveNumber);
        _gameManager = FindObjectOfType<GameManagerInGame>();

        foreach(var wave in _waves)
        {
            _countEnemyesInLevel += wave.Templates.Length;
        }
    }

    private void Update()
    {
        if (IsWin) return;
        if (CurrentCountOfEnemyesKilled >= _countEnemyesInLevel) 
        {
            var winMenu = FindObjectOfType<WinMenu>();
            winMenu.OpenNextLevel();
            LevelsManager levelManager = FindObjectOfType<LevelsManager>();
            for (int i = 0; i < levelManager.Levels.Count; i++)
            {
                Level level = levelManager.Levels[i];
                if (level.IsOpen)
                {
                    winMenu.LevelsViews[level.Label].OpenLevel();
                }
            }
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
                _isNextWaveActive = false;
                CurrentCountOfEnemyesKilledInCurrentWave = 0;
                if(_currentWaveNumber == _waves.Count - 1)
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
            if(_currentWave.CurrentTemplateNumber > _currentWave.Templates.Length - 1)
            {
                _isNextWaveActive = true;
                return;
            }
            InstantiateEnemy(_currentWave.CurrentTemplateNumber);
            _currentWave.CurrentTemplateNumber++;
        }
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
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
    public int CurrentTemplateNumber;
}
