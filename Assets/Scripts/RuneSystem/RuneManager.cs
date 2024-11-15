using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BodyPart
{
    Front, Back, Top, Bottom
}

public enum PartFace
{
    Back, Front, Left, Right, Top
}

public class RuneManager : MonoBehaviour
{
    [SerializeField] RuneSO[] runes;
    [SerializeField] RuneSetter[] runeSetters;

    void Start()
    {
        int counter = 0;
        foreach (BodyPart part in Enum.GetValues(typeof(BodyPart)))
        {
            foreach (PartFace face in Enum.GetValues(typeof(PartFace)))
            {
                SetRune(runes[counter], part, face);
                counter++;
            }
        }
    }

    public void SetRune(RuneSO _rune, BodyPart _part, PartFace _face)
    {
        int index = (int)_part;
        if (index > runeSetters.Length)
        {
            LogSystem.Instance.Log("Body part index out of range");
            return;
        }

        RuneSetter runeSetter = runeSetters[index];
        runeSetter.SetRune(_rune, _face);
    }
}
