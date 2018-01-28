using System;
using System.Collections;
using System.Collections.Generic;
using GlobalGameJam2018Networking;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public ItemDisplay ItemDisplayLeft;
    public ItemDisplay ItemDisplayRight;

    private readonly List<IItem> ItemsStored = new List<IItem>();
    private int Places = 2;
    private int Used => ItemsStored.Count;

    public bool CanInteract(IItem item)
    {

        if (item != null)
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
            DisplayItems();
            return tempItem;
        }
        return null;
    }

    public bool PutItem(IItem item)
    {
        if (CanInteract(item)) {
            ItemsStored.Add(item);
            DisplayItems();
            return true;
        }
        return false;
    }

    private void DisplayItems()
    {
        if (Used > 1)
        {
            ItemDisplayRight?.Display(ItemsStored[1]);
            ItemDisplayLeft?.Display(ItemsStored[0]);
        }
        else if (Used > 0)
        {
            ItemDisplayRight?.HideAll();
            ItemDisplayLeft?.Display(ItemsStored[0]);
        }
        else
        {
            ItemDisplayLeft?.HideAll();
            ItemDisplayRight?.HideAll();
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
