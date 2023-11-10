using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class TokenInventory : MonoBehaviour //IInventoryItem//
{
    //public InventoryWithSlot _inventory { get; set; }
    [SerializeField] private InventoryItemInfo _appleInfo;
    private UIInventorySlot[] _uislots;

    public UIInventory _uiinventory ;
    public InventoryWithSlot _inventory => _uiinventory.inventory;
    //public InventoryWithSlot _inventory => tester._inventory;
    //private UIInventoryTester tester;


    //public IInventoryItemInfo info { get; }

    //public IInventoryItemState state { get; }

    //public Type type => GetType();

    //public IInventoryItem Clone()
    //{
    //    throw new NotImplementedException();
    //}
    //public ItemForInventory(IInventoryItemInfo info)
    //{
    //    this.info = info;
    //    state = new InventoryItemState();
    //}


    private void Awake()
    {
        //var spriteR = gameObject.GetComponent<SpriteRenderer>();
        //spriteR.sprite = _appleInfo.spriteIcon; ;
    }

    // Start is called before the first frame update
    void Start()
    {
        //_uiinventory = GetComponent<UIInventory>();
        ////_inventory = GetComponent<InventoryWithSlot>();
        //_uislots = GameObject.Find("UICanvas").GetComponentsInChildren<UIInventorySlot>();
        ////Apple apple = new Apple(_appleInfo);
        ////_imageIcon.sprite = _appleInfo.spriteIcon;

        //Debug.Log($"_uiinventory : {_uiinventory.name} & {_uiinventory.inventory}");
        //Debug.Log($"_uiinventory : {_uiinventory.name} & {_uiinventory.inventory}");
        Debug.Log($"_appleInfo : {_appleInfo.name} & {_appleInfo.description}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _uiinventory = GameObject.Find("[INTERFACE]").GetComponent<UIInventory>();
        //_inventory = GetComponent<InventoryWithSlot>();
        _uislots = GameObject.Find("UICanvas").GetComponentsInChildren<UIInventorySlot>();
        //Apple apple = new Apple(_appleInfo);
        //_imageIcon.sprite = _appleInfo.spriteIcon;

        Debug.Log($"_uiinventory : {_uiinventory.name} & {_uiinventory.inventory}");

        //var wert = collision.GetComponent<ItemForInventory>();
        //wert.
        Debug.Log($"  Collider2D collision  !!!!  {collision.gameObject.name}");
        if (collision.CompareTag("Player"))//it could be enemy
        {
            FillSlots();
        }
    }

    public void FillSlots()
    {
        var allSlots = _inventory.GetAllSlots();
        //create dynamic List to delete which allready is full
        var avaibleSlots = new List<IInventorySlot>(allSlots);

        //int fillSlots = 3;
        //for (int i = 0; i < fillSlots; i++)
        //{
        //    var filledSlot = AddAppleIntoAppropriateSlot(avaibleSlots);
        //    avaibleSlots.Remove(filledSlot);
        //}

        var apple = new Apple(_appleInfo);
        apple.state.amount = 1;

        //bool notSetObject = false;
        for (int i = 0;i < avaibleSlots.Count;i++)
        {
            Debug.Log($" -------- : {_inventory.capacity} & {avaibleSlots[i]} & {apple.type}");
            var ok = _inventory.TryToAddToSlot2(this, avaibleSlots[i], apple);
            if (ok) break;
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

    //private IInventorySlot AddAppleIntoAppropriateSlot(List<IInventorySlot> slots)
    //{
    //    //var rSlotIndex = Random.Range(0, slots.Count);
    //    //var rSlot = slots[rSlotIndex];
    //    //var rCount = Random.Range(1, 4);

    //    var apple = new Apple(_appleInfo);
    //    apple.state.amount = 1;
    //    _inventory.TryToAddToSlot(this, rSlot, apple);
    //    return rSlot;
    //}

}
