using UnityEngine;

public class InventoryTester : MonoBehaviour
{
    [Header("Add")]
    [SerializeField] InventoryManager inventory;
    [SerializeField] ItemIndex itemIndex;
    [SerializeField] int amount;
    [Header("Swap")]
    [SerializeField] Slot slot1;
    [SerializeField] Slot slot2;

    public void Add()
    {
        inventory.AddItem(itemIndex, amount);
    }

    public void Swap()
    {
        SlotHelper.SwapSlots(slot1, slot2);
    }
}
