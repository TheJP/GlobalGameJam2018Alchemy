using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalGameJam2018Networking;

public class InteractivePipe : MonoBehaviour, IInteractable
{
    public Pipe Pipe { get; set; }

    private readonly Queue<Ingredient> waitingIngredients = new Queue<Ingredient>();

    public bool CanInteract(IItem item) => false;

    public IItem GetItem() => waitingIngredients.Count > 0 ? waitingIngredients.Dequeue() : null;

    public bool PutItem(IItem item)
    {
        throw new System.NotImplementedException();
    }
}
