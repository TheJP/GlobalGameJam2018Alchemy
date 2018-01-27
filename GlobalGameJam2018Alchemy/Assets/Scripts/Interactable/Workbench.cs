using GlobalGameJam2018Networking;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Workbench : MonoBehaviour, IInteractable
{
    /// <summary>
    /// The recipes that will be accepted by the workshop
    /// </summary>
    public List<Recipe> MyRecipes
    {
        set;
        private get;
    }

    /// <summary>
    /// Current items in the input-pipeline
    /// </summary>
    private List<IItem> InItems;

    /// <summary>
    /// Current Item in the output-pipeline
    /// </summary>
    private IItem OutItem;

    /// <summary>
    /// The efficiency of the workbench, determines the production speed
    /// </summary>
    public float Efficiency
    {
        set;
        private get;
    }

    /// <summary>
    /// Initial State : The workbench is currently free of work, so no recipe is worked on
    /// </summary>
    private Recipe currentRecipe = null;

    /// <summary>
    /// Decider if the machine can be started
    /// </summary>
    /// <returns>The recipe that can be done</returns>
    private Recipe CanStartup()
    {
        // detects if the machine is not working and the output is empty
        if(currentRecipe == null && OutItem == null)
        {
            return this.MyRecipes.FirstOrDefault(AllIngredientsAvailable);
        }
        return null;
    }

    private bool AllIngredientsAvailable(Recipe recipe)
    {
        // PRECONDITION: all items in InItems are unique!
        // ToDo: check for precondition enforcement?
        return recipe.InItems.Count == InItems.Count
            && InItems.All(recipe.AsksForInputItem);
    }

    /// <summary>
    /// detect if an item fits in the input of a recipe
    /// </summary>
    /// <returns></returns>
    private bool DoesAnyRecipeAskForItem(IItem item) {
        // if we already have it, we thread it so as no recipe ask for it
        if (this.InItems.Any(i => IsSameItemType(i, item)))
        {
            return false;
        }
        return this.MyRecipes.Any(p => RecipeFullfillable(p, item));
    }

    private bool RecipeFullfillable(Recipe recipe, IItem item)
    {
        return this.InItems.All(recipe.AsksForInputItem)
            && recipe.AsksForInputItem(item);
    }

    private bool IsSameItemType(IItem a, IItem b)
    {
        if(a.Type == b.Type)
        {
            // ToDo: Do only Ingredient have additional logic
            var aIngredient = a as Ingredient;
            var bIngredient = b as Ingredient;

            return aIngredient?.Colour == bIngredient?.Colour;
        }
        return false;
    }

    /// <summary>
    /// Starts up the machine
    /// </summary>
    private void Startup()
    {
        Recipe recipe = CanStartup();
        if (recipe != null)
        {
            currentRecipe = recipe;
            StartAnimation();
            Invoke("OnFinish", recipe.Complexity/this.Efficiency);
        }

    }

    /// <summary>
    /// onFinish Method is called when the call timer is finished 
    /// </summary>
    private void OnFinish()
    {
        StopAnimation();
        OutItem = currentRecipe.CreateItem();
        currentRecipe = null;
    }

    private void StartAnimation()
    {
        // ToDo: animate some fancy shit
    }

    private void StopAnimation()
    {
        // stop animating shit
    }

    /// <summary>
    /// Returns if the user can interact, returns false, if it is in use.
    /// </summary>
    /// <returns></returns>
    public bool CanInteract(IItem item)
    {
        if (item == null)
        {
            return OutItem != null;
        }
        else
        {
            return this.currentRecipe == null 
                && this.OutItem == null 
                && DoesAnyRecipeAskForItem(item);
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
