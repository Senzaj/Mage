using System;
using System.Collections.Generic;
using Sources.Modules.UI.Scripts;
using Sources.Modules.Weapons.Scripts;
using Sources.Modules.Weapons.Scripts.Base;
using TMPro;
using UnityEngine;

namespace Sources.Modules.Workshop.Scripts.UI
{
    public class SpellsShop : MonoBehaviour
    {
        [SerializeField] private List<SpellSlot> _spellSlots;
        [SerializeField] private int _activeSpellsLimit;
        [SerializeField] private TMP_Text _activeSpellsText;
        [SerializeField] private Color _activeSpellsAvailableColor;
        [SerializeField] private Color _activeSpellsEnoughColor;
        [SerializeField] private Panel _workShopPanel;

        private Staff _staff;
        private SpellSlotDates _slotDates;

        public event Action<int, SpellSlot> SlotBuyButtonPressed;
        
        public void Init(Staff staff)
        {
            _staff = staff;
            
            _slotDates = WorkShopSaver.Instance.GetData() ?? new SpellSlotDates
            {
                ActiveSpells = new List<SpellType>(),
                SlotDates = new List<SpellType>()
            };

            foreach (SpellSlot slot in _spellSlots)
            {
                if (_slotDates.SlotDates.Contains(slot.SpellType))
                {
                    slot.BuySpell();
                    
                    if (_slotDates.ActiveSpells.Contains(slot.SpellType))
                    {
                        EquipSpell(slot.SpellType, slot);
                    }
                }
            }

            foreach (SpellSlot slot in _spellSlots)
            {
                if (slot.IsEquipped && staff.ActiveSpellsCount < _activeSpellsLimit)
                {
                    if (_slotDates.SlotDates.Contains(slot.SpellType) == false)
                    {
                        _slotDates.SlotDates.Add(slot.SpellType);
                    }

                    if (_slotDates.ActiveSpells.Contains(slot.SpellType) == false)
                    {
                        _slotDates.ActiveSpells.Add(slot.SpellType);
                    }
                    
                    _staff.AddSpellCaster(slot.SpellType);
                }
                else if(slot.IsEquipped && _slotDates.ActiveSpells.Contains(slot.SpellType) == false)
                {
                    slot.UnEquipSpell();
                }
            }

            Save();
            
            CheckSpellsLimit();
        }
        private void OnEnable()
        {
            _workShopPanel.DisabledWithoutPanel += TrySave;
            
            foreach (SpellSlot slot in _spellSlots)
            {
                slot.BuyButtonPressed += OnSlotBuyButtonPressed;
                slot.EquipButtonPressed += OnEquipButtonPressed;
            }
        }

        private void OnDisable()
        {
            _workShopPanel.DisabledWithoutPanel -= TrySave;
            
            foreach (SpellSlot slot in _spellSlots)
            {
                slot.BuyButtonPressed -= OnSlotBuyButtonPressed;
                slot.EquipButtonPressed -= OnEquipButtonPressed;
            }
        }

        private void OnEquipButtonPressed(SpellType spellType, SpellSlot spellSlot)
        {
            if (spellSlot.IsEquipped == false)
            {
                EquipSpell(spellType, spellSlot);
                AddActiveSpell(spellType);
            }
            else
            {
                UnEquipSpell(spellType, spellSlot);
                RemoveActiveSpell(spellType);
            }
        }

        private void EquipSpell(SpellType spellType, SpellSlot spellSlot)
        {
            _staff.AddSpellCaster(spellType);
            spellSlot.EquipSpell();
            CheckSpellsLimit();
        }

        private void UnEquipSpell(SpellType spellType, SpellSlot spellSlot)
        {
            _staff.RemoveSpellCaster(spellType);
            spellSlot.UnEquipSpell();

            CheckSpellsLimit();
        }
        
        public void BuySpell(SpellSlot spellSlot)
        {
            foreach (SpellSlot slot in _spellSlots)
            {
                if (slot == spellSlot)
                {
                    spellSlot.BuySpell();

                    _slotDates.SlotDates.Add(spellSlot.SpellType);
                    
                    if (_staff.ActiveSpellsCount < _activeSpellsLimit)
                    {
                        EquipSpell(spellSlot.SpellType, spellSlot);
                        AddActiveSpell(spellSlot.SpellType);
                    }
                    
                    Save();
                }
            }
        }

        private void TrySave()
        {
            SpellSlotDates workShopSaverData = WorkShopSaver.Instance.GetData() ?? new SpellSlotDates
            {
                ActiveSpells = new List<SpellType>(),
                SlotDates = new List<SpellType>()
            };
            
            bool canSave = _slotDates.ActiveSpells.Count != workShopSaverData.ActiveSpells.Count;

            if (canSave == false)
            {
                if(workShopSaverData.ActiveSpells.Count > 0)
                {
                    foreach (var spellType in workShopSaverData.ActiveSpells)
                    {
                        if (_slotDates.ActiveSpells.Contains(spellType) == false)
                        {
                            canSave = true;
                            break;
                        }
                    }
                }
                else if (_slotDates.ActiveSpells.Count > 0)
                {
                    canSave = true;
                }
            }

            if (canSave == false)
            {
                foreach (var spellType in _slotDates.SlotDates)
                {
                    if (workShopSaverData.SlotDates.Contains(spellType) == false)
                    {
                        canSave = true;
                        break;
                    }
                }
            }

            if (canSave)
                Save();
        }

        private void Save()
        {
            WorkShopSaver.Instance.SaveData(_slotDates);
        }

        private void CheckSpellsLimit()
        {
            if (_staff.ActiveSpellsCount < _activeSpellsLimit)
            {
                foreach (SpellSlot slot in _spellSlots)
                    slot.EnableEquipButton();

                _activeSpellsText.color = _activeSpellsAvailableColor;
            }
            else
            {
                foreach (SpellSlot slot in _spellSlots)
                {
                    if (slot.IsEquipped)
                        slot.EnableEquipButton();
                    else
                        slot.DisableEquipButton();

                    _activeSpellsText.color = _activeSpellsEnoughColor;
                }
            }
            
            RewriteActiveSpellsLimit(_staff.ActiveSpellsCount);
        }

        private void RewriteActiveSpellsLimit(int currentActiveSpells)
        {
            _activeSpellsText.text = (currentActiveSpells + "/" + _activeSpellsLimit);
        }

        private void AddActiveSpell(SpellType spell)
        {
            _slotDates.ActiveSpells.Add(spell);
        }

        private void RemoveActiveSpell(SpellType spell)
        {
            _slotDates.ActiveSpells.Remove(spell);
        }

        private void OnSlotBuyButtonPressed(int price, SpellSlot slot) => SlotBuyButtonPressed?.Invoke(price, slot);
    }
}
