using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.UI.Scripts.PausePanel
{
    public class VolumeSlider : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Button _volumeButton;
        [SerializeField] private CanvasGroup _redLine;

        private const float MinVolume = 0;
        private const float MaxVolume = 1;
        private const float AlphaRedLineDisabled = 0;
        private const float AlphaRedLineEnabled = 1;
        
        public event Action<float> RequestSaveVolume;
        public event Action<bool> RequestSaveEnabled; 

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(ChangeVolume);
            _volumeButton.onClick.AddListener(OnButtonPressed);
            TryChangeButtonView();
        }
        
        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(ChangeVolume);
            _volumeButton.onClick.RemoveListener(OnButtonPressed);
        }
        
        public void SetActive(bool value)
        {
            _redLine.alpha = value ? AlphaRedLineDisabled : AlphaRedLineEnabled;
        }

        public virtual void SetVolume(float value)
        {
            _slider.value = value;
        }
        
        private void OnButtonPressed()
        {
            if (_slider.value == MinVolume)
                SetVolumeByButton(MaxVolume, AlphaRedLineDisabled);
            else
                SetVolumeByButton(MinVolume, AlphaRedLineEnabled);
        }
        
        private void SetVolumeByButton(float volume, float alphaRedLine)
        {
            _slider.value = volume;
            ChangeVolume(volume);
            _redLine.alpha = alphaRedLine;
            RequestSaveEnabled?.Invoke(_slider.value != MinVolume);
            RequestSaveVolume?.Invoke(volume);
        }

        private void TryChangeButtonView()
        {
            _redLine.alpha = _slider.value == MinVolume ? AlphaRedLineEnabled : AlphaRedLineDisabled;
            RequestSaveEnabled?.Invoke(_slider.value != MinVolume);
        }
        
        protected virtual void ChangeVolume(float volume)
        {
            TryChangeButtonView();
            RequestSaveVolume?.Invoke(volume);
        }
    }
}