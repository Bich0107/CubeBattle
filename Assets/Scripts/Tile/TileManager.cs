using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] int sizeX;
    [SerializeField] int sizeZ;
    [SerializeField] Tile startTile;
    public Tile StartTile
    {
        get { return startTile; }
        set { startTile = value; }
    }

    [SerializeField] Vector3 startPos;
    public Vector3 StartPos => startPos;

    [SerializeField] float distance;
    [SerializeField] Tile[] tiles;
    public int SizeX => sizeX;
    public int SizeZ => sizeZ;
    public Tile[] Tiles => tiles;

    void Awake()
    {
        tiles = new Tile[sizeX * sizeZ];
        Initialize();
    }

    void Initialize()
    {
        for (int i = 0; i < sizeZ; i++)
        {
            for (int j = 0; j < sizeX; j++)
            {
                tiles[j + i * sizeX] = new Tile(j, i);
            }
        }
    }

    public Tile GetTile(int x, int z)
    {
        if (z < 0 || z >= sizeZ || x < 0 || x >= sizeX) return null;
        return tiles[z * sizeX + x];
    }

    public Tile GetTile(Vector3 position)
    {
        int x = Mathf.RoundToInt(startTile.X + (position.x - startPos.x) / distance);
        int z = Mathf.RoundToInt(startTile.Z + (position.z - startPos.z) / distance);
        return tiles[z * sizeX + x];
    }

    public Tile GetFrontTile(Tile tile)
    {
        if (tile.Z + 1 >= sizeZ) return null;
        return tiles[(tile.Z + 1) * sizeX + tile.X];
    }

    public Tile GetBackTile(Tile tile)
    {
        if (tile.Z - 1 < 0) return null;
        return tiles[(tile.Z - 1) * sizeX + tile.X];
    }

    public Tile GetRightTile(Tile tile)
    {
        if (tile.X + 1 >= sizeX) return null;
        return tiles[tile.Z * sizeX + tile.X + 1];
    }

    public Tile GetLeftTile(Tile tile)
    {
        if (tile.X - 1 < 0) return null;
        return tiles[tile.Z * sizeX + tile.X - 1];
    }

    public Vector3 GetTilePosition(Tile tile)
    {
        Vector3 pos = new()
        {
            x = startPos.x + (tile.X - startTile.X) * distance,
            y = startPos.y,
            z = startPos.z + (tile.Z - startTile.Z) * distance
        };
        return pos;
    }

    public Tile GetRandomTileInRange(int minX, int maxX, int minZ, int maxZ)
    {
        int x = Random.Range(minX, maxX + 1);
        int z = Random.Range(minZ, maxZ + 1);
        return GetTile(x, z);
    }

    public Tile GetRandomTileOnCircle(Tile _centerTile, float _radius)
    {
        // create a random angle from 0 to 360
        float angle = Random.Range(0f, Mathf.PI * 2);

        // calculate x and z value
        int x = Mathf.RoundToInt(_centerTile.X + Mathf.Cos(angle) * _radius);
        int z = Mathf.RoundToInt(_centerTile.Z + Mathf.Sin(angle) * _radius);
        return tiles[z + x * sizeX];
    }
}
