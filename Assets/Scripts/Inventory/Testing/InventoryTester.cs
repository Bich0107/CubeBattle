using UnityEngine;

public class InventoryTester : MonoBehaviour
{
    [SerializeField] InventoryManager inventory;
    [SerializeField] ItemIndex itemIndex;
    [SerializeField] int amount;

    public void Add()
    {
        Debug.Log("Add item: " + (inventory.AddItem(itemIndex, amount) ? "success" : "failed"));
    }
}
