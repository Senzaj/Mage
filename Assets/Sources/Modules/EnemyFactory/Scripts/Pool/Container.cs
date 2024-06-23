using System.Collections.Generic;
using Sources.Modules.Enemy;
using Sources.Modules.Particles.Scripts;
using Sources.Modules.Sound.Scripts;
using UnityEngine;

namespace Sources.Modules.EnemyFactory.Scripts.Pool
{
    internal class Container : MonoBehaviour
    {
        private EnemyUnit _prefab;
        private ParticleSpawner _particleSpawner;
        private SoundContainer _soundContainer;
        private AudioSource _audioSourcePrefab;
        private EnemySound _enemySoundPrefab;
        
        public EnemyType EnemyType { get; private set; }

        private List<EnemyUnit> _units;

        public void Init(EnemyType enemyType, EnemyUnit prefab, ParticleSpawner particleSpawner,
            SoundContainer soundContainer, AudioSource audioSourcePrefab, EnemySound enemySoundPrefab)
        {
            EnemyType = enemyType;
            _prefab = prefab;
            _particleSpawner = particleSpawner;
            _soundContainer = soundContainer;
            _audioSourcePrefab = audioSourcePrefab;
            _enemySoundPrefab = enemySoundPrefab;
            _units = new List<EnemyUnit>();
        }

        public void AddUnit(EnemyUnit unit)
        {
            _units.Add(unit);
        }

        public List<EnemyUnit> GetUnits(int unitCount)
        {
            List<EnemyUnit> inactiveUnits = new List<EnemyUnit>();

            if (_units.Count < unitCount)
            {
                foreach (var unit in _units)
                {
                    if (unit.gameObject.activeSelf == false)
                        inactiveUnits.Add(unit);
                }

                int difference = unitCount - inactiveUnits.Count;

                for (int i = 0; i < difference; i++)
                {
                    EnemyUnit spawned = Instantiate(_prefab, transform.position, Quaternion.identity,
                        gameObject.transform);
                    EnemySound enemySound = Instantiate(_enemySoundPrefab, _soundContainer.transform.position,
                        Quaternion.identity, _soundContainer.transform);
                    enemySound.Init(_soundContainer, _audioSourcePrefab);
                    spawned.Init(enemySound);
                    spawned.SetParticleSpawner(_particleSpawner);
                    _units.Add(spawned);
                    inactiveUnits.Add(spawned);
                }
            }
            else
            {
                for (int i = 0; i < unitCount; i++)
                {
                    if (_units[i].gameObject.activeSelf == false)
                        inactiveUnits.Add(_units[i]);
                }
            }
            
            return inactiveUnits;
        }
    }
}
