using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryWithSlot : IInventory
{
    public event Action<object, IInventoryItem, int> OnInventoryItemAddedEvent;
    public event Action<object, Type, int> OnInventoryItemRemoveEvent;
    public event Action<object> OnInventoryStateChangedEvent;

    private List<IInventorySlot> _slots;
    public int capacity { get; set; }

    public InventoryWithSlot(int capacity)
    {
        this.capacity = capacity;
        _slots = new List<IInventorySlot>(capacity);
        for (int i = 0; i < capacity; i++)
        {
            _slots.Add(new InventorySlot());
        }
    }


    public void TransitFromSlotToSlot(object sender, IInventorySlot fromSlot, IInventorySlot toSlot)
    {
        if (fromSlot.isEmpty) return;

        if(toSlot.isFull) return;

        if (!toSlot.isEmpty && fromSlot.itemType != toSlot.itemType) return;

        var slotCapacity = fromSlot.capacity;
        var fits = fromSlot.amount + toSlot.amount <= slotCapacity;
        var amountToAdd = fits
                         ? fromSlot.amount
                         : slotCapacity - toSlot.amount;
        var amountLeft = fromSlot.amount - amountToAdd;

        if (toSlot.isEmpty)
        {
            toSlot.SetItem(fromSlot.item);
            fromSlot.Clear();
            OnInventoryStateChangedEvent?.Invoke(sender);
        }

        toSlot.item.state.amount += amountToAdd;
        if (fits)
            fromSlot.Clear();
        else
        {
            fromSlot.item.state.amount = amountLeft;
        }
        OnInventoryStateChangedEvent?.Invoke(sender);
    }


    public bool ifFull => _slots.All(slot => slot.isFull);

    public IInventoryItem GetItem(Type itemType) => _slots.Find(slot => slot.itemType == itemType).item;
    public IInventoryItem[] GetAllItems()
    {
        List<IInventoryItem> allItems = new List<IInventoryItem>();
        foreach (var slot in _slots)
        {
            if (!slot.isEmpty)
                allItems.Add(slot.item);
        }
        return allItems.ToArray();
    }
    //List of uniq type Items
    public IInventorySlot[] GetAllSlots()
    {
        return _slots.ToArray();
    }//

    //List of uniq type Items
    public IInventoryItem[] GetAllItem(Type itemType)
    {
        List<IInventoryItem> items = new List<IInventoryItem>();
        var slotsOfType = _slots.FindAll(slot => !slot.isEmpty && slot.itemType == itemType);//.ToArray();
        foreach (var slot in slotsOfType)
        {
            items.Add(slot.item);
        }
        return items.ToArray();
    }//
    public IInventoryItem[] GetAllEquippedItem()
    {
        List<IInventoryItem> items = new List<IInventoryItem>();
        var slotsOfType = _slots.FindAll(slot => !slot.isEmpty && slot.item.state.isEquipped);//.ToArray();
        foreach (var slot in slotsOfType)
        {
            items.Add(slot.item);
        }
        return items.ToArray();
    }//

    public int GetItemAmount(Type itemType)
    {
        int summ = 0;
        var slotsOfType = _slots.FindAll(slot => !slot.isEmpty && slot.itemType == itemType);//.ToArray();
        foreach (var slot in slotsOfType)
        {
            summ += slot.amount;
        }
        return summ;
    }//

    public bool TryToAdd(object sender, IInventoryItem item)
    {
        var slotWithSameItemButNotEmpty = _slots.Find(slot => !slot.isEmpty && slot.itemType == item.type && !slot.isFull);
        if (slotWithSameItemButNotEmpty != null)
        {
            return TryToAddToSlot(sender, slotWithSameItemButNotEmpty, item);
        }
        //Try to create Slot of Type
        IInventorySlot emptySlot = _slots.Find(slot => slot.isEmpty);
        if (emptySlot != null)
        {
            return TryToAddToSlot(sender, emptySlot, item);
        }

        Console.WriteLine($"Cannot add item ({item.type}), amount: {item.state.amount}, because there is no place for that");

        return false;
    }//

    public bool TryToAddToSlot2(object sender, IInventorySlot slot, IInventoryItem item)
    {
        //if (slot.isFull) return false;
        if (slot.isEmpty) return TryToAddToSlot(sender, slot, item);

        bool myType = slot.itemType == item.type;
        if (!myType)
        {
            return false;
        }
        Debug.Log($"                 3      ");
        return TryToAddToSlot(sender, slot, item);
    }

    //public bool TryToAddToSlot2(object sender, IInventorySlot slot, IInventoryItem item)
    //{

    //}

    public bool TryToAddToSlot(object sender, IInventorySlot slot, IInventoryItem item)
    {
        //var fits = slot.amount + item.state.amount <= item.info.maxItemsInInventorySlot;
        ////add items to slot
        //var amountToAdd = fits
        //                ? item.state.amount
        //                : item.info.maxItemsInInventorySlot - slot.amount;
        //var amountLeft = item.state.amount - amountToAdd;

        //var clonedItem = item.Clone();
        //clonedItem.state.amount = amountToAdd;

        if (slot.isEmpty)
        {
            slot.SetItem(item);
        }
        else
        {
            slot.item.state.amount ++;
        }

        //Console.WriteLine($"Item added to inventory. ItemType: {item.type}, amount: {amountToAdd}");
        OnInventoryItemAddedEvent?.Invoke(sender, item, 1);
        OnInventoryStateChangedEvent?.Invoke(sender);
        return true;
        //if (amountLeft <= 0)
        //{
        //    return true;
        //}

        //item.state.amount = amountLeft;
        //return TryToAdd(sender, item);
    }//
    //public bool TryToAddToSlot(object sender, IInventorySlot slot, IInventoryItem item)
    //{
    //    var fits = slot.amount + item.state.amount <= item.info.maxItemsInInventorySlot;
    //    //add items to slot
    //    var amountToAdd = fits
    //                    ? item.state.amount
    //                    : item.info.maxItemsInInventorySlot - slot.amount;
    //    var amountLeft = item.state.amount - amountToAdd;

    //    var clonedItem = item.Clone();
    //    clonedItem.state.amount = amountToAdd;

    //    if (slot.isEmpty)
    //    {
    //        Console.WriteLine($"Fill new Slot. Slot: {slot.GetHashCode()}, add: {clonedItem.state.amount}");
    //        slot.SetItem(clonedItem);
    //    }
    //    else
    //    {
    //        slot.item.state.amount += amountToAdd;
    //    }

    //    Console.WriteLine($"Item added to inventory. ItemType: {item.type}, amount: {amountToAdd}");
    //    OnInventoryItemAddedEvent?.Invoke(sender, item, amountToAdd);
    //    OnInventoryStateChangedEvent?.Invoke(sender);

    //    if (amountLeft <= 0)
    //    {
    //        return true;
    //    }

    //    item.state.amount = amountLeft;
    //    return TryToAdd(sender, item);
    //}//

    public void Remove(object sender, Type itemType, int amount = 1)
    {
        var slotsWithItem = _slots.FindAll(slot => !slot.isEmpty && slot.itemType == itemType).ToArray();
        if (slotsWithItem.Length == 0)
        {
            return;
        }

        var amountToRemove = amount;
        for (int i = slotsWithItem.Length - 1; i >= 0; i--)
        {
            var slot = slotsWithItem[i];
            if (slot.amount >= amountToRemove)
            {
                slot.item.state.amount -= amountToRemove;

                if (slot.amount <= 0)
                {
                    slot.Clear();
                }

                Console.WriteLine($"Item removed from inventory. ItemType: {itemType}, amount: {amountToRemove}, from slot {slot.GetHashCode()}");
                OnInventoryItemRemoveEvent?.Invoke(sender, itemType, amountToRemove);
                OnInventoryStateChangedEvent?.Invoke(sender);
                break;
            }

            var amountRemoved = slot.amount;
            amountToRemove -= slot.amount;
            slot.Clear();
            Console.WriteLine($"Item removed from inventory. ItemType: {itemType}, amount: {amountRemoved}");
            OnInventoryItemRemoveEvent?.Invoke(sender, itemType, amountRemoved);
            OnInventoryStateChangedEvent?.Invoke(sender);
        }
    }

    //public 

    public bool HasItem(Type type, out IInventoryItem item)
    {
        item = GetItem(type);
        return item != null;
    }
}
