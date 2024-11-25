using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveRune : BaseRune
{
    [SerializeField] protected float value;

    public float Value => value;

    public abstract void Empower(GameObject _activeRune);
}
