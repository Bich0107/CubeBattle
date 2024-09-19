using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePart : PathScript, ITriggerByPlayer
{
    PathSetter pathSetter;

    void Awake()
    {
        pathSetter = FindObjectOfType<pathSetter>();
    }

    void OnEnable()
    {
        Appear();
    }

    protected override void Appear()
    {

    }

    public void Trigger()
    {

    }

    protected override void Disappear()
    {

    }
}
