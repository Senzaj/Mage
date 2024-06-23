using Sources.Modules.Particles.Scripts.Pool;
using UnityEngine;

namespace Sources.Modules.Particles.Scripts
{
    public class ParticleSpawner : MonoBehaviour
    {
        [SerializeField] private ParticlePool _pool;

        public void SpawnParticle(ParticleType particleType, Vector3 position)
        {
            Particle particle = _pool.GetObject(particleType);
            particle.transform.position = position;
            particle.Enable();
        } 
    }
}
