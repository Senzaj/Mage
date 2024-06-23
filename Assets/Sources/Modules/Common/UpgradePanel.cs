using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.Common
{
    public class UpgradePanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currentValue;
        [SerializeField] private TMP_Text _upgradeValue;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private Button _buyButton;
        [SerializeField] private int _price;

        private int _addPrice;
        public Button BuyButton => _buyButton;
        public int Price => _price;

        private void Awake()
        {
            ChangePriceText(_price.ToString());
        }

        public void ChangeCurrentValueText(string text)
        {
            _currentValue.text = text;
        }

        public void ChangeUpgradeValueText(string text)
        {
            _upgradeValue.text = text;
        }

        public void SetAddPrice(int value)
        {
            if (value <= 0)
                return;

            _addPrice = value;
        }

        public void AddPrice()
        {
            _price += _addPrice;
            ChangePriceText(_price.ToString());
        }

        private void ChangePriceText(string text)
        {
            _priceText.text = text;
        }
    }
}
