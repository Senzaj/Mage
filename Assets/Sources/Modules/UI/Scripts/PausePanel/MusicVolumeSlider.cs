using Sources.Modules.Sound.Scripts;
using UnityEngine;

namespace Sources.Modules.UI.Scripts.PausePanel
{
    public class MusicVolumeSlider : VolumeSlider
    {
        [SerializeField] private BackgroundSound _backgroundSound;

        protected override void ChangeVolume(float volume)
        {
            _backgroundSound.ChangeVolume(volume);
            base.ChangeVolume(volume);
        }

        public override void SetVolume(float value)
        {
            _backgroundSound.ChangeVolume(value);
            base.SetVolume(value);
        }
    }
}