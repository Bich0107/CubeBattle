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
    int availableIndicesAmount; // store real length of this array 

    void Awake()
    {
        tileManager = FindObjectOfType<TileManager>();
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
        Tile temp;
        foreach (SpawnRange spawnRange in spawnRanges)
        {
            for (int z = spawnRange.minZ; z <= spawnRange.maxZ; z++)
            {
                for (int x = spawnRange.minX; x <= spawnRange.maxX; x++)
                {
                    temp = tileManager.GetTile(x, z);
                    if (!temp.IsOccupied) SetObject(tileManager.GetTile(x, z));
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
            InitializeEmptyTilesList(spawnRanges[j]); // add all empty tiles in this spawn range into a list
            Shuffle(availableIndices); // shuffle those tiles indices to create randomness

            int spawnCounter = 0;

            // set objects to random tiles base on available indices
            for (int i = 0; spawnCounter < spawnPerSpawnRange && i < availableIndicesAmount; i++, spawnCounter++)
            {
                int index = availableIndices[i];
                SetObject(emptyTiles[index]);
            }
        }
    }

    void InitializeEmptyTilesList(SpawnRange spawnRange)
    {
        int index = -1;
        availableIndicesAmount = 0; // reset value
        Tile temp;

        for (int z = spawnRange.minZ; z <= spawnRange.maxZ; z++)
        {
            for (int x = spawnRange.minX; x <= spawnRange.maxX; x++)
            {
                // skip this tile if its already hold something
                temp = tileManager.GetTile(x, z);
                if (!temp.IsOccupied)
                {
                    index++;
                    emptyTiles[index] = temp;
                    availableIndices[index] = index;
                }
            }
        }

        availableIndicesAmount = index + 1;
    }

    // use to shuffle vailable indices to create randomness
    void Shuffle(int[] _array)
    {
        for (int i = _array.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            // Swap
            int temp = _array[i];
            _array[i] = _array[j];
            _array[j] = temp;
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
