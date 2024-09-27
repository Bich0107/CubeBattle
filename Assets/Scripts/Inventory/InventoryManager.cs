using UnityEngine;

public enum ItemIndex
{
    Item1, Item2, Item3, Item4
}

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
            else if (slots[i].HaveSameItem(_itemIndex))
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
        int index1 = 0;
        int index2 = 1;

        do
        {
            // loop until finding a slot with empty space or reaching the second to last index of the array
            while (slots[index1].IsFull())
            {
                if (index1 == slots.Length - 2) return;
                index1++;
                index2++;
            }

            // loop until finding a slot have the same item as slot1 or reaching the end of the array
            while (!slots[index1].HaveSameItem(slots[index2]))
            {
                if (index2 == slots.Length - 1) return;
                index2++;
            }

            SlotHelper.MergeSlots(slots[index1], slots[index2]);
        } while (index1 < slots.Length - 1);
    }

    // unfinished
    public Item GetItem(ItemIndex _itemIndex, int _amount)
    {
        return null;
    }

    public void ResetAll()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].Reset();
        }
    }
}
