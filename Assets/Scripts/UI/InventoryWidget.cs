using System;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Behaviour = Core.Behaviour;

namespace UI
{
    public class InventoryWidget : Behaviour,
        ISlotSelector
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private SlotWidget[] _slotWidgets;

        [SerializeField] private TextMeshProUGUI _totalWeightText;
        
        [SerializeField] private Button _dropButton;

        private ISelectableSlot _selectedSlot;

        private IPlayableCharacter _playableCharacter;

        public void Construct(IPlayableCharacter playableCharacter)
        {
            var slots = playableCharacter.Inventory.Get();

            for (var i = 0; i < slots.Length; i++)
            {
                _slotWidgets[i].Construct(slots[i], this);
            }
            
            _dropButton.onClick.AddListener(DropSelectedItem);

            playableCharacter.CriticalWeightReached += OnCriticalWeightReached;
            playableCharacter.CriticalWeightRelieved += OnCriticalWeightRelieved;
            playableCharacter.Inventory.TotalWeightChanged += OnTotalWeightChanged;

            _playableCharacter = playableCharacter;
            
            UpdateTotalWeightText(_playableCharacter.Inventory.TotalWeight);
        }

        private void UpdateTotalWeightText(float value)
        {
            var roundedWeight = Math.Round(value, 2);

            _totalWeightText.text = $"{roundedWeight}/{_playableCharacter.CriticalWeight}kg";
        }

        private void OnCriticalWeightRelieved()
        {
            _totalWeightText.color = Color.gray;
        }

        private void OnCriticalWeightReached()
        {
            _totalWeightText.color = Color.red;
        }
        
        private void OnTotalWeightChanged(float value)
        {
            UpdateTotalWeightText(value);
        }

        private void DropSelectedItem()
        {
            if (_selectedSlot == null)
            {
                return;
            }

            var selectedSlotTarget = _selectedSlot.Target;

            if(!_playableCharacter.DropItem(selectedSlotTarget.AssignedItem, 1, true, out _))
            {
                return;
            }
            
            Deselect();
        }
        
        public void Select(ISelectableSlot slot)
        {
            if (_selectedSlot != null)
            {
                _selectedSlot.SendOnDeselected();
            }
            else
            {
                _dropButton.interactable = true;
            }
            
            _selectedSlot = slot;
        }

        public void Deselect()
        {
            if (_selectedSlot == null)
            {
                return;
            }

            _dropButton.interactable = false;
            
            _selectedSlot.SendOnDeselected();
            _selectedSlot = null;
        }

        public void Show()
        {
            _panel.Activate();
        }

        public void Hide()
        {
            Deselect();
            
            _panel.Deactivate();
        }

        public void Clear()
        {
            foreach (var slotWidget in _slotWidgets)
            {
                slotWidget.Clear();
            }
            
            _dropButton.onClick.RemoveListener(DropSelectedItem);

            _playableCharacter.CriticalWeightRelieved -= OnCriticalWeightRelieved;
            _playableCharacter.CriticalWeightReached -= OnCriticalWeightReached;
            _playableCharacter.Inventory.TotalWeightChanged -= OnTotalWeightChanged;

            _playableCharacter = null;
        }
    }
}