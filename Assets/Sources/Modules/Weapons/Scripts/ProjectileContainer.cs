using System.Collections.Generic;
using Sources.Modules.Particles.Scripts;
using Sources.Modules.Weapons.Scripts.Common;
using UnityEngine;

namespace Sources.Modules.Weapons.Scripts
{
    internal class ProjectileContainer : MonoBehaviour
    {
        private Projectile _prefab;
        private List<Projectile> _projectiles;
        private ParticleSpawner _particleSpawner;

        public SpellType SpellType { get; private set; }

        public void Init(SpellType spellType, Projectile prefab, ParticleSpawner particleSpawner)
        {
            SpellType = spellType;
            _prefab = prefab;
            _particleSpawner = particleSpawner;
            _projectiles = new List<Projectile>();
        }

        public void AddProjectile(Projectile projectile)
        {
            _projectiles.Add(projectile);
        }

        public Projectile GetProjectile()
        {
            Projectile inactiveProjectile = null;

            foreach (Projectile projectile in _projectiles)
            {
                if (projectile.isActiveAndEnabled == false)
                {
                    inactiveProjectile = projectile;
                    break;
                }
            }

            if (inactiveProjectile == null)
            {
                Projectile spawned =
                    Instantiate(_prefab, transform.position, Quaternion.identity, transform);
                spawned.SetParticleSpawner(_particleSpawner);
                _projectiles.Add(spawned);
                inactiveProjectile = spawned;
            }

            return inactiveProjectile;
        }
    }
}
