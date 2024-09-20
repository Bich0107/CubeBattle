using System;
using UnityEngine;

[Serializable]
public class Tile
{
    [SerializeField] GameObject target;
    [SerializeField] int x;
    [SerializeField] int z;
    public int X => x;
    public int Z => z;

    public Tile() { }
    public Tile(int _x, int _z)
    {
        x = _x;
        z = _z;
    }

    public void SetObject(GameObject _go) => target = _go;
    public GameObject Target => target;
    public bool IsOccupied => target != null;
}
