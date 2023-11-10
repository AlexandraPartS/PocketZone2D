using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class UIInventoryTester : MonoBehaviour
{
    [SerializeField] private UIInventory _uiinventory;
    [SerializeField] public InventoryWithSlot _inventory { get; }

    private IInventoryItemInfo _appleInfo;
    private IInventoryItemInfo _pepperInfo;
    private UIInventorySlot[] _uislots;
    private int capacity = 5;

    public UIInventoryTester(InventoryItemInfo appleInfo, InventoryItemInfo pepperInfo, UIInventorySlot[] uislots)
    {
        _appleInfo = appleInfo;
        _pepperInfo = pepperInfo;
        _uislots = uislots;

        _inventory = new InventoryWithSlot(capacity);
        _inventory.OnInventoryStateChangedEvent += OnInventoryStateChanged;
    }

    //Fill Inventory for all
    public void FillSlots()
    {
        var allSlots = _inventory.GetAllSlots();
        //create dynamic List to delete which allready is full
        var avaibleSlots = new List<IInventorySlot>(allSlots);

        int fillSlots = 3;
        for (int i=0; i< fillSlots; i++)
        {
            var filledSlot = AddRandomApplesIntoRandomSlot(avaibleSlots);
            avaibleSlots.Remove(filledSlot);
            filledSlot = AddRandomPepperIntoRandomSlot(avaibleSlots);
            avaibleSlots.Remove(filledSlot);
        }

        SetupInventoryUI(_inventory);
        Debug.Log($" 2. All is fill");
    }


    private void SetupInventoryUI(InventoryWithSlot inventory)
    {
        var allSlots = inventory.GetAllSlots();
        var allSlotsCount = allSlots.Length;
        for (int i = 0; i < allSlotsCount; i++)
        {
            var slot = allSlots[i];
            var uiSlot = _uislots[i];
            uiSlot.SetSlot(slot);
            uiSlot.Refresh();
        }
    }

    private void OnInventoryStateChanged(object obj)
    {
        //will refresh every Slot
        foreach (var uiSlot in _uislots)
        {
            uiSlot.Refresh();
        }
    }

    private IInventorySlot AddRandomApplesIntoRandomSlot(List<IInventorySlot> slots)
    {
        var rSlotIndex = Random.Range(0, slots.Count);
        var rSlot = slots[rSlotIndex];
        var rCount = Random.Range(1, 4);
        var apple = new Apple(_appleInfo);
        apple.state.amount = rCount;
        _inventory.TryToAddToSlot(this, rSlot, apple);
        return rSlot;
    }

    private IInventorySlot AddRandomPepperIntoRandomSlot(List<IInventorySlot> slots)
    {
        var rSlotIndex = Random.Range(0, slots.Count);
        var rSlot = slots[rSlotIndex];
        var rCount = Random.Range(1, 4);
        var pepper = new Pepper(_pepperInfo);
        pepper.state.amount = rCount;
        _inventory.TryToAddToSlot(this, rSlot, pepper);
        return rSlot;
    }

    public void AddRandomApples()
    {
        //Create apple item
        var rnd = 1; //Random.Range(1, 5);
        Apple apple = new Apple(_appleInfo);
        apple.state.amount = rnd;

        //Get all slots
        var allSlots = _inventory.GetAllSlots();
        //var rSlotIndex = Random.Range(0, allSlots.Length);
        //var rSlot = allSlots[rSlotIndex];

        //Set item to slot
        for (int i = 0; i < allSlots.Length; i++)
        {
            Debug.Log($"    MT    {i}");
            var ok = _inventory.TryToAddToSlot2(this, allSlots[i], apple);
            if(ok) break;
        }
    }
    //void RemoveRandomApples()
    //{
    //    int rnd = Random.Range(1, 10);
    //    _inventory.Remove(this, typeof(Apple), rnd);
    //    Debug.Log($"     GetItemAmount  =  {_inventory.GetItemAmount(typeof(Apple))}");
    //}






}
