using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Goal : MonoBehaviour, ITriggerByPlayer
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

    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        Tile spawnTile = tileManager.GetRandomTileInRange(minX, maxX + 1, minZ, maxZ + 1);
        Vector3 spawnPos = tileManager.GetTilePosition(spawnTile);
        spawnTile.SetObject(gameObject);
        transform.position = spawnPos;
        Debug.Log("spawn at: " + transform.position);
    }

    public void Triggered()
    {
        Debug.Log("Player reached goal");
    }
}
