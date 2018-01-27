using GlobalGameJam2018Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProducedItem : MonoBehaviour, IItem {

    public enum ProcessedItemType { Herbessence, Slimeessence, Powderessence, Steamessence, Fairiedust, Lightifier, Fireflies, Glowstone}

    public ItemType Type
    {
        get
        {
            return ItemType.Processed;        
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
