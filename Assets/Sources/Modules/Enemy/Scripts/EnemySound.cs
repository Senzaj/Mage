using Sources.Modules.Sound.Scripts;
using UnityEngine;

namespace Sources.Modules.Enemy
{
    public class EnemySound : SoundBase
    {
        [SerializeField] private AudioClip _dieClip;
        [SerializeField] private AudioClip _damagedClip;

        public void PlayDamaged(Vector3 position)
        {
            PlayClip(_damagedClip, position);
        }

        public void PlayDie(Vector3 position)
        {
            PlayClip(_dieClip, position);
        }
    }
}