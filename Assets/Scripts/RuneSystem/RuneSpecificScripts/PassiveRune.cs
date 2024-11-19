using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveRune : BaseRune
{
    [SerializeField] float value;

    public float Value => value;
}
