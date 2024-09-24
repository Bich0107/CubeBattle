using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPositionSetter : MonoBehaviour
{
    TileManager tileManager;
    [Header("Spawn tile coordinates")]
    [SerializeField] int minX;
    [SerializeField] int maxX;
    [SerializeField] int minZ, maxZ;

    void Awake()
    {
        tileManager = FindObjectOfType<TileManager>();
    }

    void OnEnable()
    {
        Spawn();
    }

    void Spawn()
    {
        Tile spawnTile = tileManager.GetRandomTileInRange(minX, maxX, minZ, maxZ);
        Vector3 spawnPos = tileManager.GetTilePosition(spawnTile);
        spawnTile.SetObject(gameObject);
        transform.position = spawnPos;
        Debug.Log(gameObject.name + " spawn at: " + transform.position);
    }
}
