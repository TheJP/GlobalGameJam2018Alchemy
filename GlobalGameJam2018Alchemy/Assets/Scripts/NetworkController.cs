using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalGameJam2018Networking;
using System;

public class NetworkController : MonoBehaviour
{
    private readonly Queue<Action> invokes = new Queue<Action>();
    private AlchemyNetwork Network { get; }
    public string PlumberName { get; private set; } = null;
    private bool Connected => PlumberName != null;

    public NetworkController()
    {
        Action<Action> addInvoke = invoke => { lock (invokes) { invokes.Enqueue(invoke); } };
        Network = new AlchemyNetwork(addInvoke);
        Network.Connected += plumberName => PlumberName = plumberName;;
        Network.ServerStopped += () => PlumberName = null;
    }

    private void Update()
    {
        lock (invokes)
        {
            while(invokes.Count > 0) { invokes.Dequeue()(); }
        }
    }

    public void SendMoneyMaker(MoneyMaker moneyMaker, Pipe pipe)
    {
        // TODO: Generate income here
        // Send item to plumber if connected, just swallow it otherwise
        if (Connected) { Network.SendMoneyMaker(moneyMaker, pipe); }
    }
}
