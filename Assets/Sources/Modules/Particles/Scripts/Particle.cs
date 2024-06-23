using UnityEngine;

namespace Sources.Modules.Particles.Scripts
{
    public class Particle : MonoBehaviour
    {
        [SerializeField] private ParticleType _particleType;

        public ParticleType ParticleType => _particleType;

        public void Enable() => gameObject.SetActive(true); 
            
        public void Disable() => gameObject.SetActive(false);
    }
}
