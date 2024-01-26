using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ.Inventory
{
    public sealed class PlayerInventory : MonoBehaviour
    {
        [SerializeField, Min(1)] private int maxCapacity;
        
        private InventorySlot[] _slots;

        public int MaxCapacity
        {
            get => maxCapacity;
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException("Capacity can't be below zero");
                maxCapacity = value;
            }
        }
        public IReadOnlyList<InventorySlot> Slots => _slots;

        public void Initialize()
        {
            _slots = new InventorySlot[maxCapacity];

            for (int cellIndex = 0; cellIndex < maxCapacity; cellIndex++)
            {
                _slots[cellIndex] = new InventorySlot();
            }
        }
        
        public bool TryAddItem<TItem>(TItem item) where TItem : ItemInfo
        {
            foreach (InventorySlot slot in _slots)
            {
                if (slot.IsEmpty)
                {
                    AddItem(item, slot);
                    return true;
                }
            }

            return false;
        }

        public bool TryRemoveItem<TItem>(TItem item) where TItem : ItemInfo
        {
            foreach (InventorySlot slot in _slots)
            {
                if (slot.ItemInfo == item)
                {
                    RemoveItem(item, slot);
                    return true;
                }
            }

            return false;
        }
        
        public void MoveItemToCell(int draggedSlotIndex, int droppedSlotIndex)
        {
            InventorySlot draggedSlot = _slots[draggedSlotIndex];
            InventorySlot droppedSlot = _slots[droppedSlotIndex];

            InventorySlot tempSlot = new InventorySlot();
            AddItem(draggedSlot.ItemInfo, tempSlot);
            RemoveItem(draggedSlot.ItemInfo, draggedSlot);
            AddItem(droppedSlot.ItemInfo, draggedSlot);
            RemoveItem(droppedSlot.ItemInfo, droppedSlot);
            AddItem(tempSlot.ItemInfo, droppedSlot);
        }

        private void Awake() => Initialize();
        
        private void AddItem<TItem>(TItem item, InventorySlot slot) where TItem : ItemInfo
        {
            if (!slot.IsEmpty)
            {
                throw new Exception("This slot is not empty. Can't add item.");
            }

            slot.AddInSlot(item);
        }
        
        private void RemoveItem<TItem>(TItem item, InventorySlot slot) where TItem : ItemInfo
        {
            if (slot.IsEmpty)
            {
                throw new Exception("Can't remove item from this slot");
            }

            slot.RemoveFromSlot();
        }
    }
}