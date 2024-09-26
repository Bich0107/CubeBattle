using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] InventorySlotSO slotSO;
    [SerializeField] TextMeshProUGUI amountText;
    [SerializeField] Image itemImage;
    public Item SlotItem => slotSO.Item;

    public void SetSlotSO(InventorySlotSO _slotSO)
    {
        slotSO = _slotSO;
        UpdateUI();
    }

    void UpdateUI()
    {
        amountText.text = slotSO.Amount.ToString();
        
        if (slotSO.Item != null)
        {
            itemImage.sprite = slotSO.Item.Sprite;
        }
        else
        {
            itemImage.sprite = null;
        }
    }

    public void AddItem(Item _item, ItemIndex _itemIndex, ref int _addAmount)
    {
        // if this slot does not contain this item or its stack is full, return
        if (SlotItem != null && (SlotItem.ItemIndex != _itemIndex || SlotItem.MaxStack == slotSO.Amount)) return;

        // if this slot is empty
        if (SlotItem == null) slotSO.Item = _item;

        // find the real add amount
        int remainSpace = SlotItem.MaxStack - slotSO.Amount;
        int realAddAmount = remainSpace >= _addAmount ? _addAmount : remainSpace;

        // update slot amount and remain add amount
        _addAmount -= realAddAmount;
        slotSO.Amount += realAddAmount;

        UpdateUI();
    }

    public void TakeItem(ItemIndex _itemIndex, ref int _takeAmount)
    {
        // if this slot is empty or does not contain this item, return
        if (SlotItem == null || SlotItem.ItemIndex != _itemIndex) return;

        // find the real add amount
        int realTakeAmount = slotSO.Amount >= _takeAmount ? _takeAmount : slotSO.Amount;

        // update slot amount and remain take amount
        _takeAmount -= realTakeAmount;
        slotSO.Amount -= realTakeAmount;

        if (slotSO.Amount == 0) EmptySlot();
        else UpdateUI();
    }

    public void EmptySlot()
    {
        amountText.text = "0";
        itemImage.sprite = null;
        slotSO = null;
    }

    public void DisplayDescription()
    {
        Debug.Log($"Description of item {SlotItem.Name}: {SlotItem.Description}");
    }

    public bool HaveSameItem(Slot other)
    {
        return other.SlotItem.ItemIndex == other.SlotItem.ItemIndex;
    }

    public void Reset()
    {
        slotSO.Amount = 0;
        slotSO.Item = null;
        UpdateUI();
    }
}
