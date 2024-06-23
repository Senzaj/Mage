using System.Collections.Generic;
using UnityEngine;
using Sources.Modules.Pools.Scripts;

namespace Sources.Modules.Particles.Scripts.Pool
{
    public class ParticlePool : Pool<Particle>
    {
        [SerializeField] private ParticleContainer _prefabContainer;

        private List<ParticleContainer> _containers;

        public void Awake()
        {
            _containers = new List<ParticleContainer>();

            foreach (Particle prefab in Prefabs)
            {
                ParticleContainer container = Instantiate(_prefabContainer, transform.position, Quaternion.identity,
                    transform);
                container.Init(prefab.ParticleType, prefab);
                _containers.Add(container);
                
                for (int i = 0; i < StartCapacity; i++)
                {
                    Particle spawned = Instantiate(prefab, transform.position, Quaternion.identity, container.transform);
                    spawned.Disable();
                    container.AddParticle(spawned);
                }
            }
        }

        public Particle GetObject(ParticleType particleType)
        {
            Particle particle = null;

            foreach (ParticleContainer container in _containers)
            {
                if (container.ParticleType == particleType)
                {
                    particle = container.GetParticle();
                    break;
                }
            }

            return particle;
        }
    }
}
