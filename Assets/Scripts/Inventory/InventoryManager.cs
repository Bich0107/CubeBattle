using System;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] Item[] items;
    [SerializeField] Slot[] slots;
    [SerializeField] InventorySlotSO[] slotsSO;

    void Awake()
    {
        Setup();
    }

    void Setup()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetSlotSO(slotsSO[i]);
        }
    }

    public int AddItem(ItemIndex _itemIndex, int _amount)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            // if this slot is empty
            if (slots[i].SlotItem == null)
            {
                // set item to this slot and store the overflow amount
                _amount = slots[i].SetItem(items[(int)_itemIndex], _amount);
            }
            else if (SlotHelper.HaveSameItem(slots[i], _itemIndex))
            {
                // add item to this slot and store the overflow amount
                _amount = slots[i].AddItem(_amount);
            }

            // return when there is nothing left to add
            if (_amount <= 0) return 0;
        }

        // return the amount that can't be put in inventory
        return _amount;
    }

    public void SortItems()
    {
        StackItems();
        SortByItemType();
    }

    void StackItems()
    {
        if (IsEmpty()) return;

        int index1 = 0;
        int index2 = 1;

        do
        {
            // check if the slot is empty
            if (slots[index1].IsEmpty)
            {
                // find the first slot with item
                while (slots[index2].IsEmpty) index2++;

                SlotHelper.MergeSlots(slots[index1], slots[index2]);
            }

            // loop until finding a slot with empty space or reaching the second to last index of the array
            while (slots[index1].IsFull())
            {
                if (index1 == slots.Length - 2) return;
                index1++;
                index2++;
            }

            // loop until finding a slot have the same item as slot1 or reaching the end of the array
            while (!SlotHelper.HaveSameItem(slots[index1], slots[index2]))
            {
                if (index2 == slots.Length - 1) return;
                index2++;
            }

            SlotHelper.MergeSlots(slots[index1], slots[index2]);
        } while (index1 < slots.Length - 1);
    }

    void SortByItemType()
    {
        if (IsEmpty()) return;

        int index1 = 0;
        int index2;
        int length = slots.Length;

        // loop through each type of item and sort items by type
        foreach (ItemIndex itemIndex in Enum.GetValues(typeof(ItemIndex)))
        {
            index2 = index1 + 1;

            // sort
            while (index1 < length - 1)
            {
                // loop until find a slot have no item or a different type
                while ((index1 < length) && SlotHelper.HaveSameItem(slots[index1], itemIndex)) index1++;

                index2 = index1 + 1;

                // loop until find a slot with the same type
                while ((index2 < length) && !SlotHelper.HaveSameItem(slots[index2], itemIndex)) index2++;

                // check if there is no item to wrap
                if (index1 >= length || index2 >= length) break;

                // swap to bring item from the end to the start of the array
                SlotHelper.SwapSlots(slots[index1], slots[index2]);

                index1++;
            }
        }
    }

    public bool GetItem(ItemIndex _itemIndex, int _amount)
    {
        // check amount of that item in inventory
        int realAmount = GetItemAmount(_itemIndex);
        if (realAmount < _amount)
        {
            Debug.Log($"not enough {_itemIndex}: {realAmount} < {_amount}");
            return false;
        }

        // take the necessary amount
        for (int i = slots.Length - 1; i >= 0; i--)
        {
            // find slot contain that item
            if (SlotHelper.HaveSameItem(slots[i], _itemIndex))
            {
                // calculate the need amount left after taking from a slot
                _amount = slots[i].TakeItem(_amount);
            }
        }
        return false;
    }

    int GetItemAmount(ItemIndex _itemIndex)
    {
        int result = 0;
        for (int i = 0; i < slots.Length; i++)
        {
            if (SlotHelper.HaveSameItem(slots[i], _itemIndex))
            {
                result += slots[i].Amount;
            }
        }
        return result;
    }

    public bool IsEmpty()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].IsEmpty) return false;
        }
        return true;
    }

    public void ResetAll()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].Reset();
        }
    }
}
