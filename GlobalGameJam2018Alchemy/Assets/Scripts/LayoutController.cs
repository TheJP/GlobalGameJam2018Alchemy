﻿using GlobalGameJam2018Networking;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class LayoutController : MonoBehaviour
{
    public const float GridSpacing = 1f;
    public static readonly Vector3 Origin = new Vector3(0.5f, 0.5f, 0f);

    private readonly RecipeCreator recipeCreator = new RecipeCreator();

    [Tooltip("GameObject that will contain all the pipes.")]
    public Transform pipes;

    [Tooltip("Prefab used to create pipes.")]
    public InteractivePipe pipePrefab;

    [Tooltip("Width of the map.")]
    public int gridWidth = 16;

    [Tooltip("Height of the map.")]
    public int gridHeight = 16;

    [Tooltip("GameObject that will contain all the walls and floor objects.")]
    public Transform room;

    [Tooltip("Prefabs that are used to build straight walls.")]
    public GameObject[] wallPrefabs;

    [Tooltip("Prefab for the corner wall.")]
    public GameObject cornerPrefab;

    [Tooltip("Prefab for the wall with hole for the door.")]
    public GameObject doorWallPrfab;

    [Tooltip("Prefab for the door.")]
    public GameObject doorPrefab;

    [Tooltip("Floor prefab.")]
    public GameObject floorPrefab;

    [Tooltip("Player prefab.")]
    public GameObject playerPrefab;

    [Tooltip("GameObject that will contain all the workbenches.")]
    public Transform benches;

    [Tooltip("Set of workbench prefabs that are initially spawned for the player for free.")]
    public GameObject[] initialWorkbenches;

    private readonly Dictionary<int, InteractivePipe> interactivePipes = new Dictionary<int, InteractivePipe>();
    public IReadOnlyDictionary<int, InteractivePipe> InteractivePipes => new ReadOnlyDictionary<int, InteractivePipe>(interactivePipes);

    private void ClearLevel()
    {
        interactivePipes.Clear();
        foreach (Transform child in pipes) { Destroy(child.gameObject); }
        foreach (Transform child in room) { Destroy(child.gameObject); }
        foreach (Transform child in benches) { Destroy(child.gameObject); }
        Destroy(FindObjectOfType<PlayerMovement>()?.gameObject);
    }

    public void CreateLevel(LevelConfig levelConfig)
    {
        transform.parent.eulerAngles = new Vector3(0f, 0f, 0f);
        ClearLevel();
        CreatePipes(levelConfig);
        CreateWalls();
        CreateFloor();
        SpawnPlayer();
        CreateBenches();
        transform.parent.eulerAngles = new Vector3(90f, 0f, 0f);
    }

    /// <summary>Generate input pipes using the given <see cref="LevelConfig"/>.</summary>
    public void CreatePipes(LevelConfig levelConfig)
    {
        var position = Origin + Vector3.down * GridSpacing * 2;
        foreach (var pipeConfig in levelConfig.Pipes.OrderBy(p => p.Order))
        {
            var pipe = Instantiate(pipePrefab, position, Quaternion.identity, pipes);
            pipe.Pipe = pipeConfig;
            this.interactivePipes.Add(pipeConfig.Id, pipe);
            position += Vector3.down * GridSpacing;
        }
    }

    /// <summary>Automatically create the walls around the lab of the lab.</summary>
    public void CreateWalls()
    {
        var position = Origin + (Vector3.down + Vector3.right) * (GridSpacing / 2f);
        var direction = Vector3.right;
        var rotation = Quaternion.identity;
        // Create 4 walls
        for (int turn = 0; turn < 4; ++turn)
        {
            // Create random wall segments
            for (int i = 1; i < (turn % 2 == 0 ? gridWidth : gridHeight); ++i)
            {
                if (turn == 1 && i == gridHeight / 2)
                {
                    // Add door in the middle of the right wall
                    Instantiate(doorWallPrfab, position, rotation, room);
                    Instantiate(doorPrefab, position, rotation, room);
                }
                else { Instantiate(wallPrefabs[Random.Range(0, wallPrefabs.Length)], position, rotation, room); }
                position += direction * GridSpacing;
            }
            // Create corner
            Instantiate(cornerPrefab, position, rotation, room);
            rotation *= Quaternion.Euler(Vector3.back * 90);
            direction = Quaternion.Euler(Vector3.back * 90) * direction;
            position += direction * GridSpacing;
        }
    }

    /// <summary>Generate floor.</summary>
    public void CreateFloor()
    {
        var position = Origin +
            Vector3.down * GridSpacing +
            Vector3.forward * 0.05f;
        for (int y = 0; y < gridHeight; ++y)
        {
            for (int x = 0; x < gridWidth; ++x)
            {
                Instantiate(floorPrefab, position + Vector3.down * GridSpacing * y + Vector3.right * GridSpacing * x, Quaternion.identity, room);
            }
        }
    }

    /// <summary>Spawn player in the middle of the floor.</summary>
    public void SpawnPlayer()
    {
        Vector3 position = Origin +
            Vector3.down * GridSpacing * (gridHeight / 2 + 0.5f) +
            Vector3.right * GridSpacing * (gridWidth / 2) +
            Vector3.back * 25f;
        Instantiate(playerPrefab, position, Quaternion.Euler(-90f, 0f, 0f), transform.parent);
    }

    private void CreateBenches()
    {
        Vector3 position = Origin +
            Vector3.down * GridSpacing +
            Vector3.right * GridSpacing * 2;
        for (int i = 0; i < initialWorkbenches.Length; ++i)
        {
            var bench = Instantiate(initialWorkbenches[i], position, initialWorkbenches[i].transform.rotation, benches);
            position += Vector3.right * GridSpacing * 1.5f;

            Workbench workbench = bench.GetComponent<Workbench>();
            switch (workbench?.recipeKey ?? "default")
            {
                case "oven":
                    workbench.MyRecipes = recipeCreator.BakeRecipes;
                    break;
                case "kettle":
                    workbench.MyRecipes = recipeCreator.TeaRecipes;
                    break;
                case "cauldron":
                    workbench.MyRecipes = recipeCreator.CauldronRecipes;
                    break;
                case "mortar":
                    workbench.MyRecipes = recipeCreator.MortarRecipes;
                    break;
                case "dest":
                    workbench.MyRecipes = recipeCreator.DestillRecipes;
                    break;
                case "tower":
                    break;
                    
            }
        }
    }
}
