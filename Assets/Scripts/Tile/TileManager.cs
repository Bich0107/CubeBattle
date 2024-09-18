using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] int sizeX;
    [SerializeField] int sizeZ;
    Tile[,] tiles;

    void Awake()
    {
        tiles = new Tile[sizeX, sizeZ];
        Initialize();
    }

    void Initialize()
    {
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeZ; j++)
            {
                tiles[i, j] = new Tile(i, j);
            }
        }
    }

    public Tile GetTile(int x, int z)
    {
        return tiles[x, z];
    }
}
