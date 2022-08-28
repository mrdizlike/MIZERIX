using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public enum SlotInfo : int
    {
        Full = -1,
        FirstSlot = 0,
        SecondSlot = 1
    }

    public Sprite EmptySlotUI;
    public Slot[] slots = {null, null};
    public Button[] SellButton;
    public Image[] SlotsHeaderUI;
    public Image[] CoolDownUI;

    //������ ������ �� ������ ����
    public SlotInfo EmptySlot()
    {
        for(int i = 0;i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                return (SlotInfo)i;
            }
        }

        return SlotInfo.Full;
    }
}
