using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RuneEquipSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] RuneEquipHandler equipHandler;
    [SerializeField] PartFace face;
    [SerializeField] RuneSO rune;
    [SerializeField] Image image;

    public void SetRune(RuneSO _rune)
    {
        rune = _rune;
        UpdateUI();
    }

    public void UpdateRuneSetter(RuneSetter _runeSetter)
    {
        _runeSetter.SetRune(rune, face);
    }

    public void UpdateUI()
    {
        if (rune == null)
        {
            image.sprite = null;
            return;
        }

        image.sprite = rune.Sprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // if this slot is select twice => remove rune from this slot
        if (equipHandler.SelectSlot(this))
        {
            inventoryManager.AddItem(rune.ObjectIndex, 1);
            inventoryManager.UpdateUI();

            rune = null;

            UpdateUI();
            return;
        }
        equipHandler.DisplayDescription(GetDescription());
    }

    public string GetDescription()
    {
        if (rune == null) return "Slot empty";
        else return $"Description of rune {rune.Name}:\n {rune.Description}";
    }

}
