using System;
using TMPro;
using UnityEngine;

public class RuneEquipHandler : MonoBehaviour
{
    [SerializeField] RuneSetter[] runeSetters;
    [SerializeField] RuneEquipSlot[] equipSlots;
    [SerializeField] RuneEquipSlot selectedSlot;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] TextMeshProUGUI bodyPartText;
    int runeSetterIndex;
    RuneSetter currentRuneSetter => runeSetters[runeSetterIndex];

    public void NextRuneSetter()
    {
        runeSetterIndex = runeSetterIndex + 1 < runeSetters.Length ? runeSetterIndex + 1 : 0;
        bodyPartText.text = Enum.GetValues(typeof(BodyPart)).GetValue(runeSetterIndex).ToString();
        DisplayRuneSlots();
    }

    public void PreviousRuneSetter()
    {
        runeSetterIndex = runeSetterIndex - 1 >= 0 ? (runeSetterIndex - 1) : (runeSetters.Length - 1);
        bodyPartText.text = Enum.GetValues(typeof(BodyPart)).GetValue(runeSetterIndex).ToString();
        DisplayRuneSlots();
    }

    /// <summary>
    /// Get runes from rune setter and display on equip slots
    /// </summary>
    public void DisplayRuneSlots()
    {
        foreach (PartFace face in Enum.GetValues(typeof(PartFace)))
        {
            int index = (int)face;
            equipSlots[index].SetRune(currentRuneSetter.GetRune(face));
        }
    }

    /// <summary>
    /// Return true if current selected slot is selected again, false otherwise
    /// </summary>
    public bool SelectSlot(RuneEquipSlot _slot)
    {
        if (selectedSlot == _slot) return true;

        selectedSlot = _slot;
        return false;
    }

    public void UpdateRuneSetter()
    {
        for (int i = 0; i < equipSlots.Length; i++)
        {
            equipSlots[i].UpdateRuneSetter(currentRuneSetter);
        }
    }

    public void Equip(Slot _slot)
    {
        if (selectedSlot == null) return;

        selectedSlot.SetRune(_slot.SlotItem.Rune);
        _slot.TakeItem(1);
        selectedSlot = null;
    }

    public void DisplayDescription(string _text) => descriptionText.text = _text;
}
