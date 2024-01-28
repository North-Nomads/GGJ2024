using System;
using GGJ.Inventory.UI.Slots;

namespace GGJ.Inventory.CustomEventArgs
{
    public sealed class DroppedSlotEventArgs : EventArgs
    {
        public InventorySlotUI DraggedSlot { get; private set; }
        public InventorySlotUI DroppedSlot { get; private set; }

        public DroppedSlotEventArgs(InventorySlotUI draggedSlot, InventorySlotUI droppedSlot)
        {
            DraggedSlot = draggedSlot;
            DroppedSlot = droppedSlot;
        }
    }
}