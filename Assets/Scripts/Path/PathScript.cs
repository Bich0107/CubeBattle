using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PathScript : MonoBehaviour
{
    [SerializeField] protected Tile tile;
    public Tile Tile => tile;
    public bool IsNew;

    public void SetTile(Tile _tile) => tile = _tile;

    public virtual void Appear() { }
    public virtual void Disappear() { }
}
