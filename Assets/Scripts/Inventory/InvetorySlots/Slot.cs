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
    public int Amount => slotSO.Amount;
    public int MaxStack => SlotItem == null ? 0 : SlotItem.MaxStack;

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

    public int SetItem(Item _item, int _addAmount)
    {
        slotSO.Item = _item;

        return UpdateAmount(_addAmount);
    }

    public int AddItem(int _addAmount)
    {
        // if its stack is full, return
        if (SlotItem.MaxStack == slotSO.Amount) return _addAmount;

        return UpdateAmount(_addAmount);
    }

    int UpdateAmount(int _addAmount)
    {
        // find the real add amount
        int remainSpace = SlotItem.MaxStack - slotSO.Amount;
        int realAddAmount = remainSpace >= _addAmount ? _addAmount : remainSpace;

        // update slot amount and remain add amount
        _addAmount -= realAddAmount;
        slotSO.Amount += realAddAmount;

        UpdateUI();

        return _addAmount;
    }

    public int TakeItem(int _takeAmount)
    {
        // if this slot is empty, return
        if (SlotItem == null) return 0;

        // find the real take amount
        int realTakeAmount = slotSO.Amount >= _takeAmount ? _takeAmount : slotSO.Amount;

        // update slot amount and remain take amount
        _takeAmount -= realTakeAmount;
        slotSO.Amount -= realTakeAmount;

        if (slotSO.Amount == 0) EmptySlot();
        else UpdateUI();

        return _takeAmount;
    }

    public void EmptySlot()
    {
        amountText.text = "0";
        itemImage.sprite = null;
        slotSO = null;
    }

    public void DisplayDescription()
    {
        // testing
        TakeItem(1);
        UpdateUI();

        if (SlotItem == null) Debug.Log("Slot empty");
        else
            Debug.Log($"Description of item {SlotItem.Name}: {SlotItem.Description}");
    }

    public bool HaveSameItem(Slot other)
    {
        if (other.SlotItem == null) return false;

        return other.SlotItem.ItemIndex == other.SlotItem.ItemIndex;
    }

    public bool HaveSameItem(ItemIndex index)
    {
        return SlotItem.ItemIndex == index;
    }

    public void Reset()
    {
        slotSO.Amount = 0;
        slotSO.Item = null;
        UpdateUI();
    }

    public bool IsFull()
    {
        if (SlotItem == null) return false;

        return SlotItem.MaxStack == slotSO.Amount;
    }

    public bool IsEmpty => SlotItem == null;
}
