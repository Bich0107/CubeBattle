using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneSetter : MonoBehaviour
{
    [SerializeField] RuneSO[] runes;
    [SerializeField] SpriteRenderer[] runeRenderers;

    public void SetRune(RuneSO _rune, PartFace _face)
    {
        int index = (int)_face;
        if (index > runes.Length)
        {
            LogSystem.Instance.Log("Part face index out of range");
            return;
        }

        runes[index] = _rune;
        runeRenderers[index].sprite = _rune.Sprite;
    }

    public void Display()
    {
        if (runes.Length == 0) return;

        for (int i = 0; i < runeRenderers.Length; i++)
        {
            runeRenderers[i].sprite = runes[i].Sprite;
        }
    }
}
