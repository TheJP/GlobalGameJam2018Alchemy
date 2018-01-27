﻿using GlobalGameJam2018Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe
{

    // Items used for the recipe
    private List<IItem> InItems {
        get;
    }

    // Item received for completing the recipe
    private IItem OutItem {
        get;
    }

    // Is used for time calculation, together with workbench efficiency
    private int Complexity {
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
    public Recipe(List<IItem> inItems, IItem outItem, int complexity)
    {
        this.InItems = inItems;
        this.OutItem = outItem;
        this.Complexity = complexity;
    }
}
