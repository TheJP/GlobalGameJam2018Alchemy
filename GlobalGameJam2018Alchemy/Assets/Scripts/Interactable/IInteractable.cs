using GlobalGameJam2018Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Makes an GameObject Interactable
/// </summary>
public interface IInteractable
{


    bool CanInteract(IItem item);


    bool PutItem(IItem item);
    IItem GetItem();

}

