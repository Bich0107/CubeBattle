using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New inventory slot info", menuName = "Inventory/Slot")]
public class InventorySlotSO : ScriptableObject
{
    public Item Item;
    public int Amount;
}
