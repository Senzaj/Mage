using System.Collections.Generic;
using UnityEngine;

namespace Sources.Modules.Sound.Scripts
{
    public class SoundContainer : MonoBehaviour
    {
        private List<AudioSource> _audioSources = new List<AudioSource>();

        public void ChangeVolume(float volume)
        {
            foreach (AudioSource audioSource in _audioSources)
                audioSource.volume = volume;
        }

        public void AddAudioSource(AudioSource audioSource) => _audioSources.Add(audioSource);
    }
}