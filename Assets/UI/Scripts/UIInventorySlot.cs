using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInventorySlot : UISlot
{
    private UIInventory _uiinventory;
    private IInventorySlot _slot;
    public UIInventoryItem _item;

    public void SetSlot(IInventorySlot newSlot)
    {
        _slot = newSlot;
    }

    private void Awake()
    {
        _uiinventory = this.GetComponentInParent<UIInventory>();
    }

    //public override void OnDrop(PointerEventData eventData)
    //{
    //    //Take old UI elements
    //    var otherItemUI = eventData.pointerDrag.GetComponent<UIInventoryItem>();//take a script component
    //    var otherSlotUI = otherItemUI.GetComponentInParent<UIInventorySlot>();

    //    var currentSlotUI = this;

    //    //Take abstract data of elements
    //    var toSlot = this._slot;
    //    var fromSlot = otherSlotUI._slot;

    //    var inventory = _uiinventory.inventory;
    //    inventory.TransitFromSlotToSlot(this, fromSlot, toSlot);

    //    Refresh();
    //    otherSlotUI.Refresh();
    //}

    public void Refresh()
    {
        if(_slot != null)
        {
            _item.Refresh(_slot);
        }

    }
}
