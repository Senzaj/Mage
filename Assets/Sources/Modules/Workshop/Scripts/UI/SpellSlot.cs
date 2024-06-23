using System;
using Lean.Localization;
using Sources.Modules.Weapons.Scripts;
using Sources.Modules.Weapons.Scripts.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.Workshop.Scripts.UI
{
    public class SpellSlot : MonoBehaviour
    {
        [SerializeField] private int _price;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private TMP_Text _damageText;
        [SerializeField] private TMP_Text _equipText;
        [SerializeField] private Color _equippedColor;
        [SerializeField] private Color _unequippedColor;
        [SerializeField] private bool _isBought = false;
        [SerializeField] private bool _isEquipped = false;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _equipButton;
        [SerializeField] private Projectile _spellProjectile;
        [SerializeField] private SpellType _spellCasterType;

        private const string EquippedString = "Equipped";
        private const string UnequippedString = "Unequipped";

        public event Action<int, SpellSlot> BuyButtonPressed;
        public event Action<SpellType, SpellSlot> EquipButtonPressed;

        public SpellType SpellType => _spellCasterType;
        public bool IsEquipped => _isEquipped;
        
        private void Awake()
        {
            _priceText.text = _price.ToString();
            _damageText.text = _spellProjectile.BaseDamage.ToString();
        }

        private void Start()
        {
            TryChangeEquipStatus();
            TryHideBuyButton();
        }

        private void OnEnable()
        {
            _buyButton.onClick.AddListener(OnBuyButtonPressed);
            _equipButton.onClick.AddListener(OnEquipButtonPressed);
        }
        
        private void OnDisable()
        {
            _buyButton.onClick.RemoveListener(OnBuyButtonPressed);
            _equipButton.onClick.RemoveListener(OnEquipButtonPressed);
        }

        public void BuySpell()
        {
            _isBought = true;
            TryHideBuyButton();
        }
        
        public void EquipSpell()
        {
            _isEquipped = true;
            TryChangeEquipStatus();
        }
        
        public void UnEquipSpell()
        {
            _isEquipped = false;
            TryChangeEquipStatus();
        }
        
        public void DisableEquipButton() => _equipButton.interactable = false;

        public void EnableEquipButton() => _equipButton.interactable = true;

        private void OnBuyButtonPressed() => BuyButtonPressed?.Invoke(_price, this);

        private void OnEquipButtonPressed() => EquipButtonPressed?.Invoke(_spellCasterType, this);

        private void TryChangeEquipStatus()
        {
            if (IsEquipped)
            {
                _equipText.text = LeanLocalization.GetTranslationText(EquippedString);
                _equipButton.GetComponent<Image>().color = _equippedColor;
            }
            else
            {
                _equipText.text = LeanLocalization.GetTranslationText(UnequippedString);
                _equipButton.GetComponent<Image>().color = _unequippedColor;
            }
                
        }
        
        private void TryHideBuyButton()
        {
            CanvasGroup buyButtonCanvas = _buyButton.GetComponent<CanvasGroup>();
            CanvasGroup equipButtonCanvas = _equipButton.GetComponent<CanvasGroup>();

            if (_isBought)
            {
                HideCanvas(buyButtonCanvas);
                ShowCanvas(equipButtonCanvas);
            }
            else
            {
                HideCanvas(equipButtonCanvas);
                ShowCanvas(buyButtonCanvas);
            }
        }

        private void HideCanvas(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }

        private void ShowCanvas(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }
    }
}
