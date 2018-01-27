using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int Gold { get; set; }
    public event Action<bool> GameOver;
}
