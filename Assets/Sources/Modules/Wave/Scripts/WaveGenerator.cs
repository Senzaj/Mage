using System;
using System.Collections.Generic;
using Sources.Modules.CoinFactory.Scripts;
using Sources.Modules.Enemy;
using Sources.Modules.EnemyFactory.Scripts;
using Sources.Modules.Finder.Scripts;
using Sources.Modules.Player.Scripts;
using Sources.Modules.Player.Scripts.UI;
using Sources.Modules.UI.Scripts.LeaderBoard;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.Modules.Wave.Scripts
{
    public class WaveGenerator : MonoBehaviour
    {
        [SerializeField] private LeaderList _leaderList;
        [SerializeField] private EnemySpawner _spawner;
        [SerializeField] private FinderCloseEnemy _finder;
        [SerializeField] private CoinSpawner _coinSpawner;
        [SerializeField] private LosePanel _losePanel;
        [SerializeField] private Mage _mage;

        private const int StartMinEnemySpawn = 3;
        private const int StartMaxEnemySpawn = 6;
        private const int Step = 3;
        private const int WaveCountToAddStep = 23;
        private const int StartWaveIndex = 0;
        private const int StartWaveCount = 1;
        
        private int _waveIndex;
        private int _waveCount;
        private int _minEnemySpawn;
        private int _maxEnemySpawn;
        
        private EnemyWaveConfig _enemyConfig;
        private List<EnemyUnit> _spawnedEnemies;
        private Dictionary<List<EnemyType>, int> _wave;
        private WaveConfigs _waveConfigs;
        private WaveData _waveData;

        public event Action UnitDied;
        public event Action<int> WaveStarted;
        public event Action WaveEnded;
        public event Action<int> WaveCountChanged;
        
        private void Start()
        {
            _wave = new Dictionary<List<EnemyType>, int>();
            _waveConfigs = new WaveConfigs();

            _waveData = WaveSaver.Instance.GetData() ?? new WaveData();

            if (_waveData is {WaveCount: <= StartWaveCount, WaveIndex: <= StartWaveIndex, MinEnemySpawn: <= StartMinEnemySpawn, MaxEnemySpawn: <= StartMaxEnemySpawn})
            {
                _waveData.WaveCount = StartWaveCount;
                _waveData.WaveIndex = StartWaveIndex;
                _waveData.MinEnemySpawn = StartMinEnemySpawn;
                _waveData.MaxEnemySpawn = StartMaxEnemySpawn;
            }

            _waveCount = _waveData.WaveCount;
            _waveIndex = _waveData.WaveIndex;
            _minEnemySpawn = _waveData.MinEnemySpawn;
            _maxEnemySpawn = _waveData.MaxEnemySpawn;

            SaveAll();

            WaveCountChanged?.Invoke(_waveCount);
        }

        private void OnEnable()
        {
            _losePanel.Rewarded += Reload;
            _losePanel.Restarted += Restart;
        }

        private void OnDisable()
        {
            _losePanel.Rewarded -= Reload;
            _losePanel.Restarted -= Restart;
        }
        
        public void StartWave()
        {
            _mage.UpdateCurrentHealth();
            
            SetNewWave();
            StartWave(_wave);
        }

        private void Restart()
        {
            Disable();
            
            _waveCount = StartWaveCount;
            _waveIndex = StartWaveIndex;
            _minEnemySpawn = StartMinEnemySpawn;
            _maxEnemySpawn = StartMaxEnemySpawn;
            
            WaveCountChanged?.Invoke(_waveCount);
            WaveEnded?.Invoke();
            
            SaveAll();
        }

        private void Reload()
        {
            Disable();

            WaveCountChanged?.Invoke(_waveCount);
            WaveEnded?.Invoke();
        }

        private void StartWave(Dictionary<List<EnemyType>, int> wave)
        {
            _spawner.SpawnEnemies(wave, _waveIndex);

            _spawnedEnemies = _spawner.GetEnemies();
            
            foreach (EnemyUnit unit in _spawnedEnemies)
            {
                unit.Died += OnUnitDied;
            }

            WaveStarted?.Invoke(_spawnedEnemies.Count);
            _finder.SetEnemyList(_spawnedEnemies);
            _coinSpawner.SetEnemies(_spawnedEnemies);
        }
        
        private void OnUnitDied(EnemyUnit unit)
        {
            unit.Died -= OnUnitDied;
            _spawnedEnemies.Remove(unit);

            UnitDied?.Invoke();

            if (_spawnedEnemies.Count <= 0)
                EndWave();
        }

        private void SetRandomUnits()
        {
            EnemyWaveConfig tempEnemyWaveConfig =
                new(_waveConfigs.GetWaveConfig(_waveIndex).GetEnemyTypes());
            tempEnemyWaveConfig.Init(Random.Range(_minEnemySpawn, _maxEnemySpawn));

            _enemyConfig = tempEnemyWaveConfig;
        }

        private void SetNewWave()
        {
            _wave.Clear();

            SetRandomUnits();

            _wave.Add(_enemyConfig.GetEnemyTypes(), _enemyConfig.SpawnCount);
        }
        
        private void EndWave()
        {
            if (_waveCount % WaveCountToAddStep == 0)
            {
                _minEnemySpawn += Step;
                _maxEnemySpawn += Step;
            }

            _waveCount++;
            _waveIndex++;
            _waveIndex %= _waveConfigs.GetWaveConfigsCount();
            
            _leaderList.SetLeaderboardScore(_waveCount);
            
            SaveAll();
            WaveCountChanged?.Invoke(_waveCount);
            WaveEnded?.Invoke();
        }

        private void Disable()
        {
            foreach (var enemyUnit in _spawnedEnemies)
            {
                enemyUnit.gameObject.SetActive(false);
                enemyUnit.Died -= OnUnitDied;
            }
            
            _spawner.TryStopSpawning();
            _spawnedEnemies = null;
            _coinSpawner.DisableCoins();
        }

        private void SaveAll()
        {
            _waveData.WaveCount = _waveCount;
            _waveData.WaveIndex = _waveIndex;
            _waveData.MinEnemySpawn = _minEnemySpawn;
            _waveData.MaxEnemySpawn = _maxEnemySpawn;
            WaveSaver.Instance.SaveData(_waveData);
        }
    }
}
