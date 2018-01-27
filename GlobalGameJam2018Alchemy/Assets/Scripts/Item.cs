using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    enum Type { powder, herb, hotLiquid, coldLiquid, mana, steam, paste};

    // by element the color of the type is meant.
    enum Element { blue, green, yellow, orange, red, violet}

    private Type myType;
    private Element myElement;
    private int quantity = 1;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {

	}
}
