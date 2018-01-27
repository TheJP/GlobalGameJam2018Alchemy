using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workbench : MonoBehaviour, IInteractable
{
    /// <summary>
    /// The recipes that will be accepted by the workshop
    /// </summary>
    private List<Recipe> myRecipes;

    /// <summary>
    /// Items that are ingoing
    /// </summary>
    private List<Item> inItems;

    /// <summary>
    /// Item that is produced
    /// </summary>
    private Item outItem;

    /// <summary>
    /// The efficiency of the workbench, determines the production speed
    /// </summary>
    private int efficiency = 1;

    /// <summary>
    /// Initial State : The workbench is currently free of work, and can be used by an interaction
    /// </summary>
    private bool available = true;

    /// <summary>
    /// Initial State : The workbench has no Item in the output, if there is an item then it has to be removed before adding another item.
    /// </summary>
    private bool output = false;


    /// <summary>
    /// Sets the efficiency of the workbench
    /// </summary>
    /// <param name="efficiency"></param>
    public void SetEfficiency(int efficiency)
    {
        this.efficiency = efficiency;
    }

    /// <summary>
    /// Sets the Recipes that can be used.
    /// </summary>
    /// <param name="myRecipes"></param>
    public void SetMyRecipes(List<Recipe> myRecipes) {
        this.myRecipes = myRecipes;
    }


    /// <summary>
    /// Returns if the user can interact, returns false, if it is in use.
    /// </summary>
    /// <returns></returns>
    public bool CanInteract()
    {
        return this.available;
    }

    /// <summary>
    /// Only if the user canInteract and the Workshop has not a full output it can be used
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool PutItem(Item item)
    {
        if (!this.output) {
            //FIGURE OUT if A Recipe is available at the workbench with the item currently holding.
            
            //drop the item to the workshop
            
            return true;
        }
        return false;
    }
    

    /// <summary>
    /// Gets the item if the output is full
    /// </summary>
    /// <returns></returns>
    public Item GetItem()
    {
        if (this.output) {
            
        }

        throw new System.NotImplementedException();
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
