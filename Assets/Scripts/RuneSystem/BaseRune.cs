using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseRune : MonoBehaviour
{
    public RuneSO rune;
    public BodyPart bodyPart;
    public PartFace partFace;

    public abstract void Activate();
    public virtual void Set(RuneSO _rune, BodyPart _bodyPart, PartFace _partFace)
    {
        rune = _rune;
        bodyPart = _bodyPart;
        partFace = _partFace;
    }
}
