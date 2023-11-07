using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventorySlot
{
    bool isEmpty { get; }
    bool isFull { get; }
    int capacity { get; }
    IInventoryItem item { get; }
    Type itemType { get; }
    int amount { get; }
    void SetItem(IInventoryItem item);
    void Clear();
}
