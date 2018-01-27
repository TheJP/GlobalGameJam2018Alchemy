using System.Collections;
using System.Collections.Generic;
using GlobalGameJam2018Networking;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable{

    List<IItem> ItemsStored;
    private int Places = 2;
    private int Used = 0;

    public bool CanInteract(IItem item)
    {
        if (item == null)
        {
            return Places > Used;
        }
        else
        {
            return Used > 0;
        }
    }

    public IItem GetItem()
    {
        if (CanInteract(null)) {
            IItem tempItem = ItemsStored[0];
            ItemsStored.RemoveAt(0);
            return tempItem;
        }
        return null;
    }

    public bool PutItem(IItem item)
    {
        if (CanInteract(item)) {
            ItemsStored.Add(item);
            return true;
        }
        return false;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
