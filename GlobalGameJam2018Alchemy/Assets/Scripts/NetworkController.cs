using System.Collections.Generic;
using UnityEngine;
using GlobalGameJam2018Networking;
using System;

[RequireComponent(typeof(GameController))]
[RequireComponent(typeof(LayoutController))]
public class NetworkController : MonoBehaviour
{
    private readonly Queue<Action> invokes = new Queue<Action>();
    private AlchemyNetwork Network { get; }
    public string PlumberName { get; private set; } = null;
    public bool Connected => PlumberName != null;
    public bool IsSinglePlayer { get; private set; } = true;

    public event Action ServerConnected;
    public event Action ServerStopped;
    public event Action<LevelConfig> StartMultiplayerLevel;

    public NetworkController()
    {
        Action<Action> addInvoke = invoke => { lock (invokes) { invokes.Enqueue(invoke); } };
        Network = new AlchemyNetwork(addInvoke);
        Network.Connected += plumberName =>
        {
            PlumberName = plumberName;
            ServerConnected?.Invoke();
        };
        Network.ServerStopped += () =>
        {
            ServerStopped?.Invoke();
            PlumberName = null;
            IsSinglePlayer = true;
        };
        Network.LevelStarted += levelConfig => StartMultiplayerLevel?.Invoke(levelConfig);
        Network.ReceivedIngredient += (ingredient, pipe) =>
        {
            var pipes = GetComponent<LayoutController>().InteractivePipes;
            if (pipes.ContainsKey(pipe.Id)) { pipes[pipe.Id].AddItem(ingredient); }
            else { Debug.LogError($"Got ingredient on invalid pipe '{pipe.Id}'"); }
        };
    }

    private void Start()
    {
        GetComponent<GameController>().GameOver += Network.GameOver;
    }

    /// <summary>Method that is called if the user enters username and hostname [and port] in the gui and clicks on connect.</summary>
    /// <param name="username">Username of the Alchemist.</param>
    /// <param name="hostname">Hostname of the Plumbers Machine.</param>
    /// <param name="port">Port on which the Plumbers game is runnning.</param>
    public void Connect(string username, string hostname, int port = NetworkBase.DefaultPort)
    {
        IsSinglePlayer = false;
        Network.Connect(username, hostname, port);
    }

    public void Disconnect()
    {
        IsSinglePlayer = true;
        Network.Disconnect();
    }

    private void Update()
    {
        lock (invokes)
        {
            while(invokes.Count > 0) { invokes.Dequeue()(); }
        }
    }

    /// <summary>Generates income by sending MoneyMaker item to plumber.</summary>
    public void SendMoneyMaker(MoneyMaker moneyMaker, Pipe pipe)
    {
        // Send item to plumber if connected, just swallow it otherwise
        if (Connected) { Network.SendMoneyMaker(moneyMaker, pipe); }
        GetComponent<GameController>().gold += moneyMaker.GoldValue;
    }
}
