using GlobalGameJam2018Networking;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class LayoutController : MonoBehaviour
{
    /// <summary>
    /// Space between the centre of two directly adijacent grid tiles.
    /// </summary>
    public const float GridSpacing = 1f;

    private static readonly Vector3 DoorPosition = new Vector3(0f, 0f, 0f);

    /// <summary>
    /// Origin of the level creation.
    /// </summary>
    private Vector3 Origin => DoorPosition +
            Vector3.left * GridSpacing * Mathf.Floor(gridWidth / 2f) +
            Vector3.forward * GridSpacing * gridHeight +
            Vector3.back * 0.5f * GridSpacing;

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

    private GameObject player = null;

    private void ClearLevel()
    {
        interactivePipes.Clear();
        foreach (Transform child in pipes) { Destroy(child.gameObject); }
        foreach (Transform child in room) { Destroy(child.gameObject); }
        foreach (Transform child in benches) { Destroy(child.gameObject); }
        if (player != null) { Destroy(player); }
    }

    public void CreateLevel(LevelConfig levelConfig)
    {
        ClearLevel();
        CreatePipes(levelConfig);
        CreateWalls();
        CreateFloor();
        SpawnPlayer();
        CreateBenches();
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
        var position = Origin +
            Vector3.forward * 0.5f * GridSpacing; // 0.5 forward from origin, because the wall is at the edge of the grid
        var direction = Vector3.right;
        var rotation = Quaternion.identity;
        // Create 4 walls
        for (int turn = 0; turn < 4; ++turn)
        {
            // Leave space for the corner
            position += direction * GridSpacing;

            // Create wall segments
            for (int i = 1; i < (turn % 2 == 0 ? gridWidth : gridHeight) - 1; ++i)
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

            // Rotate for next wall
            position += direction * 0.5f * GridSpacing;
            rotation *= Quaternion.Euler(Vector3.up * 90);
            direction = Quaternion.Euler(Vector3.up * 90) * direction;
            position += direction * 0.5f * GridSpacing;
        }
    }

    /// <summary>Generate floor.</summary>
    public void CreateFloor()
    {
        var position = Origin;
        for (int y = 0; y < gridHeight; ++y)
        {
            for (int x = 0; x < gridWidth; ++x)
            {
                Instantiate(floorPrefab, position + Vector3.back * GridSpacing * y + Vector3.right * GridSpacing * x, Quaternion.identity, room);
            }
        }
    }

    /// <summary>Spawn player in the middle of the floor.</summary>
    public void SpawnPlayer()
    {
        // Spawn alchemist high up in the air. Landing near to the door
        Vector3 position = DoorPosition +
            Vector3.forward * 2f * GridSpacing +
            Vector3.up * 25f;
        player = Instantiate(playerPrefab, position, Quaternion.identity, transform.parent);
    }

    /// <summary>Spawn all workbenches that are given as prefabs for initial spawning.</summary>
    private void CreateBenches()
    {
        var position = Origin;
        var workBenches = new List<GameObject>();

        // Spawn all workbenches
        for (int i = 0; i < initialWorkbenches.Length; ++i)
        {
            var rotation = initialWorkbenches[i].transform.rotation;
            workBenches.Add(Instantiate(initialWorkbenches[i], position, rotation, benches));
            position += Vector3.right * GridSpacing * 2f;
        }

        // Assign the correct recipes to each workbench
        var recipeCreator = workBenches.Select(b => b.GetComponent<RecipeBook>()).First(b => b != null).RecipeCreator;
        var recipeLists = new Dictionary<string, List<Recipe>>()
        {
            ["oven"] = recipeCreator.BakeRecipes,
            ["kettle"] = recipeCreator.TeaRecipes,
            ["cauldron"] = recipeCreator.CauldronRecipes,
            ["mortar"] = recipeCreator.MortarRecipes,
            ["dest"] = recipeCreator.DestillRecipes,

        };
        foreach (var workbench in workBenches
            .Select(bench => bench.GetComponent<Workbench>())
            .Where(bench => bench != null && bench.recipeKey != null && recipeLists.ContainsKey(bench.recipeKey)))
        {
            workbench.MyRecipes = recipeLists[workbench.recipeKey];
        }
    }
}
