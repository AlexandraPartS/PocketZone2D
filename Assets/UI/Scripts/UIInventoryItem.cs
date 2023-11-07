using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIInventoryItem : UIItem
{
    [SerializeField] private UnityEngine.UI.Image _imageIcon;
    [SerializeField] public UnityEngine.UI.Text _textAmount;

    private IInventoryItem _item;

    public void Refresh(IInventorySlot slot)
    {
        if (slot.isEmpty)
        {
            Cleanup();
            return;
        }

        _item = slot.item;

        _imageIcon.sprite = _item.info.spriteIcon;
        _imageIcon.gameObject.SetActive(true);
        Debug.Log($" 3. Before text---------------");
        var textAmountEnubled = slot.amount > 1;
        _textAmount.gameObject.SetActive(textAmountEnubled);

        if (textAmountEnubled)
        {
            _textAmount.text = $"X{slot.amount.ToString()}";
        }
    }

    private void Cleanup()
    {
        _imageIcon.gameObject.SetActive(false);
        _textAmount.gameObject.SetActive(false);
    }
}
