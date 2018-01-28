using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalGameJam2018Networking;

public class InteractivePipe : MonoBehaviour, IInteractable
{
    [Tooltip("Object that is set to active if this pipe is an input.")]
    public GameObject inputArrow;

    [Tooltip("Object that is set to active if this pipe is an output.")]
    public GameObject outputArrow;

    [Tooltip("GameObject to visualise the content of the interactive pipe.")]
    public ItemDisplay itemDisplay;

    public Pipe Pipe { get; set; }

    private readonly Queue<Ingredient> waitingIngredients = new Queue<Ingredient>();

    private void Start()
    {
        if(Pipe.Direction == PipeDirection.ToAlchemist) { inputArrow?.SetActive(true); }
        else { outputArrow?.SetActive(true); }
    }

    public bool CanInteract(IItem item)
    {
        if (item == null){ return Pipe.Direction == PipeDirection.ToAlchemist && waitingIngredients.Count > 0; }
        else { return Pipe.Direction == PipeDirection.ToPipes && item is MoneyMaker; }
    }

    public IItem GetItem()
    {
        if (waitingIngredients.Count > 0)
        {
            var item = waitingIngredients.Dequeue();
            itemDisplay?.Display(waitingIngredients.Count > 0 ? waitingIngredients.Peek() : null);
            return item;
        }
        return null;
    }

    public bool PutItem(IItem item)
    {
        if (item != null && CanInteract(item))
        {
            itemDisplay?.Display(item);
            FindObjectOfType<NetworkController>().SendMoneyMaker(item as MoneyMaker, Pipe);
            return true;
        }
        else { return false; }
    }

    public void AddItem(Ingredient item)
    {
        if (waitingIngredients.Count <= 0)
        {
            itemDisplay?.Display(item);
        }
        waitingIngredients.Enqueue(item);
    }
}
