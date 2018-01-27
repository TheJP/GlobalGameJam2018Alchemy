using GlobalGameJam2018Networking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LayoutController : MonoBehaviour
{
    public const float GridSpacing = 1f;
    public static readonly Vector3 Origin = new Vector3(0.5f, 0.5f, 0f);

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

    /// <summary>Generate input pipes using the given <see cref="LevelConfig"/>.</summary>
    public void CreatePipes(LevelConfig levelConfig)
    {
        var position = Origin + Vector3.down * GridSpacing * 2;
        foreach (var pipeConfig in levelConfig.Pipes.OrderBy(p => p.Order))
        {
            var pipe = Instantiate(pipePrefab, position, Quaternion.identity, pipes);
            pipe.Pipe = pipeConfig;
            position += Vector3.down * GridSpacing;
        }
    }

    /// <summary>Automatically create the walls around the lab of the lab.</summary>
    public void CreateWalls()
    {
        var position = Origin + (Vector3.down + Vector3.right) * (GridSpacing / 2f);
        var direction = Vector3.right;
        var rotation = Quaternion.identity;
        for (int turn = 0; turn < 4; ++turn)
        {
            for (int i = 1; i < (turn % 2 == 0 ? gridWidth : gridHeight) - 1; ++i)
            {
                if (turn == 1 && i == gridHeight / 2)
                {
                    Instantiate(doorWallPrfab, position, rotation, room);
                    Instantiate(doorPrefab, position, rotation, room);
                }
                else { Instantiate(wallPrefabs[Random.Range(0, wallPrefabs.Length)], position, rotation, room); }
                position += direction * GridSpacing;
            }
            Instantiate(cornerPrefab, position, rotation, room);
            rotation *= Quaternion.Euler(Vector3.back * 90);
            direction = Quaternion.Euler(Vector3.back * 90) * direction;
            position += direction * GridSpacing;
        }
    }

    public void CreateFloor()
    {
        // TODO: Implement
    }
}
