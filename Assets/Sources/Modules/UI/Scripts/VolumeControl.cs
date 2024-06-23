using Sources.Modules.UI.Scripts.PausePanel;
using UnityEngine;

namespace Sources.Modules.UI.Scripts
{
    internal class VolumeControl : MonoBehaviour
    {
        [SerializeField] private MusicVolumeSlider _musicSlider;
        [SerializeField] private SoundVolumeSlider _soundSlider;
        [SerializeField] private Panel _settingsPanel;
        
        private const float MaxVolume = 1;

        private VolumeData _volumeData;
        private float _lastVolumeSound;
        private float _lastVolumeMusic;

        private void Awake()
        {
            _volumeData = VolumeSaver.Instance.GetData() ?? new VolumeData();

            if (_volumeData.VolumeChanged == false)
            {
                _volumeData = new VolumeData()
                {
                    MusicEnable = true,
                    SoundEnable = true,
                    MusicVolume = MaxVolume,
                    SoundVolume = MaxVolume,
                    VolumeChanged = true
                };
                
                VolumeSaver.Instance.SaveData(_volumeData);
            }
            else
            {
                _volumeData = VolumeSaver.Instance.GetData();
            }
        }

        private void Start()
        {
            _soundSlider.SetActive(_volumeData.SoundEnable);
            _soundSlider.SetVolume(_volumeData.SoundVolume);
            
            _musicSlider.SetActive(_volumeData.MusicEnable);
            _musicSlider.SetVolume(_volumeData.MusicVolume);
            
            TrySave();
        }

        private void OnEnable()
        {
            _soundSlider.RequestSaveVolume += OnSoundVolumeRequestSave;
            _soundSlider.RequestSaveEnabled += OnSoundActiveRequestSave;
            
            _musicSlider.RequestSaveVolume += OnMusicVolumeRequestSave;
            _musicSlider.RequestSaveEnabled += OnMusicActiveRequestSave;
            
            _settingsPanel.DisabledWithoutPanel += TrySave;
        }

        private void OnDisable()
        {
            _soundSlider.RequestSaveVolume -= OnSoundVolumeRequestSave;
            _soundSlider.RequestSaveEnabled -= OnSoundActiveRequestSave;
            
            _musicSlider.RequestSaveVolume -= OnMusicVolumeRequestSave;
            _musicSlider.RequestSaveEnabled -= OnMusicActiveRequestSave;

            _settingsPanel.DisabledWithoutPanel -= TrySave;
        }

        private void OnSoundVolumeRequestSave(float value)
        {
            _volumeData.SoundVolume = value;
        }
        
        private void OnSoundActiveRequestSave(bool value)
        {
            _volumeData.SoundEnable = value;
        }

        private void OnMusicVolumeRequestSave(float value)
        {
            _volumeData.MusicVolume = value;
        }

        private void OnMusicActiveRequestSave(bool value)
        {
            _volumeData.MusicEnable = value;
        }

        private void TrySave()
        {
            VolumeData tempData = VolumeSaver.Instance.GetData() ?? new VolumeData();

            bool canSave = _volumeData.MusicVolume != tempData.MusicVolume
                           || _volumeData.SoundVolume != tempData.SoundVolume
                           || _volumeData.MusicEnable != tempData.MusicEnable
                           || _volumeData.SoundEnable != tempData.SoundEnable;

            if (canSave)
            {
                VolumeSaver.Instance.SaveData(_volumeData);
            }
        }
    }
}
