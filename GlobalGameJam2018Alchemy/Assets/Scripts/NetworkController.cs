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
    public LevelConfig Level { get; private set; } = null;
    public string PlumberName { get; private set; } = null;
    public bool Connected => PlumberName != null;
    public bool IsSinglePlayer { get; private set; } = false;

    public event Action ServerConnected;
    public event Action ServerStopped;

    public NetworkController()
    {
        Action<Action> addInvoke = invoke => { lock (invokes) { invokes.Enqueue(invoke); } };
        Network = new AlchemyNetwork(addInvoke);
        Network.Connected += plumberName =>
        {
            PlumberName = plumberName;
            IsSinglePlayer = false;
            ServerConnected?.Invoke();
        };
        Network.ServerStopped += () => PlumberName = null;
        Network.ServerStopped += () => ServerStopped?.Invoke();
        Network.LevelStarted += levelConfig =>
        {
            Level = levelConfig;
            GetComponent<LayoutController>().CreateLevel(levelConfig);
        };
        Network.ReceivedIngredient += (ingredient, pipe) =>
        {
            var pipes = GetComponent<LayoutController>().InteractivePipes;
            if (pipes.ContainsKey(pipe.Id)) { pipes[pipe.Id].AddItem(ingredient); }
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
        Network.Connect(username, hostname, port);
    }

    public void Disconnect() => Network.Disconnect();

    /// <summary>Method that is called if the user clicks on single player.</summary>
    public void PlaySinglePlayer()
    {
        GetComponent<LayoutController>().CreateLevel(LevelConfig.Builder("Singleplayer")
            .AddPipe(PipeDirection.ToAlchemist, 0)
            .AddPipe(PipeDirection.ToAlchemist, 1)
            .AddPipe(PipeDirection.ToPipes, 2)
            .Create());
        IsSinglePlayer = true;
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
