using System.Collections.Generic;
using Sources.Modules.Particles.Scripts;
using Sources.Modules.Player.Scripts;
using Sources.Modules.Pools.Scripts;
using Sources.Modules.Weapons.Scripts.Common;
using UnityEngine;

namespace Sources.Modules.Weapons.Scripts
{
    public class ProjectilesPool : Pool<Projectile>
    {
        [SerializeField] private ProjectileContainer _prefabContainer;
        [SerializeField] private Mage _mage;

        private List<ProjectileContainer> _containers;
        private ParticleSpawner _particleSpawner;

        public void Init(ParticleSpawner particleSpawner)
        {
            _containers = new List<ProjectileContainer>();
            _particleSpawner = particleSpawner;

            foreach (Projectile prefab in Prefabs)
            {
                ProjectileContainer container = Instantiate(_prefabContainer, transform.position, Quaternion.identity,
                    transform);
                container.Init(prefab.SpellType, prefab, _particleSpawner);
                _containers.Add(container);
                
                for (int i = 0; i < StartCapacity; i++)
                {
                    Projectile spawned = Instantiate(prefab, transform.position, Quaternion.identity, container.transform);
                    spawned.SetParticleSpawner(_particleSpawner);
                    spawned.Disable();
                    container.AddProjectile(spawned);
                }
            }
        }

        public Projectile GetObject(SpellType spellType)
        {
            Projectile projectile = null;

            foreach (ProjectileContainer container in _containers)
            {
                if (container.SpellType == spellType)
                {
                    projectile = container.GetProjectile();
                    projectile.SetDamageScaler(_mage.DamageScaler);
                    break;
                }
            }

            return projectile;
        }
    }
}
