using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalGameJam2018Networking;

public class InteractivePipe : MonoBehaviour, IInteractable
{
    public Pipe Pipe { get; set; }

    private readonly Queue<Ingredient> waitingIngredients = new Queue<Ingredient>();

    public bool CanInteract(IItem item)
    {
        if (item == null){ return Pipe.Direction == PipeDirection.ToAlchemist && waitingIngredients.Count > 0; }
        else { return Pipe.Direction == PipeDirection.ToPipes && item is MoneyMaker; }
    }

    public IItem GetItem() => waitingIngredients.Count > 0 ? waitingIngredients.Dequeue() : null;

    public bool PutItem(IItem item)
    {
        if (item != null && CanInteract(item))
        {
            FindObjectOfType<NetworkController>().SendMoneyMaker(item as MoneyMaker, Pipe);
            return true;
        }
        else { return false; }
    }

    public void AddItem(Ingredient item)
    {
        if (waitingIngredients.Count <= 0) { } // TODO: Show new item arrived
        waitingIngredients.Enqueue(item);
    }
}
