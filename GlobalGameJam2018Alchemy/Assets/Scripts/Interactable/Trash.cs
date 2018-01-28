using System.Collections;
using System.Collections.Generic;
using GlobalGameJam2018Networking;
using UnityEngine;

public class Thrash : MonoBehaviour, IInteractable
{

    List<IItem> ItemsStored;
    private int Places = 2;
    private int Used = 0;

    public bool CanInteract(IItem item)
    {
        return item != null;
        
    }

    public IItem GetItem()
    {
        return null;
    }

    public bool PutItem(IItem item)
    {
        if (CanInteract(item))
        {
            //remove item
            return true;
        }
        return false;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
