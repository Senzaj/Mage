using Sources.Modules.Sound.Scripts;
using UnityEngine;

namespace Sources.Modules.UI.Scripts.PausePanel 
{
    public class SoundVolumeSlider : VolumeSlider
    {
        [SerializeField] private SoundContainer _soundContainer;

        protected override void ChangeVolume(float volume)
        {
            _soundContainer.ChangeVolume(volume);
            base.ChangeVolume(volume);
        }

        public override void SetVolume(float value)
        {
            _soundContainer.ChangeVolume(value);
            base.SetVolume(value);
        }
    } 
}