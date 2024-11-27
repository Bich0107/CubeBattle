using UnityEngine;

public class InventoryTester : MonoBehaviour
{
    [Header("Add")]
    [SerializeField] InventoryManager inventory;
    [SerializeField] ObjectIndex objIndex;
    [SerializeField] int amount;
    [Header("Swap")]
    [SerializeField] Slot slot1;
    [SerializeField] Slot slot2;

    public void Add()
    {
        inventory.AddItem(objIndex, amount);
    }

    public void Swap()
    {
        SlotHelper.SwapSlots(slot1, slot2);
    }
}
