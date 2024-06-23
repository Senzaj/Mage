using System.Collections.Generic;
using Sources.Modules.Enemy;
using Sources.Modules.Particles.Scripts;
using Sources.Modules.Pools.Scripts;
using Sources.Modules.Sound.Scripts;
using UnityEngine;

namespace Sources.Modules.EnemyFactory.Scripts.Pool
{
    public class EnemyPool : Pool<EnemyUnit>
    {
        [SerializeField] private Container _prefabContainer;
        [SerializeField] private SoundContainer _soundContainer;
        [SerializeField] private AudioSource _audioSourcePrefab;
        [SerializeField] private EnemySound _enemySoundPrefab;

        private List<Container> _containers;
        private ParticleSpawner _particleSpawner;

        public void Init(ParticleSpawner particleSpawner)
        {
            _particleSpawner = particleSpawner;
            
            _containers = new List<Container>();

            foreach (EnemyUnit prefab in Prefabs)
            {
                Container container = Instantiate(_prefabContainer, transform.position, Quaternion.identity,
                    transform);

                container.Init(prefab.EnemyType, prefab, _particleSpawner, _soundContainer, _audioSourcePrefab,
                    _enemySoundPrefab);
                _containers.Add(container);

                for (int i = 0; i < StartCapacity; i++)
                {
                    EnemyUnit spawned = Instantiate(prefab, transform.position, Quaternion.identity,
                        container.transform);
                    EnemySound enemySound = Instantiate(_enemySoundPrefab, _soundContainer.transform.position,
                        Quaternion.identity, _soundContainer.transform);
                    enemySound.Init(_soundContainer, _audioSourcePrefab);
                    spawned.Init(enemySound);
                    spawned.SetParticleSpawner(_particleSpawner);
                    spawned.gameObject.SetActive(false);
                    container.AddUnit(spawned);
                }
            }
        }

        public List<EnemyUnit> GetObjects(EnemyType enemyType, int unitCount)
        {
            List<EnemyUnit> units = new List<EnemyUnit>();
            
            foreach (Container container in _containers)
            {
                if (container.EnemyType == enemyType)
                {
                    units = container.GetUnits(unitCount);
                    
                    break;
                }
            }

            return units;
        }
    }
}
