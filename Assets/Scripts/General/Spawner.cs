using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    TileManager tileManager;
    [SerializeField] string prefabTag;
    [SerializeField] int spawnAmount;

    void Start()
    {
        tileManager = FindObjectOfType<TileManager>();
    }
}
