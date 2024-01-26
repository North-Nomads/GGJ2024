using System;

namespace GGJ.Inventory
{
    public sealed class InventorySlot
    {
        private ItemInfo _itemInfo;
        
        public bool IsEmpty => _itemInfo == null;
        public ItemInfo ItemInfo => _itemInfo;

        public void AddInSlot<TItem>(TItem item) where TItem : ItemInfo
        {
            if (!IsEmpty)
            {
                throw new Exception("Can't add item to slot. It was not empty");
            }
            
            _itemInfo = item;
        }

        public void RemoveFromSlot()
        {
            if (IsEmpty)
            {
                throw new Exception("Can't remove item from slot. It was empty");
            }
            
            _itemInfo = null;
        }
    }
}
