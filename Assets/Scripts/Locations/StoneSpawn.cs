using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StoneSpawn : MonoBehaviour
{
    [Inject] private DiContainer _container;

    public GameObject stoneTile;
    public GameObject cuperTile;
    public GameObject ironTile;
    public GameObject coalTile;
    public GameObject emptyTile;

    [SerializeField] private Transform stonesParent;

    public int width = 30;
    public int height = 30;

    public int minStones = 30;
    public int maxStones = 60;

    private bool[,] occupied;
    private System.Random rng;
    private List<GameObject> spawnedStones = new List<GameObject>();

    private void Start()
    {
        rng = new System.Random(1234);
        occupied = new bool[width, height];
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        int stoneCount = GetStoneCount();
        for (int i = 0; i < stoneCount; i++)
        {
            Vector2Int pos = GetRandomPosition();
            while (IsOccupied(pos))
                pos = GetRandomPosition();


            GameObject prefab = GetStonePrefab();
            if (prefab != null)
            {
                PlaceStone(pos, prefab);
            }
        }
    }

    private int GetStoneCount()
    {
        return rng.Next(minStones, maxStones);
    }

    private Vector2Int GetRandomPosition()
    {
        int x = rng.Next(0, width);
        int y = rng.Next(0, height);
        return new Vector2Int(x, y);
    }

    private bool IsOccupied(Vector2Int pos)
    {
        return occupied[pos.x, pos.y];
    }

    private GameObject GetStonePrefab()
    {
        double roll = rng.NextDouble();

        if (roll < 0.05)
            return ironTile;

        if (roll < 0.15)
            return cuperTile;

        if (roll < 0.30)
            return coalTile;

        if (roll < 0.60)
            return stoneTile;

        return emptyTile;
    }

    private void PlaceStone(Vector2Int pos, GameObject prefab)
    {
        Vector3 worldPos = transform.position + new Vector3(pos.x, pos.y, 0);
        GameObject stone = _container.InstantiatePrefab(prefab, worldPos, Quaternion.identity, stonesParent);

        spawnedStones.Add(stone);
        occupied[pos.x, pos.y] = true;
    }
}
