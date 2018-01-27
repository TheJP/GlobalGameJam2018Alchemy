using GlobalGameJam2018Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workbench : MonoBehaviour, IInteractable
{
    /// <summary>
    /// The recipes that will be accepted by the workshop
    /// </summary>
    public List<Recipe> MyRecipes {
        set;
        private get;
    }

    /// <summary>
    /// Items that are ingoing
    /// </summary>
    private List<IItem> InItems;

    /// <summary>
    /// Item that is produced
    /// </summary>
    private IItem OutItem;

    /// <summary>
    /// The efficiency of the workbench, determines the production speed
    /// </summary>
    public int Efficiency{
        set;
        private get;
    }

    /// <summary>
    /// Initial State : The workbench is currently free of work, and can be used by an interaction
    /// </summary>
    private bool available = true;

    /// <summary>
    /// Initial State : The workbench has no Item in the output, if there is an item then it has to be removed before adding another item.
    /// </summary>
    private bool output = false;



    /// <summary>
    /// Decider if the machine can be started
    /// </summary>
    /// <returns></returns>
    private bool CanStartup() {
        // detects if the machine is not working and the output is empty
        bool firstCondition = available && !output;
        // detects if items for a recipe exist
        bool secondCondition = RecipeRequirementsCheck();
        return firstCondition && secondCondition;

    }

    /// <summary>
    /// detect if an item fits in the input of a recipe
    /// </summary>
    /// <returns></returns>
    private bool RecipeItemsAvailable(IItem item) {
        //TODO: RECIPE SYSTEM HAS TO BE FINISHED
        return true;
    }

    /// <summary>
    /// checks wheter any recipe available is fullfilled
    /// </summary>
    /// <returns></returns>
    private bool RecipeRequirementsCheck() {
        //TODO: REcipe System has to be finished
        return true;
    }


    /// <summary>
    /// Starts up the machine
    /// </summary>
    private void Startup() {
        if (CanStartup()){

        available = false;
            Invoke("OnFinish", this.Efficiency);//TODO RECIPE TIME DIFFICULTY);
        }

    }

    /// <summary>
    /// onFinish Method is called when the call timer is finished 
    /// </summary>
    private void OnFinish() {
        available = true;
        output = true;
        //TODO set OUTPUT ITEM!
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
            InItems.Add(item);
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
            IItem itemReturn = OutItem;
            OutItem = null;
            return itemReturn;
        }
        return null;
    }

    // Use this for initialization
    void Start()
    {
        Efficiency = 1;
        //load recipes by call to SetMyRecipes
    }

    // Update is called once per frame
    void Update()
    {

    }


}
