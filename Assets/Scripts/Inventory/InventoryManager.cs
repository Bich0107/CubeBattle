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

    public bool AddItem(ItemIndex _itemIndex, int _amount)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].AddItem(items[(int)_itemIndex], _itemIndex, ref _amount);
            if (_amount <= 0) return true;
        }
        return false;
    }

    public void SortItems()
    {
        int index1 = 0;
        int index2 = 1;
        while (index1 < slots.Length - 1)
        {
            if (slots[index1].HaveSameItem(slots[index2]))
            {

            }
        }
    }

    void MergeSlots(Slot slot1, Slot slot2)
    {
        if (slot1.SlotItem.ItemIndex != slot2.SlotItem.ItemIndex) return;

    }

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
