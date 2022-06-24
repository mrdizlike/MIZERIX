using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : ScriptableObject
{
    public Item item;
    public ActiveItem Aitem;
    public KeyCode Key;
    public int SlotIndex;

    public Slot(Item item, KeyCode Key)
    {
        this.item = item;
        this.Key = Key;
    }
}
