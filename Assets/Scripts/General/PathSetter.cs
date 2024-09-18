using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathSetter : MonoBehaviour
{
    [SerializeField] TileManager tileManager;
    [Header("General settings")]
    [SerializeField] List<Tile> pathTiles;
    [SerializeField] Tile spawnTile;
    [SerializeField] Vector3 spawnPosition;
    [SerializeField] float distance;
    [SerializeField] float specialPathChance = 0.2f;
    [Header("Path style around settings")]
    [SerializeField] int numOfRound;

    void Start()
    {
        tileManager = FindObjectOfType<TileManager>();
        GeneratePath();
    }

    public void GeneratePath()
    {
        SpawnByNumOfRound(spawnTile, numOfRound);
    }

    GameObject SpawnPath(Vector3 _position)
    {
        GameObject path = ObjectPool.Instance.Spawn(Tags.Path);

        if (path == null) return null;

        path.transform.position = _position;
        path.SetActive(true);



        return path;
    }

    // need update later
    void SpawnByNumOfRound(Tile _startTile, int _numOfRound)
    {
        Tile startTile = _startTile;
        Vector3 pos = spawnPosition;
        int x, z, total;

        for (int i = -_numOfRound; i <= numOfRound; i++)
        {
            for (int j = -numOfRound; j <= numOfRound; j++)
            {
                // make path only appear around startTile in cirlce for numOfRound
                total = Mathf.Abs(i) + Mathf.Abs(j);
                if (total <= numOfRound)
                {
                    // get current tile
                    x = startTile.X + i;
                    z = startTile.Z + j;
                    Tile tile = tileManager.GetTile(x, z);

                    // check if this tile already hold something
                    if (tile.IsOccupied) continue;

                    // get spawn position
                    pos.x = spawnPosition.x + i * distance;
                    pos.z = spawnPosition.z + j * distance;

                    // create path and add the tile holding it to control later 
                    GameObject path = SpawnPath(pos);
                    tile.SetObject(path);
                    pathTiles.Add(tile);
                }
            }
        }
    }
}

