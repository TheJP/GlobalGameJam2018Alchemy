using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Makes an GameObject Interactable
/// </summary>
public interface IInteractable
{


    bool CanInteract(Item item);


    bool PutItem(Item item);
    Item GetItem();

}

