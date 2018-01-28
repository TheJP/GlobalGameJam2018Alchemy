using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int Gold { get; set; }
    public event Action<bool> GameOver;

    internal void TriggerGameOver(bool success) => GameOver?.Invoke(success);
}
