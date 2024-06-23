using System;
using Sources.Modules.Common;
using TMPro;
using UnityEngine;

namespace Sources.Modules.Wallet.Scripts.MVP
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private UpgradePanel _coinMultiplierPanel;
        [SerializeField] private TMP_Text _inGameText;
        [SerializeField] private TMP_Text _workshopText;
        
        public event Action<int> CoinIncreasedButtonPressed;

        public UpgradePanel CoinMultiplierPanel => _coinMultiplierPanel;

        private void OnEnable()
        {
            _coinMultiplierPanel.BuyButton.onClick.AddListener((() => CoinIncreasedButtonPressed?.Invoke(_coinMultiplierPanel.Price)));
        }

        private void OnDisable()
        {
            _coinMultiplierPanel.BuyButton.onClick.RemoveListener((() => CoinIncreasedButtonPressed?.Invoke(_coinMultiplierPanel.Price)));
        }

        public void ChangeCoinText(int coin)
        {
            _inGameText.text = coin.ToString();
            _workshopText.text = coin.ToString();
        }
        
        public void ChangeCoinIncreaseText(int multiplier, int increase)
        {
            _coinMultiplierPanel.ChangeCurrentValueText(multiplier.ToString());
            _coinMultiplierPanel.ChangeUpgradeValueText((multiplier + increase).ToString());
        }
    }
}
