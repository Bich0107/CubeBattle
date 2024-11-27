using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] RuneEquipHandler equipHandler;
    [SerializeField] InventorySlotSO slotSO;
    [SerializeField] TextMeshProUGUI amountText;
    [SerializeField] Image itemImage;
    public InventorySlotSO SlotSO => slotSO;
    public Item SlotItem => slotSO.Item;
    public int Amount => slotSO.Amount;
    public int MaxStack => SlotItem == null ? 0 : SlotItem.MaxStack;

    void Start()
    {
        equipHandler = FindObjectOfType<RuneEquipHandler>();
    }

    public void SetSlotSO(InventorySlotSO _slotSO)
    {
        slotSO = _slotSO;
        UpdateUI();
    }

    public void UpdateUI()
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

    /// <summary>
    /// This function take item from this slot and return the taken amount
    /// </summary>
    /// <param name="_takeAmount">The amount to be taken.</param>
    /// <returns>The number of item taken form this slot.</returns>
    public int TakeItem(int _takeAmount)
    {
        // if this slot is empty, return
        if (SlotItem == null) return 0;

        // find the real take amount
        int realTakeAmount = slotSO.Amount >= _takeAmount ? _takeAmount : slotSO.Amount;

        // update slot amount
        slotSO.Amount -= realTakeAmount;
        if (slotSO.Amount == 0) EmptySlot();
        else UpdateUI();

        return realTakeAmount;
    }

    public void EmptySlot()
    {
        amountText.text = "0";
        itemImage.sprite = null;
        slotSO.Item = null;
    }

    public string GetDescription()
    {
        if (SlotItem == null) return "Slot empty";
        else return $"Description of rune {SlotItem.Name}:\n {SlotItem.Description}";
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

    public void OnPointerClick(PointerEventData eventData)
    {
        equipHandler.DisplayDescription(GetDescription());
        equipHandler.Equip(this);
    }

    public bool IsEmpty => SlotItem == null;
}
