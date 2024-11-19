using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneFace : MonoBehaviour
{
    [SerializeField] PartFace partFace;
    [SerializeField] RuneSetter runeSetter;

    public RuneSO GetRune()
    {
        return runeSetter.GetRune(partFace);
    }
}
