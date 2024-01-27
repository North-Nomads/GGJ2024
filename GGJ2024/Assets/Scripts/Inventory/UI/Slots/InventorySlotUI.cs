using System;
using GGJ.Inventory.CustomEventArgs;
using GGJ.UI.Masks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GGJ.Inventory.UI.Slots
{
    public class InventorySlotUI : SlotUI
    {
        [Header("Mask prefabs")]
        [SerializeField] private HoveringMask hoveringMask;
        [SerializeField] private SelectingMask selectingMask;

        public void Initialize(InventorySlot slot)
        {
            slot.OnSlotStatusUpdate += OnSlotUpdate;

            if (!slot.IsEmpty)
            {
                UpdateSlotValues(new InventoryEventArgs(slot.ItemInfo));
            }
        }
        
        public void Select()
        {
            selectingMask.Select();
            hoveringMask.UpdateFade(0f);
            hoveringMask.gameObject.SetActive(false);
        }

        public void Deselect()
        {
            selectingMask.Deselect();
            hoveringMask.UpdateFade(0f);
            hoveringMask.gameObject.SetActive(true);
        }

        private void OnSlotUpdate(object sender, InventoryEventArgs args)
        {
            UpdateSlotValues(args);
        }

        private void UpdateSlotValues(InventoryEventArgs args)
        {
            IsEmpty = args.ItemInfo == null;
            if (IsEmpty) return;
            
            SlotIcon = args.ItemInfo.Icon;
        }
    }
}