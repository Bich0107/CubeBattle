using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneManager : MonoBehaviour
{
    [Tooltip("Each 5 runes (Back, Front, Left, Right, Top) for each body part (front, back, top, bottom)")]
    [SerializeField] RuneSO[] runes;
    [SerializeField] Transform[] runeSpawnTrans;
    [SerializeField] RuneSetter[] runeSetters;

    void Awake()
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
        int partIndex = (int)_part;
        if (partIndex > runeSetters.Length)
        {
            LogSystem.Instance.Log("Body part index out of range");
            return;
        }

        RuneSetter runeSetter = runeSetters[partIndex];
        runeSetter.SetRune(_rune, _face);

        // instantiate and store current instance of rune prefab into current rune SO
        if (_rune.RunePrefab != null)
        {
            int tranIndex = partIndex * Enum.GetValues(typeof(PartFace)).Length + (int)_face;
            _rune.RuneGO = Instantiate(_rune.RunePrefab, runeSpawnTrans[tranIndex].position, Quaternion.identity, runeSpawnTrans[tranIndex]);
        }
    }

    public RuneSO GetRune(BodyPart _part, PartFace _face)
    {
        int index = (int)_part * Enum.GetValues(typeof(PartFace)).Length + (int)_face;
        RuneSO result = runeSetters[index].GetRune(_face);
        // ???????

        return result;
    }
}
