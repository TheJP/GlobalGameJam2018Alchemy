using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ItemSignatures;
using GlobalGameJam2018Networking;
using UnityEngine;

public class Tower : MonoBehaviour, IInteractable {

    /// <summary>
    /// describes the amount of shots of the color, can be upgraded
    /// </summary>
    private int QuantityPerItem = 5;

    private ProcessedItemSignature towerRequest;

    /// <summary>
    /// Storage of the EnergyAmunition, only one ammunition.
    /// </summary>
    private ProcessedItem EnergyAmunition;

    private int Amunition = 0;


    private bool CanShoot() {
        return Amunition > 0;
    }

    /// <summary>
    /// shooting Enemy, decreasing the Quantity of the EnergyAmunition supplied
    /// </summary>
    public bool Shoot() {
        if (CanShoot()) {

            //TODO ENEMY Implementation!
            Amunition--;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Only ProcessedItems of the Type Energy will be accepted
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool CanInteract(IItem item)
    {
        ProcessedItem proItem = item as ProcessedItem;
        return proItem?.ProcessedType == ProcessedItem.ProcessedItemType.Energy;
    }

    /// <summary>
    /// There is nothing you need to get out of the tower.
    /// </summary>
    public IItem GetItem()
    {
        return null;
    }

    public bool PutItem(IItem item)
    {
        if (CanInteract(item)) {
            EnergyAmunition = (ProcessedItem)item;
            Amunition = 5;
            //remove the item holding
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
