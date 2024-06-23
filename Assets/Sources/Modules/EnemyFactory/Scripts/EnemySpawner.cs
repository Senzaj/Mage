using System.Collections;
using System.Collections.Generic;
using Sources.Modules.Common;
using Sources.Modules.Enemy;
using Sources.Modules.EnemyFactory.Scripts.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.Modules.EnemyFactory.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<SpawnPoint> _spawnPoints;
        [SerializeField] private Transform _playerPosition;

        private const float ObstacleCheckRadius = 2.5f;
        private const float SpawningCooldown = 1f;

        private int _collidersCount; 
        private EnemyPool _enemyPool;
        private List<EnemyUnit> _currentUnits;
        private List<EnemyUnit> _allWaveUnits;
        private Coroutine _spawningWork;

        public void Init(EnemyPool enemyPool)
        {
            _enemyPool = enemyPool;
        }

        public void SpawnEnemies(Dictionary<List<EnemyType>, int> wave, int waveCount)
        {
            _currentUnits = new List<EnemyUnit>();
            _allWaveUnits = new List<EnemyUnit>();
            
            foreach (var keyValuePair in wave)
            {
                foreach (var enemyType in keyValuePair.Key)
                {
                    _currentUnits = _enemyPool.GetObjects(enemyType, keyValuePair.Value);
                    
                    foreach (EnemyUnit unit in _currentUnits)
                    {
                        unit.AddLevels(waveCount);
                        unit.SetTarget(_playerPosition);
                    }
                        
                    _allWaveUnits.AddRange(_currentUnits);
                }
            }

            if (_spawningWork != null)
                StopCoroutine(_spawningWork);

            _spawningWork = StartCoroutine(Spawning());
        }

        public void TryStopSpawning()
        {
            if (_spawningWork != null)
                StopCoroutine(_spawningWork);
        }

        public List<EnemyUnit> GetEnemies() => _allWaveUnits.GetRange(0, _allWaveUnits.Count);

        private IEnumerator Spawning()
        {
            WaitForSeconds spawnCooldown = new (SpawningCooldown);

            foreach (var enemyUnit in _allWaveUnits)
            {
                Collider2D enemyCollider = enemyUnit.Collider2D;
                bool inObstacle = false;
                
                do
                {
                    Vector3 newPosition = _spawnPoints[Random.Range(0, _spawnPoints.Count)].transform.position;
                    enemyUnit.transform.position = newPosition;

                    Collider2D[] colliders = Physics2D.OverlapCircleAll(newPosition, ObstacleCheckRadius);
                    
                    foreach (var collider in colliders)
                    {
                        inObstacle = collider != enemyCollider && collider.TryGetComponent(out Obstacle _);
                        
                        if (inObstacle) 
                            break;
                        
                        yield return null;
                    }

                    yield return null;

                } while (inObstacle);

                enemyUnit.gameObject.SetActive(true);

                yield return spawnCooldown;
            }
        }
    }
}
