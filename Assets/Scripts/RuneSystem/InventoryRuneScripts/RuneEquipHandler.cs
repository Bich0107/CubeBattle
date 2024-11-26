using System;
using TMPro;
using UnityEngine;

public class RuneEquipHandler : MonoBehaviour
{
    [SerializeField] RuneSetter[] runeSetters;
    [SerializeField] RuneEquipSlot[] equipSlots;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] RuneEquipSlot selectedSlot;
    int runeSetterIndex;
    RuneSetter currentRuneSetter => runeSetters[runeSetterIndex];

    public void NextRuneSetter()
    {
        runeSetterIndex = runeSetterIndex + 1 < runeSetters.Length ? runeSetterIndex + 1 : 0;
        DisplayRuneSlots();
    }

    public void PreviousRuneSetter()
    {
        runeSetterIndex = runeSetterIndex - 1 >= 0 ? runeSetterIndex - 1 : runeSetters.Length - 1;
        DisplayRuneSlots();
    }

    public void DisplayRuneSlots()
    {
        foreach (PartFace face in Enum.GetValues(typeof(PartFace)))
        {
            int index = (int)face;
            equipSlots[index].SetRune(currentRuneSetter.GetRune(face));
        }
    }

    public void SelectSlot(RuneEquipSlot _slot)
    {
        selectedSlot = _slot;
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

    public void DisplayDescription(string _text)
    {
        descriptionText.text = _text;
    }
}
