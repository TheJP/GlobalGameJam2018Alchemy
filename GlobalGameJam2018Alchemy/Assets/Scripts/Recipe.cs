using Assets.Scripts.Recipe;
using GlobalGameJam2018Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Recipe
{

    // Items used for the recipe
    public IList<ItemSignature> InItems {
        get;
    }

    // Item received for completing the recipe
    public Func<IItem> CreateItem {
        get;
    }

    // Is used for time calculation, together with workbench efficiency
    public int Complexity {
        get;
    }

    /// <summary>
    /// Creates a Recipe that can have multiple or just one item inItems, that will be 
    /// used to fullfill the Recipe. By fullfilling the recipe the outItem will be given out.
    /// Complexity describes the difficulty to complete the task.
    /// </summary>
    /// <param name="inItems"></param>
    /// <param name="outItem"></param>
    /// <param name="complexity"></param>
    public Recipe(List<ItemSignature> inItems, Func<IItem> createItem, int complexity)
    {
        this.InItems = inItems;
        this.CreateItem = createItem;
        this.Complexity = complexity;
    }

    public bool AsksForInputItem(IItem item)
    {
        return InItems.Any(p => p.IsItemAcceptable(item));
    }
}
