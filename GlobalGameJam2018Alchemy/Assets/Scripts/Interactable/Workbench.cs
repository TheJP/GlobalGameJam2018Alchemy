using GlobalGameJam2018Networking;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts.ItemSignatures;

public class Workbench : MonoBehaviour, IInteractable
{
    [Tooltip("Where the current input-objects dhould be spawned.")]
    public Transform[] InputItemLocations;

    [Tooltip("Where the produced item should be placed upon completion.")]
    public Transform OutputItemLocation;

    [Tooltip("The element displaying the working effect to be displayed.")]
    public GameObject ActiveObject;

    [Tooltip("The Gameobject responsible for providing the correct Prefabs for displaying items.")]
    public PrefabLibraryBase PrefabLibrary;

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
    private readonly List<IItem> InItems = new List<IItem>();

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

    private readonly List<GameObject> temporaryObjects = new List<GameObject>();

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
        if(a.Type == b.Type && a.GetType() == b.GetType())
        {
            // ToDo: Do only Ingredient have additional logic
            // ToDo: change code to a.equals(b)
            var aIngredient = a as Ingredient;
            if(aIngredient != null)
            {
                var bIngredient = b as Ingredient;
                return aIngredient?.Colour == bIngredient?.Colour;
            }

            var aProcessed = a as ProcessedItem;
            if (aProcessed != null)
            {
                var bProcessed = b as ProcessedItem;
                return aProcessed?.ProcessedColor == bProcessed?.ProcessedColor
                    && aProcessed?.ProcessedType == bProcessed?.ProcessedType;
            }

            var aMoneyMaker = a as MoneyMaker;
            if (aMoneyMaker != null)
            {
                var bMoneyMaker = b as MoneyMaker;
                return string.Equals(aMoneyMaker?.Name, bMoneyMaker?.Name);
            }
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
            InItems.Clear();
            StartAnimation();
            Invoke("OnFinish", recipe.Complexity/this.Efficiency);

            DestroyTempObjects();
        }

    }

    /// <summary>
    /// onFinish Method is called when the call timer is finished 
    /// </summary>
    private void OnFinish()
    {
        StopAnimation();
        DestroyTempObjects();
        // ToDo: this

        OutItem = currentRecipe.CreateItem();
        temporaryObjects.Add(Instantiate(PrefabLibrary.GetPrefab(OutItem), OutputItemLocation.transform));
        currentRecipe = null;
    }

    private void DestroyTempObjects()
    {
        foreach (var o in temporaryObjects)
        {
            Destroy(o);
        }
        temporaryObjects.Clear();
    }

    private void StartAnimation()
    {
        if (ActiveObject != null)
        {
            this.ActiveObject.SetActive(true);
        }
    }

    private void StopAnimation()
    {
        if (ActiveObject != null)
        {
            this.ActiveObject.SetActive(false);
        }
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
        if (CanInteract(item))
        {
            //add the item to the workshop
            InItems.Add(item);
            temporaryObjects.Add(Instantiate(PrefabLibrary.GetPrefab(item), InputItemLocations[InItems.Count - 1].transform));

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
            DestroyTempObjects();
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
