using System;
using Sources.Modules.Player.Scripts.MVP;
using Sources.Modules.UI.Scripts;
using Sources.Modules.YandexSDK.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.Player.Scripts.UI
{
    public class LosePanel : MonoBehaviour
    {
        [SerializeField] private YandexSdk _yandex;
        [SerializeField] private Mage _mage;
        [SerializeField] private PlayerSetup _playerSetup;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _rewardButton;
        [SerializeField] private Panel _panel;
        [SerializeField] private GameObject _parentReward;

        public event Action Rewarded;
        public event Action Restarted;

        private void OnEnable()
        {
            _mage.Died += _panel.TurnOn;

            _restartButton.onClick.AddListener(OnRestartButtonClick);
            _rewardButton.onClick.AddListener(OnRewardButtonClick);
        }

        private void OnDisable()
        {
            _mage.Died -= _panel.TurnOn;
            
            _restartButton.onClick.RemoveListener(OnRestartButtonClick);
            _rewardButton.onClick.RemoveListener(OnRewardButtonClick);
        }

        public void OnRewardedChanged(bool value)
        {
            _parentReward.SetActive(value);
        }

        private void OnRestartButtonClick()
        {
            OnRestarted();
            Restarted?.Invoke();
        }

        private void OnRewardButtonClick()
        {
            if (_yandex.IsInitialized)
                _yandex.ShowVideo(OnRewarded);
            else
                OnRewarded();
        }

        private void OnRewarded()
        {
            SetDefault();
            Rewarded?.Invoke();
        }

        private void OnRestarted()
        {
            SetDefault();
            _playerSetup.SetDefault();
            Restarted?.Invoke();
        }

        private void SetDefault()
        {
            _panel.TurnOff();
            _mage.UpdateCurrentHealth();
            _mage.SetStartPosition();
        }
    }
}