using Sources.Modules.Sound.Scripts;
using UnityEngine;

namespace Sources.Modules.Player.Scripts
{
    public class PlayerSound : SoundBase
    {
        [SerializeField] private AudioClip _dieClip;
        [SerializeField] private AudioClip _damagedClip;

        public void DamagedPlay(Vector3 position)
        {
            PlayClip(_damagedClip, position);
        }

        public void DiePlay(Vector3 position)
        {
            PlayClip(_dieClip, position);
        }
    }
}