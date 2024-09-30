using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotHelper
{
    public static void SwapSlots(Slot slot1, Slot slot2)
    {
        InventorySlotSO temp = slot1.SlotSO;
        slot1.SetSlotSO(slot2.SlotSO);
        slot2.SetSlotSO(temp);
    }

    public static void MergeSlots(Slot slot1, Slot slot2)
    {
        // if slot1 is empty
        if (slot1.IsEmpty)
        {
            slot1.SetItem(slot2.SlotItem, slot2.Amount);
            slot2.EmptySlot();
            return;
        }

        // if slot1 have item, calculate total amount and set accordingly
        int totalItem = slot1.Amount + slot2.Amount;

        if (totalItem <= slot1.MaxStack) // if all item can be store in 1 slot
        {
            int amount = slot2.Amount;
            slot1.AddItem(amount);
            slot2.EmptySlot();
        }
        else
        {
            totalItem = slot1.AddItem(slot2.Amount); // add item to slot1 and keep the remainder
            slot2.TakeItem(slot2.Amount - totalItem); // update slot2 amount
        }
    }

    public static bool HaveSameItem(Slot slot1, Slot slot2)
    {
        if (slot1.SlotItem == null || slot2.SlotItem == null) return false;

        return slot1.SlotItem.ItemIndex == slot2.SlotItem.ItemIndex;
    }

    public static bool HaveSameItem(Slot slot, ItemIndex index)
    {
        if (slot.SlotItem == null) return false;

        return slot.SlotItem.ItemIndex == index;
    }
}
