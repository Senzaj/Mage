using System.Collections.Generic;
using UnityEngine;

namespace Sources.Modules.Particles.Scripts.Pool
{
    public class ParticleContainer : MonoBehaviour
    {
        private Particle _prefab;
        private List<Particle> _particles;

        public ParticleType ParticleType { get; private set; }

        public void Init(ParticleType particleType, Particle prefab)
        {
            ParticleType = particleType;
            _prefab = prefab;
            _particles = new List<Particle>();
        }

        public void AddParticle(Particle particle)
        {
            _particles.Add(particle);
        }

        public Particle GetParticle()
        {
            Particle inactivePartilce = null;

            foreach (Particle particle in _particles)
            {
                if (particle.isActiveAndEnabled == false)
                {
                    inactivePartilce = particle;
                    break;
                }
            }

            if (inactivePartilce == null)
            {
                Particle spawned =
                    Instantiate(_prefab, transform.position, Quaternion.identity, transform);
                _particles.Add(spawned);
                inactivePartilce = spawned;
            }

            return inactivePartilce;
        }
    }
}
