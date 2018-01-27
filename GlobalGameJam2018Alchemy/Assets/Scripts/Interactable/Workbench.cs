using GlobalGameJam2018Networking;
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
    private List<IItem> inItems;

    /// <summary>
    /// Item that is produced
    /// </summary>
    private IItem outItem;

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
    /// Decider if the machine can be started
    /// </summary>
    /// <returns></returns>
    public bool CanStartup() {
        // detects if the machine is not working and the output is empty
        bool firstCondition = available && !output;
        // detects if items for a recipe exist
        bool secondCondition = RecipeFullfilled();
        return firstCondition && secondCondition;

    }

    /// <summary>
    /// detect if an item fits in the input of a recipe
    /// </summary>
    /// <returns></returns>
    public bool RecipeItemsAvailable(IItem item) {
        //TODO: RECIPE SYSTEM HAS TO BE FINISHED
        return true;
    }

    /// <summary>
    /// checks wheter any recipe available is fullfilled
    /// </summary>
    /// <returns></returns>
    public bool RecipeFullfilled() {
        //TODO: REcipe System has to be finished
        return true;
    }


    /// <summary>
    /// Starts up the machine
    /// </summary>
    public void Startup() {
        if (CanStartup()){
        //TODO WORK HERE NOW!
        available = false;
        // call timer and onFinish method

        }

    }

    /// <summary>
    /// onFinish Method is called when the call timer is finished 
    /// </summary>
    public void OnFinish() {
        available = true;
        output = true;
    }





    /// <summary>
    /// Returns if the user can interact, returns false, if it is in use.
    /// </summary>
    /// <returns></returns>
    public bool CanInteract(IItem item)
    {
        if (item == null)
        {
            return this.available && this.output;
        }
        else {
            return this.available && !this.output && RecipeItemsAvailable(item);
        }
    }

    /// <summary>
    /// Only if the user canInteract and the Workshop has not a full output it can be used
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool PutItem(IItem item)
    {
        if (CanInteract(item)) {

            //add the item to the workshop
            inItems.Add(item);
            Startup();
            //Player has to drop the item to the workshop
            return true;
        }
        return false;
    }
    

    /// <summary>
    /// Gets the item if the output is full
    /// </summary>
    /// <returns></returns>
    public IItem GetItem()
    {
        if (CanInteract(null)) {
            return outItem;            
        }
        return null;
    }

    // Use this for initialization
    void Start()
    {
        //load recipes by call to SetMyRecipes
    }

    // Update is called once per frame
    void Update()
    {

    }


}
