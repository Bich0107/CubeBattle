using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathSetter : MonoBehaviour
{
    [SerializeField] TileManager tileManager;
    [Header("General settings")]
    [SerializeField] List<PathScript> paths;
    [SerializeField] Tile spawnTile;
    [SerializeField] float distance;
    //[SerializeField] float specialPathChance = 0.2f;
    [Header("Path style around settings")]
    [SerializeField] int numOfRound;

    void Start()
    {
        tileManager = FindObjectOfType<TileManager>();
        GeneratePath();
    }

    public void GeneratePath()
    {
        SpawnByNumOfRound(spawnTile);
    }

    GameObject SpawnPath(Vector3 _position)
    {
        GameObject path = ObjectPool.Instance.Spawn(Tags.Path);

        if (path == null) return null;

        path.transform.position = _position;
        path.SetActive(true);

        return path;
    }

    public void SpawnByNumOfRound(Tile _startTile)
    {
        Tile startTile = _startTile;
        Vector3 startPos = _startTile.Target.transform.position;
        Vector3 pos = new Vector3();
        pos.y = startPos.y;

        int x, z;

        for (int i = 1; i <= numOfRound; i++)
        {
            // front tile
            x = startTile.X;
            z = startTile.Z + i;
            AddPathToTile(startPos, pos, startTile, x, z, i);

            // back tile
            x = startTile.X;
            z = startTile.Z - i;
            AddPathToTile(startPos, pos, startTile, x, z, i);

            // right tile
            x = startTile.X + i;
            z = startTile.Z;
            AddPathToTile(startPos, pos, startTile, x, z, i);

            // left tile
            x = startTile.X - i;
            z = startTile.Z;
            AddPathToTile(startPos, pos, startTile, x, z, i);

            // front right tile
            x = startTile.X + i;
            z = startTile.Z + i;
            AddPathToTile(startPos, pos, startTile, x, z, i + 1);

            // front left tile
            x = startTile.X - i;
            z = startTile.Z + i;
            AddPathToTile(startPos, pos, startTile, x, z, i + 1);

            // back right tile
            x = startTile.X + i;
            z = startTile.Z - i;
            AddPathToTile(startPos, pos, startTile, x, z, i + 1);

            // back left tile
            x = startTile.X - i;
            z = startTile.Z - i;
            AddPathToTile(startPos, pos, startTile, x, z, i + 1);
        }

        DisableOldPaths();
        UpdatePathsStatus();
    }

    void AddPathToTile(Vector3 _startPos, Vector3 _pos, Tile _startTile, int tileX, int tileZ, int _numOfRound)
    {
        // make sure tiles created is in range base on current num of round
        if (Mathf.Abs(tileX - _startTile.X) + Mathf.Abs(tileZ - _startTile.Z) > _numOfRound) return;

        Tile _tile = tileManager.GetTile(tileX, tileZ);

        // if the tile already holding an object, check if it a path object, if it is, renew its status
        if (_tile.IsOccupied)
        {
            PathScript script = _tile.Target.GetComponent<PathScript>();
            if (script != null) script.IsNew = true;
            return;
        }

        // find new tile position base on the starting tile and its position
        _pos.x = _startPos.x + (_tile.X - _startTile.X) * distance;
        _pos.z = _startPos.z + (_tile.Z - _startTile.Z) * distance;

        // create path and add it to list for later control 
        GameObject path = SpawnPath(_pos);
        PathScript pathScript = path.GetComponent<PathScript>();
        pathScript.SetTile(_tile);
        _tile.SetObject(path);
        paths.Add(pathScript);
    }

    void DisableOldPaths()
    {
        // Iterating backwards to avoid index issues
        for (int i = paths.Count - 1; i >= 0; i--)
        {
            if (!paths[i].IsNew)
            {
                // remove the object this tile is holding
                paths[i].Tile.SetObject(null);

                // disable the path
                paths[i].Disappear();

                paths.RemoveAt(i);
            }
        }
    }

    void UpdatePathsStatus()
    {
        foreach (PathScript script in paths)
        {
            script.IsNew = false;
        }
    }
}

