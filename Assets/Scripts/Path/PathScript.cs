using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PathScript : MonoBehaviour
{
    [SerializeField] Tile tile;
    public Tile Tile
    {
        get { return tile; }
        set { tile = value; }
    }

    protected virtual void Appear() { }
    protected virtual void Disappear() { }
}
