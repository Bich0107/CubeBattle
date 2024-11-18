using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    [SerializeField] Spawner wallSpawner;
    [SerializeField] Spawner scrollingPilarSpawner;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        scrollingPilarSpawner.Spawn();

        // the wall will take up all empty tile so spawn it last
        wallSpawner.Spawn();
    }
}
