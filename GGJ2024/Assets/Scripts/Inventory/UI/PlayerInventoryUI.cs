using System;
using System.Linq;
using GGJ.Inventory.CustomEventArgs;
using GGJ.Inventory.UI.Slots;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GGJ.Inventory.UI
{
    public class PlayerInventoryUI : MonoBehaviour
    {
        [SerializeField] private InventorySlotUI slotUIPrefab;
        [SerializeField] private GameObject slotUIContainer;
        [SerializeField] private HintWindow hintWindow;

        private PlayerInventory _playerInventory;
        private InventorySlotUI[] _uiSlots;
        private InventorySlotUI _selectedSlot;

        public bool IsActive => gameObject.activeInHierarchy;

        private InventorySlotUI SelectedSlot
        {
            get => _selectedSlot;
            set
            {
                _selectedSlot = value;
                DeselectAll();
                _selectedSlot.Select();

                UpdateHintWindow(value);
            }
        }

        public void Initialize(PlayerInventory playerInventory)
        {
            _playerInventory = playerInventory;
            _uiSlots = new InventorySlotUI[_playerInventory.MaxCapacity];

            for (int cellIndex = 0; cellIndex < _playerInventory.MaxCapacity; cellIndex++)
            {
                InventorySlotUI slotUI = Instantiate(slotUIPrefab, slotUIContainer.transform, false);
                _uiSlots[cellIndex] = slotUI;
                
                InventorySlot slot = _playerInventory.Slots[cellIndex];
                _uiSlots[cellIndex].Initialize(slot);
                
                slotUI.SlotSelected += OnSelectSlot;
                slotUI.SlotIsEmpty += OnEmptySlot;
            }
        }

        public void HandleInventoryView()
        {
            gameObject.SetActive(!IsActive);
        }

        private void OnEnable()
        {
            SelectFirstNotEmpty();
        }

        private void OnDisable()
        {
            DeselectAll();
        }

        private void OnEmptySlot()
        {
            if (SelectedSlot == null || !SelectedSlot.IsEmpty) return;
            UpdateHintWindow(SelectedSlot);
            SelectNextNotEmpty();
        }
        
        private void DeselectAll() => Array.ForEach(_uiSlots, e => e.Deselect());

        private void SelectNextNotEmpty()
        {
            int selectedSlotIndex = Array.IndexOf(_uiSlots, SelectedSlot);

            if (selectedSlotIndex < 0)
            {
                hintWindow.HideWindowComponents(true);
                return;
            }

            int nextStepValue = -1;
            for (int i = 0; i < _uiSlots.Length; i++)
            {
                if (selectedSlotIndex == 0)
                {
                    nextStepValue *= -1;
                }

                selectedSlotIndex += nextStepValue;

                if (_uiSlots[selectedSlotIndex % _uiSlots.Length].IsEmpty) continue;

                SelectedSlot = _uiSlots[selectedSlotIndex % _uiSlots.Length];
                return;
            }
        }

        private void SelectFirstNotEmpty()
        {
            InventorySlotUI slotUI = Array.Find(_uiSlots, e => !e.IsEmpty);
            
            if (slotUI == null)
            {
                hintWindow.HideWindowComponents(true);
                return;
            }
            SelectedSlot = slotUI;
        }

        private void OnSelectSlot(object sender, EventArgs args)
        {
            if (_uiSlots.Any(slot => slot == (InventorySlotUI)sender))
            {
                SelectedSlot = sender as InventorySlotUI;
            }
        }

        private void UpdateHintWindow(InventorySlotUI slotUI)
        {
            if (slotUI == null || slotUI.IsEmpty)
            {
                hintWindow.HideWindowComponents(true);
                return;
            }
            int slotIndex = Array.IndexOf(_uiSlots, slotUI);
            InventorySlot slot = _playerInventory.Slots[slotIndex];
                
            hintWindow.UpdateValues(slot.ItemInfo.Icon, slot.ItemInfo.Title, slot.ItemInfo.Description);
        }
    }
}