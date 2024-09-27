using UnityEngine;

public class InventoryTester : MonoBehaviour
{
    [SerializeField] InventoryManager inventory;
    [SerializeField] ItemIndex itemIndex;
    [SerializeField] int amount;

    public void Add()
    {
        inventory.AddItem(itemIndex, amount);
    }
}
