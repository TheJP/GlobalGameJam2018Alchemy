using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{


    bool CanInteract();


    bool PutItem(Item item);
    Item GetItem();

}

