using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    TileManager tileManager;

    void Start()
    {
        tileManager = FindObjectOfType<TileManager>();
    }

    
}
