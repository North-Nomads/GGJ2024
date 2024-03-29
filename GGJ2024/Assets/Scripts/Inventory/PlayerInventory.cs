using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GGJ.Inventory.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GGJ.Inventory
{
    public sealed class PlayerInventory : MonoBehaviour
    {
        [SerializeField] private PlayerInventoryUI view;
        [SerializeField] private InventoryGrid inventoryGrid;
        
        [Header("Only for test")]
        [SerializeField] private List<ItemInfo> testItem;
        [SerializeField] private int testItemCount;
        
        private InventorySlot[] _slots;

        public InventoryGrid InventoryGrid => inventoryGrid;
        public int MaxCapacity => inventoryGrid.Width * inventoryGrid.Height;
        public IReadOnlyList<InventorySlot> Slots => _slots;
        public event EventHandler<ItemInfo> OnPlayerInventoryUpdated = delegate { };

        public void Initialize()
        {
            _slots = new InventorySlot[MaxCapacity];

            for (int cellIndex = 0; cellIndex < MaxCapacity; cellIndex++)
            {
                _slots[cellIndex] = new InventorySlot();
            }
            
            view.Initialize(this);

            for (int i = 0; i < testItemCount; i++)
            {
                foreach (ItemInfo item in testItem)
                {
                    TryAddItem(item);
                }
            }
            StartCoroutine(test());
        }

        public IEnumerator test()
        {
            if (testItem.Count == 0)
                yield break;

            while (true)
            {
                yield return new WaitForSeconds(3);
                TryAddItem(testItem[0]);
            }
        }
        
        public void OnInventoryOpenClose(InputAction.CallbackContext callbackContext) 
        {
            if (!callbackContext.performed)
                return;
            Debug.Log(callbackContext.phase);
               
            view.HandleInventoryView();
            
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

        public bool TryRemoveItem<TItem>(TItem item, int amount) where TItem : ItemInfo
        {
            int rightItemCount = _slots.Count(x => x.ItemInfo == item);
            if (rightItemCount >= amount)
            {
                for (int i = 0; i < amount; i++)
                {
                    TryRemoveItem(item);
                }
                return true;
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
            OnPlayerInventoryUpdated(this, item);
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