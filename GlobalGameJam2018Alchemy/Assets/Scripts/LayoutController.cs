using GlobalGameJam2018Networking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LayoutController : MonoBehaviour
{
    [Tooltip("GameObject that will contain all the pipes. The position of this GameObject will be used to place the top most pipe.")]
    public Transform pipes;

    [Tooltip("Prefab used to create pipes.")]
    public InteractivePipe pipePrefab;

    [Tooltip("Defines how much vertical distance will be put between two pipes.")]
    public float pipeVerticalSpacing = 2f;

    public void CreatePipes(LevelConfig levelConfig)
    {
        var position = pipes.position;
        foreach (var pipeConfig in levelConfig.Pipes.OrderBy(p => p.Order))
        {
            var pipe = Instantiate(pipePrefab, position, Quaternion.identity, pipes);
            position += Vector3.down * pipeVerticalSpacing;
        }
    }
}
