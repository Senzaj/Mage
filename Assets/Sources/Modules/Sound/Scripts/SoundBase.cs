using UnityEngine;

namespace Sources.Modules.Sound.Scripts
{
    public abstract class SoundBase : MonoBehaviour
    {
        protected AudioSource CurrentAudioSource;

        public void Init(SoundContainer soundContainer, AudioSource audioSource)
        {
            transform.parent = soundContainer.transform;
            CurrentAudioSource = Instantiate(audioSource, soundContainer.transform.position, Quaternion.identity, soundContainer.transform);
            soundContainer.AddAudioSource(CurrentAudioSource);
        }
        
        protected void PlayClip(AudioClip clip, Vector3 position)
        {
            CurrentAudioSource.transform.position = position;
            
            CurrentAudioSource.clip = clip;
            CurrentAudioSource.Play();
        }
    }
}