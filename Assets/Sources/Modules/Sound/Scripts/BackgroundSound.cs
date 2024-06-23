using System.Collections;
using UnityEngine;

namespace Sources.Modules.Sound.Scripts
{
    [RequireComponent(typeof(AudioSource))]
    public class BackgroundSound : MonoBehaviour
    {
        [SerializeField] private AudioClip[] _backgroundClips;

        private int _backgroundClipsIndex;
        private AudioSource _audioSourceBackgroundClips;
        private Coroutine _audioPlayWork;

        private void Awake()
        {
            _audioSourceBackgroundClips = GetComponent<AudioSource>();
            
            _backgroundClipsIndex = 0;
            _audioPlayWork = null;
            
            StartNewClip();
        }

        public void ChangeVolume(float volume)
        {
            _audioSourceBackgroundClips.volume = volume;
        }
        
        private void StartNewClip()
        {
            if (_audioPlayWork != null)
                StopCoroutine(_audioPlayWork);

            _audioPlayWork = StartCoroutine(AudioPlay());
        }

        private IEnumerator AudioPlay()
        {
            WaitUntil waitUntil = new (() => _audioSourceBackgroundClips.isPlaying == false) ;

            _audioSourceBackgroundClips.clip = _backgroundClips[_backgroundClipsIndex];
            _audioSourceBackgroundClips.Play();
            _backgroundClipsIndex++;
            _backgroundClipsIndex %= _backgroundClips.Length;
            
            yield return waitUntil;

            StartNewClip();
        }
    }
}
