using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : ScriptableObject
{

    // Items used for the recipe
    private List<Item> inItems;

    // Item received for completing the recipe
    private Item outItem;

    // Is used for time calculation, together with workbench efficiency
    private int complexity;

    /// <summary>
    /// Creates a Recipe that can have multiple or just one item inItems, that will be 
    /// used to fullfill the Recipe. By fullfilling the recipe the outItem will be given out.
    /// Complexity describes the difficulty to complete the task.
    /// </summary>
    /// <param name="inItems"></param>
    /// <param name="outItem"></param>
    /// <param name="complexity"></param>
    public Recipe(List<Item> inItems, Item outItem, int complexity)
    {
        this.inItems = inItems;
        this.outItem = outItem;
        this.complexity = complexity;
    }
}
