using System.Collections;
using System.Collections.Generic;
using GGJ.Inventory;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class InventoryTests
{
[Test]
    public void AddingItemsToInventory()
    {
        GameObject playerObject = new GameObject();
        var inventory = playerObject.AddComponent<PlayerInventory>();
        //inventory.MaxCapacity = 5;
        inventory.Initialize();
        var item = ScriptableObject.CreateInstance<ItemInfo>();
        
        bool addItemResult = inventory.TryAddItem(item);
        
        Assert.True(addItemResult);
        Assert.AreSame(item, inventory.Slots[0].ItemInfo);
    }

    [Test]
    public void RemovingItemsFromInventory()
    {
        GameObject playerObject = new GameObject();
        var inventory = playerObject.AddComponent<PlayerInventory>();
        //inventory.MaxCapacity = 5;
        inventory.Initialize();
        var item = ScriptableObject.CreateInstance<ItemInfo>();
        inventory.TryAddItem(item);
        
        bool removeItemResult = inventory.TryRemoveItem(item);
        
        Assert.True(removeItemResult);
        Assert.IsNull(inventory.Slots[0].ItemInfo);
    }

    [Test]
    public void MovingItemsBetweenSlots()
    {
        GameObject playerObject = new GameObject();
        var inventory = playerObject.AddComponent<PlayerInventory>();
        //inventory.MaxCapacity = 5;
        inventory.Initialize();
        var item1 = ScriptableObject.CreateInstance<ItemInfo>();
        var item2 = ScriptableObject.CreateInstance<ItemInfo>();
        inventory.TryAddItem(item1);
        inventory.TryAddItem(item2);
        
        inventory.MoveItemToCell(0, 1);
        
        Assert.AreSame(item1, inventory.Slots[1].ItemInfo);
        Assert.AreSame(item2, inventory.Slots[0].ItemInfo);
    }

    [Test]
    public void TryAddItemToFullInventory()
    {
        GameObject playerObject = new GameObject();
        var inventory = playerObject.AddComponent<PlayerInventory>();
        //inventory.MaxCapacity = 5;
        inventory.Initialize();
        
        for (int i = 0; i < inventory.Slots.Count; i++)
        {
            inventory.TryAddItem(ScriptableObject.CreateInstance<ItemInfo>());
        }
        
        var newItem = ScriptableObject.CreateInstance<ItemInfo>();
        bool addItemResult = inventory.TryAddItem(newItem);
        
        Assert.False(addItemResult);
    }

    [Test]
    public void TryRemoveItemFromEmptySlot()
    {
        GameObject playerObject = new GameObject();
        var inventory = playerObject.AddComponent<PlayerInventory>();
        //inventory.MaxCapacity = 5;
        inventory.Initialize();
        var item = ScriptableObject.CreateInstance<ItemInfo>();
        
        bool removeItemResult = inventory.TryRemoveItem(item);
        
        Assert.False(removeItemResult);
    }
}
