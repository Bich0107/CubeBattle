using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnRange
{
    public int minX;
    public int maxX;
    public int minZ;
    public int maxZ;

    public int RandomX() => Random.Range(minX, maxX + 1);
    public int RandomZ() => Random.Range(minZ, maxZ + 1);
}

public class Spawner : MonoBehaviour
{
    TileManager tileManager;
    [SerializeField] string prefabTag;
    [SerializeField] int spawnAmount;
    [SerializeField] SpawnRange[] spawnRanges;
    [SerializeField] bool spawnRandomly = true;
    Tile[] emptyTiles;
    int[] availableIndices;
    int availableIndicesAmount;

    void Start()
    {
        tileManager = FindObjectOfType<TileManager>();

        Spawn();
    }

    public void Spawn()
    {
        if (spawnRandomly)
        {
            // on default, only tiles on the leftmost and rightmost z line is empty
            availableIndices = new int[tileManager.Tiles.Length / tileManager.SizeX];
            emptyTiles = new Tile[tileManager.Tiles.Length / tileManager.SizeX];
            SpawnRandom();
        }
        else
        {
            SpawnAll();
        }
    }

    void SpawnAll()
    {
        foreach (SpawnRange spawnRange in spawnRanges)
        {
            for (int z = spawnRange.minZ; z <= spawnRange.maxZ; z++)
            {
                for (int x = spawnRange.minX; x <= spawnRange.maxX; x++)
                {
                    SetObject(tileManager.GetTile(x, z));
                }
            }
        }
    }

    #region Spawn random
    void SpawnRandom()
    {
        int spawnPerSpawnRange = spawnAmount / spawnRanges.Length;

        for (int j = 0; j < spawnRanges.Length; j++)
        {
            InitializeEmptyTilesList(spawnRanges[j]); // add all empty tiles in this spawn range into a lsit
            Shuffle(); // shuffle those tiles indices to create randomness

            // set objects to random tiles base on available indices
            for (int i = 0; i < spawnPerSpawnRange; i++)
            {
                int index = availableIndices[i];
                SetObject(emptyTiles[index]);
            }
        }
    }

    void InitializeEmptyTilesList(SpawnRange spawnRange)
    {
        int index = 0;
        availableIndicesAmount = 0; // reset value

        for (int z = spawnRange.minZ; z <= spawnRange.maxZ; z++)
        {
            for (int x = spawnRange.minX; x <= spawnRange.maxX; x++)
            {
                emptyTiles[index] = tileManager.GetTile(x, z);
                availableIndices[index] = index;
                index++;
            }
        }
        availableIndicesAmount = index; // get the real amount of empty tiles

        Shuffle();
    }

    void Shuffle()
    {
        for (int i = availableIndicesAmount - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            // Swap
            int temp = availableIndices[i];
            availableIndices[i] = availableIndices[j];
            availableIndices[j] = temp;
        }
    }
    #endregion

    void SetObject(Tile _tile)
    {
        GameObject spawnObject = ObjectPool.Instance.Spawn(prefabTag);
        _tile.SetObject(spawnObject);
        spawnObject.transform.position = tileManager.GetTilePosition(_tile);
        spawnObject.SetActive(true);
    }
}
