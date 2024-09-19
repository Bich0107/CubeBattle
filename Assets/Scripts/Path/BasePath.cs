using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePart : PathScript, ITriggerByPlayer
{
    PathSetter pathSetter;

    void Awake()
    {
        pathSetter = FindObjectOfType<PathSetter>();
    }

    void OnEnable()
    {
        Appear();
    }

    public override void Appear()
    {
        IsNew = true;
    }

    public void Triggered()
    {
        pathSetter.SpawnByNumOfRound(tile);
    }

    public override void Disappear()
    {
        gameObject.SetActive(false);
    }
}
