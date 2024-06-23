using System;
using Sources.Modules.Common;
using UnityEngine;

namespace Sources.Modules.Player.Scripts.MVP
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private UpgradePanel _healthPanel;
        [SerializeField] private UpgradePanel _damagePanel;
        [SerializeField] private UpgradePanel _speedPanel;
        
        public event Action<int> MaxHealthIncreasingButtonPressed;
        public event Action<int> DamageScalerIncreasingButtonPressed;
        public event Action<int> SpeedIncreasingButtonPressed;
        public event Action MaxHealthIncreasingBought;
        public event Action DamageScalerIncreasingBought;
        public event Action SpeedIncreasingBought;

        private void OnEnable()
        {
            _healthPanel.BuyButton.onClick.AddListener((() => MaxHealthIncreasingButtonPressed?.Invoke(_healthPanel.Price)));
            _damagePanel.BuyButton.onClick.AddListener((() => DamageScalerIncreasingButtonPressed?.Invoke(_damagePanel.Price)));
            _speedPanel.BuyButton.onClick.AddListener((() => SpeedIncreasingButtonPressed?.Invoke(_speedPanel.Price)));
        }

        private void OnDisable()
        {
            _healthPanel.BuyButton.onClick.RemoveListener((() => MaxHealthIncreasingButtonPressed?.Invoke(_healthPanel.Price)));
            _damagePanel.BuyButton.onClick.RemoveListener((() => DamageScalerIncreasingButtonPressed?.Invoke(_damagePanel.Price)));
            _speedPanel.BuyButton.onClick.RemoveListener((() => SpeedIncreasingButtonPressed?.Invoke(_speedPanel.Price)));
        }

        public void ChangeMaxHealthText(float maxHealth, float increase)
        {
            _healthPanel.ChangeCurrentValueText(Mathf.CeilToInt(maxHealth).ToString());
            _healthPanel.ChangeUpgradeValueText(Mathf.CeilToInt(maxHealth + increase).ToString());
        }
        
        public void ChangeDamageScalerText(float damageScaler, float increase)
        {
            _damagePanel.ChangeCurrentValueText(damageScaler.ToString("F1"));
            _damagePanel.ChangeUpgradeValueText((damageScaler + increase).ToString("F1"));
        }

        public void ChangeSpeedText(float speed, float increase)
        {
            _speedPanel.ChangeCurrentValueText(speed.ToString("F1"));
            _speedPanel.ChangeUpgradeValueText((speed + increase).ToString("F1"));
        }

        public void AddMaxHealth() => MaxHealthIncreasingBought?.Invoke();

        public void AddDamageScaler() => DamageScalerIncreasingBought?.Invoke();

        public void AddSpeed() => SpeedIncreasingBought?.Invoke();
    }
}