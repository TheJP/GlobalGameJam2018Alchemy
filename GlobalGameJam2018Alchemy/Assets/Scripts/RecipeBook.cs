using System.Collections;
using System.Collections.Generic;
using GlobalGameJam2018Networking;
using UnityEngine;

public class RecipeBook : MonoBehaviour, IInteractable{

    /// <summary>
    /// The RecipeBook should be always interactable
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool CanInteract(IItem item)
    {
        return true;
    }

    /// <summary>
    /// Has to do the same as PutItem
    /// </summary>
    public IItem GetItem()
    {
        PutItem(null);
        return null;
    }

    /// <summary>
    /// Returns true, when operation is successfull
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool PutItem(IItem item)
    {
        DisplayRecipes();
        return false;
    }

    public void DisplayRecipes() {

        // TODO: left to be implemented by a cat.
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
