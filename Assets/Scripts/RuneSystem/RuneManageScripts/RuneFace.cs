using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Keep part face value of a rune <see langword="and"/> help <see langword="return"/> rune infor</summary>
public class RuneFace : MonoBehaviour
{
    [SerializeField] PartFace partFace;
    [SerializeField] RuneSetter runeSetter;

    public RuneSO GetRune()
    {
        return runeSetter.GetRune(partFace);
    }
}
